using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;
using HarmonyLib;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace ASCIIMod.HarmonyPatches
{
	[HarmonyPatch(typeof(XRL.World.Parts.ActivatedAbilities.XmlData))]
	public class HandleUITileNodePatch
	{
		[HarmonyPrefix]
		[HarmonyPatch("HandleUITileNode")]
		static bool Prefix(XmlDataHelper xml) {
			ActivatedAbilities.XmlData.UITileStates key = xml.ParseAttribute("State", ActivatedAbilities.XmlData.UITileStates.Default, required: false, ActivatedAbilities.XmlData.ParseTileState);
			Renderable value = Renderable.UITile(xml.ParseAttribute("Tile", "", required: true), xml.ParseAttribute("Foreground", 'r'), xml.ParseAttribute("Detail", 'W'), xml.ParseAttribute("RenderString", ""), xml.ParseAttribute("TileColor", '\0'));
            ActivatedAbilities.XmlData.currentData.UITiles[key] = value;
			return false;	
		}
	}
}
