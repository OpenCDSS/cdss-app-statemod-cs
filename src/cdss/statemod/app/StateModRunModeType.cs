using System;
using System.Collections.Generic;
using System.Text;

namespace cdss.statemod.app
{
    public class StateModRunModeType
    {

        /*
         * The name that should be displayed when the best fit type is used in UIs and reports,
         * typically an abbreviation or short name without spaces.
         */
        private String displayName;

        /**
         * Lowercase name, useful in some cases.
         */
        private String lowercaseName;

        /**
         * Definition of the run mode, for use in tool tips, etc.
         */
        private String definition;

        private StateModRunModeType(string displayName, string lowercase, string definition) { DisplayName = displayName; LowerCase = lowercase; Definition = definition; }

        private string DisplayName { get; set; }
        private string LowerCase { get; set; }
        private string Definition { get; set; }

        public static StateModRunModeType BASEFLOWS { get { return new StateModRunModeType("baseflow", "Baseflow", "Baseflow mode"); } }
        public static StateModRunModeType CHECK { get { return new StateModRunModeType("check", "Check", "Data check mode."); } }
        public static StateModRunModeType SIMULATE { get { return new StateModRunModeType("simulate", "Simulate", "Simulate with standard options."); } }

        /**
         * Return the display name.
         */
        public String getDisplayName()
        {
            return this.DisplayName;
        }

        /**
        * Return the definition.
        */
        public String getDefinition()
        {
            return this.Definition;
        }

        /**
         * Return the lowercase name.
         */
        public String getLowercaseName()
        {
            return this.LowerCase;
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("Run Mode(Display Name): " + this.DisplayName);
            return buffer.ToString();
        }

        /**
 	    * Return the enumeration value given a string name (case-independent).
 	    * @return the enumeration value given a string name (case-independent), or null if not matched.
 	    */
        //public static StateModRunModeType valueOfIgnoreCase(String name)
        //{
        //    if (name == null)
        //    {
        //        return null;
        //    }
        //    StateModRunModeType[] values = values();
        //    // Currently supported values
        //    for (StateModRunModeType t : values)
        //    {
        //        if (name.equalsIgnoreCase(t.toString()))
        //        {
        //            return t;
        //        }
        //    }
        //    return null;
        //}

    }
}
