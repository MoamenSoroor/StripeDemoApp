using StripeDemoApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace StripeDemoApp.Services
{
    public class TenantService
    {

        public TenantService(AppDataContext db)
        {
            this.db = db;
        }

        //static int CurrentTenantId = Configurations.GetCurrentTenantId();
        private readonly AppDataContext db;

        //public TenantInfo SetCurrentTenant(int id)
        //{
        //    var user = GetAllTenants().Find(u => u.Id == id);
        //    if (user!= null)
        //    {
        //        CurrentTenantId = id;
        //        return user;
        //    }
        //    return GetCurrentTenant();
        //}

        //public TenantInfo GetCurrentTenant()
        //{
        //    var tenantId = Configurations.GetCurrentTenantId();

        //    return GetAllTenants().Find(u => u.Id == tenantId);
        //}

        public List<TenantInfo> GetAllTenants()
        {
            return db.Tenants.ToList();
            //return new List<TenantInfo>(){
            //    new TenantInfo()
            //    {
            //        Email = "eng.ahmed@gmail.com",
            //        Id = 1,
            //        TenantName = "eng. Ahmed",
            //        StripePaymentInfoId = 1,
            //        StripePaymentInfo = new StripUserPaymentInfo()
            //        {
            //            Id=1,
            //            PublicKey = "pk_test_51NrBSvJIhi7Tsh1qCE23s8BlErKrKuQ5ryxuBWb4pg9fxMdI2wk793PF2nbZ7z02CKlolnWAiuXfVOW4epgDbouz00BiYA9gCF",
            //            SecretKey = "sk_test_51NrBSvJIhi7Tsh1qMbm6qItfYSLEJVPk9EY7jb0pwsmrOHX0WKN9739TcjNKFvAqNNo0GHyDbwiHNtgMIrBwJoa1002hC2EIcM",
            //        }
            //    },
            //        new TenantInfo()
            //    {
            //        Email = "moa2@gmail.com",
            //        Id = 2,
            //        TenantName = "moamen soroor 2",
            //        StripePaymentInfoId = 2,
            //        StripePaymentInfo = new StripUserPaymentInfo()
            //        {
            //            Id=2,
            //            PublicKey = "pk_test_51NtTmKF2NqG2MwCbKmavZ1PKnSVUFTiZjqvDGi4PHqd38ZxhweYRzl3WYRxL5O4Cw8DPeSkqDJ2HHpTPWp6G6yTO00XclsLC28",
            //            SecretKey = "sk_test_51NtTmKF2NqG2MwCbZ61OSe6FzRdxrdtQSAYWK9XNCTPE2U2Gz8zxrL6SEHXWDnq3t6uhUMXUJCPJs917A0mun3w900DjFwo9Yv",
            //        }
            //    }
            //};

        }

        public TenantInfo GetTenantInfo(int tenantId)
        {

            return GetAllTenants().Find(u => u.Id == tenantId);
        }




    }









}