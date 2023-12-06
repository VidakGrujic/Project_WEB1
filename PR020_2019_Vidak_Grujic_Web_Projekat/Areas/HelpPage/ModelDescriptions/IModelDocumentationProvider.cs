using System;
using System.Reflection;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}