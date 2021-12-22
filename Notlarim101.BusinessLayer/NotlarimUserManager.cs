using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim101.BusinessLayer.Abstract;
using Notlarim101.DataAccessLayer.EntityFramework;
using Notlarim101.Entity;
using Notlarim101.Entity.Messages;
using Notlarim101.Entity.ValueObject;

namespace Notlarim101.BusinessLayer
{
    public class NotlarimUserManager
    {
        //kullanıcı adı
        //kullanıcı mail kontrol
        //kayıt işlemini gerçekleştir
        //aktivasyon e posta
        private Repository<NotlarimUser> ruser = new Repository<NotlarimUser>();
        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = ruser.Find(s => s.Username == data.UserName || s.Email == data.EMail);

            BusinessLayerResult<NotlarimUser> lr = new BusinessLayerResult<NotlarimUser>();

            if (user != null)
            {
                if (user.Username==data.UserName)
                {
                    lr.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanıcı adı kayıtlı");
                }
                if (user.Email==data.EMail)
                {
                    lr.AddError(ErrorMessageCode.EmailAlreadyExist, "Email kayıtlı");
                   
                }
                //throw new Exception("Kayıtlı kullanıcı veya eposta adresi");
            }
            else
            {
                int dbResult = ruser.Insert(new NotlarimUser()
                {
                    Name=data.Name,
                    Surname=data.SurName,
                    Username = data.UserName,
                    Email = data.EMail,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    //repositorye taşındı
                    //ModifiedOn = DateTime.Now,
                    //CreatedOn = DateTime.Now,
                    //ModifiedUsername = "system"
                });
                if (dbResult>0)
                {
                    lr.Result = ruser.Find(s => s.Email == data.EMail && s.Username == data.UserName);
                    //activasyon maili atılacak
                    //lr.Result.ActivateGuid;
                }
            }
            return lr;
        }

        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //giriş kontrolü
            //hesap aktif edilmiş mi kontrolü
            //yönlendirme
            //session a kullanıcı bilgileri aktarma burda yaomicaz home controllerde
            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(s => s.Username == data.UserName && s.Password == data.Password);
            if (res.Result!=null)     
            {
                if (!res.Result.IsActive) //false olanı döndürür
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiş !!");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "lütfen mailinizi kontrol ediniz");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPasswordWrong, "Kullanıcı adı ve ya şifre yanlış");
            }
            return res;
        }
    }
}
