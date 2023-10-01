using StripeDemoApp.Data;
using StripeDemoApp.Models;
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

        private readonly AppDataContext db;

        public List<TenantInfo> GetAllTenants()
        {
            return db.Tenants.ToList();

        }

        public List<TenantInfoViewModel> GetAllTenantViewModel()
        {
            return db.Tenants.Select(t=> new TenantInfoViewModel {
                Email = t.Email,
                Id = t.Id,
                IsRegisterationCompleted = t.IsRegisterationCompleted,
                StripePaymentAccountId = t.StripePaymentInfo.AccountId,
                StripePaymentInfoId = t.StripePaymentInfoId,
                TenantName = t.TenantName,
                StripePaymentPublicKey = t.StripePaymentInfo.PublicKey,
                StripePaymentSecretKey = t.StripePaymentInfo.SecretKey
            }
            ).ToList();

        }

        public TenantInfo GetTenantInfo(int tenantId)
        {

            return GetAllTenants().Find(u => u.Id == tenantId);
        }



        public TenantInfoViewModel GetTenantInfoViewModel(int tenantId)
        {

            return db.Tenants.Select(t => new TenantInfoViewModel
            {
                Email = t.Email,
                Id = t.Id,
                IsRegisterationCompleted = t.IsRegisterationCompleted,
                StripePaymentAccountId = t.StripePaymentInfo.AccountId,
                StripePaymentInfoId = t.StripePaymentInfoId,
                TenantName = t.TenantName,
                StripePaymentPublicKey = t.StripePaymentInfo.PublicKey,
                StripePaymentSecretKey = t.StripePaymentInfo.SecretKey
            }).FirstOrDefault(t => t.Id == tenantId);
        }

        public bool CreateEditTenantInfo(TenantInfoViewModel tenantInfo)
        {
            TenantInfo t;
            if (tenantInfo.Id == 0)
                t = new TenantInfo();
            else
                t = db.Tenants.Include("StripePaymentInfo").FirstOrDefault(tn => tn.Id == tenantInfo.Id);

            t.Email = tenantInfo.Email;
            t.Id = tenantInfo.Id;
            t.IsRegisterationCompleted = tenantInfo.IsRegisterationCompleted;
            t.TenantName = tenantInfo.TenantName;
            
            if(t.StripePaymentInfoId == 0  && IsValidToSaveStripeInfo(tenantInfo)){
                //t.StripePaymentInfoId = 
                var s = new StripUserPaymentInfo();
                s.AccountId = tenantInfo.StripePaymentAccountId;
                s.PublicKey = tenantInfo.StripePaymentPublicKey;
                s.SecretKey = tenantInfo.StripePaymentSecretKey;
                t.StripePaymentInfoId = s.Id;
                t.StripePaymentInfo = s;
                t.IsRegisterationCompleted = true;
                db.StripePaymentInfo.Add(s);
                
            }
            else if (t.StripePaymentInfoId > 0 && IsValidToSaveStripeInfo(tenantInfo))
            {
                var s = t.StripePaymentInfo;
                s.AccountId = tenantInfo.StripePaymentAccountId;
                s.PublicKey = tenantInfo.StripePaymentPublicKey;
                s.SecretKey = tenantInfo.StripePaymentSecretKey;
                //t.StripePaymentInfoId = s.Id;
                //db.StripePaymentInfo.(s);
                t.IsRegisterationCompleted = true;
            }

            if (t.Id == 0)
                db.Tenants.Add(t);

            db.SaveChanges();
            return t.IsRegisterationCompleted;

        }



        private bool IsValidToSaveStripeInfo(TenantInfoViewModel viewModel)
        {
            return (!string.IsNullOrWhiteSpace(viewModel.StripePaymentPublicKey)
                && !string.IsNullOrWhiteSpace(viewModel.StripePaymentSecretKey))
                || !string.IsNullOrWhiteSpace(viewModel.StripePaymentAccountId);
        }


    }









}