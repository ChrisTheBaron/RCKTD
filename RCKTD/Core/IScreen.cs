using System.Collections.Generic;

namespace RCKTD.Core
{

    interface IScreen : IUpdatable, IDrawable, IControllable
    {

        /**
         * Called *before* the screen is shown
         */
        void ProcessShow(Dictionary<string, object> data);

        /**
         * Called *after* the screen is hidden
         */
        void ProcessHide();

    }

}
