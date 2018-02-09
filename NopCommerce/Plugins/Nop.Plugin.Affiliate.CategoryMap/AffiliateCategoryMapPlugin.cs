using Nop.Core.Data;
using Nop.Core.Plugins;
using Nop.Plugin.Affiliate.CategoryMap;
using Nop.Plugin.Affiliate.CategoryMap.Data;
using Nop.Plugin.Affiliate.CategoryMap.Domain;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Affiliate.CategoryMap
{
    public class AffiliateCategoryMapPlugin : BasePlugin
    {
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private CategoryMappingObjectContext _context;

        public AffiliateCategoryMapPlugin(ISettingService settingService, CategoryMappingObjectContext context, ILocalizationService localizationService)
        {
            this._settingService = settingService;
            this._localizationService = localizationService;
            this._context = context;
        }

       
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            _context.Install();
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Affiliate", "Affiliates");

            var categorySettings = new ProductMappingSettings
            {
                AdditionalCostPercent = 5
            };
            _settingService.SaveSetting(categorySettings);

            base.Install();
        }

        

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            _context.Uninstall();
            this.DeletePluginLocaleResource("Nop.Plugin.Affiliate");

            _settingService.DeleteSetting<ProductMappingSettings>();
            base.Uninstall();
        }
    }
}
