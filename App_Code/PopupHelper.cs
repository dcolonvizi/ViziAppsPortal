using System;

    /// <summary>
    /// Summary description for PopupHelper.
    /// </summary>
    public class PopupHelper
    {
        /// <summary>
        /// Private constructor since class exposes only static methods
        /// </summary>
        private PopupHelper() { }

        /// <summary>
        /// Create a new popup window with the given name and size.
        /// </summary>
        /// <param name="url">The URL of the new window.</param>
        /// <param name="name">The name of the new window.</param>
        /// <param name="height">The height of the new window.</param>
        /// <param name="width">The width of the new window.</param>
        /// <returns>A string containing the necessary javascript
        /// enclosed in script tags.</returns>
        public static string GeneratePopupScript(string url,int height, int width)
        {
            return GeneratePopupScript(url,
                 height, width, true);
        }

        /// <summary>
        /// Create a new popup window with the given name and size.
        /// </summary>
        /// <param name="url">The URL of the new window.</param>
        /// <param name="name">The name of the new window.</param>
        /// <param name="height">The height of the new window.</param>
        /// <param name="width">The width of the new window.</param>
        /// <returns>A string containing the necessary javascript
        /// enclosed in script tags.</returns>
        public static string GeneratePopupScript(string url,int height, int width, bool includeReturn)
        {
            return string.Format(
                "PopUp(\"{0}\", \"height={1}, width={2}, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes\");{3}",
                url,
                height.ToString(),
                width.ToString(),
                includeReturn ? "return false;" : string.Empty);
        }

        public static string GeneratePopupScript(string url,
            int height, int width,  bool doMenu, bool doStatus, bool doToolbar,bool includeReturn)
        {
            string menubar = doMenu ? "yes" : "no";
            string status = doStatus ? "yes" : "no";
            string toolbar = doToolbar ? "yes" : "no";

            return string.Format(
                "PopUp(\"{0}\", \"height={1}, width={2}, left=200, top=200, menubar={3}, status={4}, location=no, toolbar={5}, scrollbars=yes, resizable=yes\");{6}",
                url,
                height.ToString(),
                width.ToString(),
                menubar,
                status,
                toolbar,
                includeReturn ? "return false;" : string.Empty);
        }

        public static string GeneratePopupScript(string url,
           int height, int width, bool doMenu, bool doStatus, bool doToolbar, bool doScrollbars, bool doResizable,bool includeReturn)
        {
            string menubar = doMenu ? "yes" : "no";
            string status = doStatus ? "yes" : "no";
            string toolbar = doToolbar ? "yes" : "no";
            string scrollbars = doScrollbars ? "yes" : "no";
            string resizable = doResizable ? "yes" : "no";

            return string.Format(
                "PopUp(\"{0}\", \"height={1}, width={2}, left=200, top=200, menubar={3}, status={4}, location=no, toolbar={5}, scrollbars={6}, resizable={7}\");{8}",
                url,
                height.ToString(),
                width.ToString(),
                menubar,
                status,
                toolbar,
                scrollbars,
                resizable,
                includeReturn ? "return false;" : string.Empty);
        }


        /// <summary>
        /// Generates javascript to close the current popup.
        /// </summary>
        /// <returns></returns>
        public static string GenerateClosePopupScript()
        {

            return "window.close();";
        }

        /// <summary>
        /// Generates javascript to close the current popup
        /// and reload the first form in the opening page.
        /// </summary>
        /// <param name="resubmitForm">Boolean to indicate whether
        /// or not the parent page should be resubmitted.</param>
        /// <returns>A string containing the javascript</returns>
        public static string GenerateClosePopupScript(bool resubmitForm)
        {

            if (!resubmitForm)
            {
                return PopupHelper.GenerateClosePopupScript();
            }
            else
            {
                return PopupHelper.GenerateClosePopupScript() +
                    "opener.document.forms[0].submit();";
            }
        }
    }
