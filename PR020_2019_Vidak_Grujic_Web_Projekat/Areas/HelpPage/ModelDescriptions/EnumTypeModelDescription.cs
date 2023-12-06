using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}