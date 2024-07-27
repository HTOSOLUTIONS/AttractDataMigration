using HTOTools.ActionHistoryActionFilter.AbstractClasses;

namespace DataMigration.Services.HTOTools.Implementations
{
    public class AddHistory : BaseAddHistory
    {

        protected override string getDefaultSessionSetting()
        {
            //return _actionExecutingContext.HttpContext.Session.GetAccountSetting(_sessionsetting);
            return null;
        }

        public AddHistory()
        {

        }

        public AddHistory(int useReferrerAction) : base(useReferrerAction)
        {
        }


        /// <summary>
        /// Use this Constructor to add a Description to the action that will be 
        /// added to the stack.  The Description can be used as the text in the return link.
        /// </summary>
        /// <param name="description"></param>
        public AddHistory(string description) : base(description)
        {
        }

        public AddHistory(string description, string classname) : base(description, classname)
        {
        }

        public AddHistory(string description, string classname, string actionName) : base(description, classname, actionName)
        {
        }

        public AddHistory(string description, string classname, int useReferrerAction) : base(description, classname, useReferrerAction)
        {
        }

        public AddHistory(string description, string classname, bool saveRecentforAdmin) : base(description, classname, saveRecentforAdmin)
        {
        }

        public AddHistory(string description, string classname, string settingname, string defaultval) : base(description, classname, settingname, defaultval)
        {
        }

        public AddHistory(string description, string classname, string settingname, string defaultval, int useReferrerAction) : base(description, classname, settingname, defaultval, useReferrerAction)
        {
        }

        public AddHistory(string description, string classname, string settingname, string defaultval, string actionName) : base(description, classname, settingname, defaultval, actionName)
        {
        }

    }
}
