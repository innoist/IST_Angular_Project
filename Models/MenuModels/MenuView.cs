using System.Collections.Generic;

namespace IST.Models.MenuModels
{
    /// <summary>
    /// Menu to show on client side
    /// </summary>
    public class MenuView
    {
        // ReSharper disable InconsistentNaming
        
        /// <summary>
        /// Title
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Heading - True if it is parent
        /// </summary>
        public bool? heading { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        public string sref { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// Sub Menu List
        /// </summary>
        public List<MenuView> submenu { get; set; }

        // ReSharper restore InconsistentNaming
    }
}
