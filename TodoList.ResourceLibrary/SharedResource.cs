using Microsoft.Extensions.Localization;
using System;

namespace TodoList.ResourceLibrary
{
    public class SharedResource
    {
        #region Fields

        private readonly IStringLocalizer _localizer;

        #endregion

        #region Constructor

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        #endregion

        #region Properties

        public string this[string key]
        {
            get
            {
                return _localizer[key];
            }
        }

        #endregion
    }
}
