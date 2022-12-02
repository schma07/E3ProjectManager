using Schma.E3ProjectManager.Core.Domain;

namespace Schma.E3ProjectManager.Core.Application
{
    /// <summary>
    /// Provides various localization keys for resolving to localized resources
    /// </summary>
    public interface ILocalizationKeyProvider
    {
        string GetRoleLocalizationKey(RoleEnum role);
    }
}
