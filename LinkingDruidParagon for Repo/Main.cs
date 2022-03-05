using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Mods;
using Assets.Scripts.Unity.UI_New.Popups;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

[assembly: MelonInfo(typeof(LinkingDruidParagon.LinkingDruidParagonMain), "LinkingDruidParagon", "1.1.0", "Burrnr")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace LinkingDruidParagon
{
	public class LinkingDruidParagonMain : BloonsTD6Mod
    {
		// Github API URL used to check if this mod is up to date. For example:
		// public override string GithubReleaseURL => "https://api.github.com/repos/gurrenm3/BTD-Mod-Helper/releases";

		// As an alternative to a GithubReleaseURL, a direct link to a web-hosted version of the .cs file
		// that has the "MelonInfo" attribute with the version of your mod
		public override string MelonInfoCsURL => "https://raw.githubusercontent.com/Burrnr/BTD6-Mods/main/LinkingDruidParagon%20for%20Repo/Main.cs";

		// The link to your normal GitHub Releases page if you're using those, or a direct link to your dll file
		 public override string LatestURL => "https://github.com/Burrnr/BloonsTD6-Mods/blob/main/LinkingDruidParagon%20for%20Repo/LinkingDruidParagon.dll";


		

		
		
		



	}
}
