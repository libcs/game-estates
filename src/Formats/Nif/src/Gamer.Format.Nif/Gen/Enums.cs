/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs.py Python script.

using System;

namespace Niflib {

/*! Describes how the vertex colors are blended with the filtered texture color. */
public enum ApplyMode : uint {
	APPLY_REPLACE = 0, /*!< Replaces existing color */
	APPLY_DECAL = 1, /*!< For placing images on the object like stickers. */
	APPLY_MODULATE = 2, /*!< Modulates existing color. (Default) */
	APPLY_HILIGHT = 3, /*!< PS2 Only.  Function Unknown. */
	APPLY_HILIGHT2 = 4, /*!< Parallax Flag in some Oblivion meshes. */
}
static partial class Nif { //--ApplyMode--//
public static void NifStream(out ApplyMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (ApplyMode)temp; }
public static void NifStream(ApplyMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(ApplyMode val) { switch (val) {
	case ApplyMode.APPLY_REPLACE: return "APPLY_REPLACE";
	case ApplyMode.APPLY_DECAL: return "APPLY_DECAL";
	case ApplyMode.APPLY_MODULATE: return "APPLY_MODULATE";
	case ApplyMode.APPLY_HILIGHT: return "APPLY_HILIGHT";
	case ApplyMode.APPLY_HILIGHT2: return "APPLY_HILIGHT2";
	default: return $"Invalid Value! - {val}";
}}}

/*! The type of texture. */
public enum TexType : uint {
	BASE_MAP = 0, /*!< The basic texture used by most meshes. */
	DARK_MAP = 1, /*!< Used to darken the model with false lighting. */
	DETAIL_MAP = 2, /*!< Combined with base map for added detail.  Usually tiled over the mesh many times for close-up view. */
	GLOSS_MAP = 3, /*!< Allows the specularity (glossyness) of an object to differ across its surface. */
	GLOW_MAP = 4, /*!< Creates a glowing effect.  Basically an incandescence map. */
	BUMP_MAP = 5, /*!< Used to make the object appear to have more detail than it really does. */
	NORMAL_MAP = 6, /*!< Used to make the object appear to have more detail than it really does. */
	PARALLAX_MAP = 7, /*!< Parallax map. */
	DECAL_0_MAP = 8, /*!< For placing images on the object like stickers. */
	DECAL_1_MAP = 9, /*!< For placing images on the object like stickers. */
	DECAL_2_MAP = 10, /*!< For placing images on the object like stickers. */
	DECAL_3_MAP = 11, /*!< For placing images on the object like stickers. */
}
static partial class Nif { //--TexType--//
public static void NifStream(out TexType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (TexType)temp; }
public static void NifStream(TexType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(TexType val) { switch (val) {
	case TexType.BASE_MAP: return "BASE_MAP";
	case TexType.DARK_MAP: return "DARK_MAP";
	case TexType.DETAIL_MAP: return "DETAIL_MAP";
	case TexType.GLOSS_MAP: return "GLOSS_MAP";
	case TexType.GLOW_MAP: return "GLOW_MAP";
	case TexType.BUMP_MAP: return "BUMP_MAP";
	case TexType.NORMAL_MAP: return "NORMAL_MAP";
	case TexType.PARALLAX_MAP: return "PARALLAX_MAP";
	case TexType.DECAL_0_MAP: return "DECAL_0_MAP";
	case TexType.DECAL_1_MAP: return "DECAL_1_MAP";
	case TexType.DECAL_2_MAP: return "DECAL_2_MAP";
	case TexType.DECAL_3_MAP: return "DECAL_3_MAP";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * The type of animation interpolation (blending) that will be used on the
 * associated key frames.
 */
public enum KeyType : uint {
	LINEAR_KEY = 1, /*!< Use linear interpolation. */
	QUADRATIC_KEY = 2, /*!< Use quadratic interpolation.  Forward and back tangents will be stored. */
	TBC_KEY = 3, /*!< Use Tension Bias Continuity interpolation.  Tension, bias, and continuity will be stored. */
	XYZ_ROTATION_KEY = 4, /*!< For use only with rotation data.  Separate X, Y, and Z keys will be stored instead of using quaternions. */
	CONST_KEY = 5, /*!< Step function. Used for visibility keys in NiBoolData. */
}
static partial class Nif { //--KeyType--//
public static void NifStream(out KeyType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (KeyType)temp; }
public static void NifStream(KeyType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(KeyType val) { switch (val) {
	case KeyType.LINEAR_KEY: return "LINEAR_KEY";
	case KeyType.QUADRATIC_KEY: return "QUADRATIC_KEY";
	case KeyType.TBC_KEY: return "TBC_KEY";
	case KeyType.XYZ_ROTATION_KEY: return "XYZ_ROTATION_KEY";
	case KeyType.CONST_KEY: return "CONST_KEY";
	default: return $"Invalid Value! - {val}";
}}}

/*! Bethesda Havok. Material descriptor for a Havok shape in Oblivion. */
public enum OblivionHavokMaterial : uint {
	OB_HAV_MAT_STONE = 0, /*!< Stone */
	OB_HAV_MAT_CLOTH = 1, /*!< Cloth */
	OB_HAV_MAT_DIRT = 2, /*!< Dirt */
	OB_HAV_MAT_GLASS = 3, /*!< Glass */
	OB_HAV_MAT_GRASS = 4, /*!< Grass */
	OB_HAV_MAT_METAL = 5, /*!< Metal */
	OB_HAV_MAT_ORGANIC = 6, /*!< Organic */
	OB_HAV_MAT_SKIN = 7, /*!< Skin */
	OB_HAV_MAT_WATER = 8, /*!< Water */
	OB_HAV_MAT_WOOD = 9, /*!< Wood */
	OB_HAV_MAT_HEAVY_STONE = 10, /*!< Heavy Stone */
	OB_HAV_MAT_HEAVY_METAL = 11, /*!< Heavy Metal */
	OB_HAV_MAT_HEAVY_WOOD = 12, /*!< Heavy Wood */
	OB_HAV_MAT_CHAIN = 13, /*!< Chain */
	OB_HAV_MAT_SNOW = 14, /*!< Snow */
	OB_HAV_MAT_STONE_STAIRS = 15, /*!< Stone Stairs */
	OB_HAV_MAT_CLOTH_STAIRS = 16, /*!< Cloth Stairs */
	OB_HAV_MAT_DIRT_STAIRS = 17, /*!< Dirt Stairs */
	OB_HAV_MAT_GLASS_STAIRS = 18, /*!< Glass Stairs */
	OB_HAV_MAT_GRASS_STAIRS = 19, /*!< Grass Stairs */
	OB_HAV_MAT_METAL_STAIRS = 20, /*!< Metal Stairs */
	OB_HAV_MAT_ORGANIC_STAIRS = 21, /*!< Organic Stairs */
	OB_HAV_MAT_SKIN_STAIRS = 22, /*!< Skin Stairs */
	OB_HAV_MAT_WATER_STAIRS = 23, /*!< Water Stairs */
	OB_HAV_MAT_WOOD_STAIRS = 24, /*!< Wood Stairs */
	OB_HAV_MAT_HEAVY_STONE_STAIRS = 25, /*!< Heavy Stone Stairs */
	OB_HAV_MAT_HEAVY_METAL_STAIRS = 26, /*!< Heavy Metal Stairs */
	OB_HAV_MAT_HEAVY_WOOD_STAIRS = 27, /*!< Heavy Wood Stairs */
	OB_HAV_MAT_CHAIN_STAIRS = 28, /*!< Chain Stairs */
	OB_HAV_MAT_SNOW_STAIRS = 29, /*!< Snow Stairs */
	OB_HAV_MAT_ELEVATOR = 30, /*!< Elevator */
	OB_HAV_MAT_RUBBER = 31, /*!< Rubber */
}
static partial class Nif { //--OblivionHavokMaterial--//
public static void NifStream(out OblivionHavokMaterial val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (OblivionHavokMaterial)temp; }
public static void NifStream(OblivionHavokMaterial val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(OblivionHavokMaterial val) { switch (val) {
	case OblivionHavokMaterial.OB_HAV_MAT_STONE: return "OB_HAV_MAT_STONE";
	case OblivionHavokMaterial.OB_HAV_MAT_CLOTH: return "OB_HAV_MAT_CLOTH";
	case OblivionHavokMaterial.OB_HAV_MAT_DIRT: return "OB_HAV_MAT_DIRT";
	case OblivionHavokMaterial.OB_HAV_MAT_GLASS: return "OB_HAV_MAT_GLASS";
	case OblivionHavokMaterial.OB_HAV_MAT_GRASS: return "OB_HAV_MAT_GRASS";
	case OblivionHavokMaterial.OB_HAV_MAT_METAL: return "OB_HAV_MAT_METAL";
	case OblivionHavokMaterial.OB_HAV_MAT_ORGANIC: return "OB_HAV_MAT_ORGANIC";
	case OblivionHavokMaterial.OB_HAV_MAT_SKIN: return "OB_HAV_MAT_SKIN";
	case OblivionHavokMaterial.OB_HAV_MAT_WATER: return "OB_HAV_MAT_WATER";
	case OblivionHavokMaterial.OB_HAV_MAT_WOOD: return "OB_HAV_MAT_WOOD";
	case OblivionHavokMaterial.OB_HAV_MAT_HEAVY_STONE: return "OB_HAV_MAT_HEAVY_STONE";
	case OblivionHavokMaterial.OB_HAV_MAT_HEAVY_METAL: return "OB_HAV_MAT_HEAVY_METAL";
	case OblivionHavokMaterial.OB_HAV_MAT_HEAVY_WOOD: return "OB_HAV_MAT_HEAVY_WOOD";
	case OblivionHavokMaterial.OB_HAV_MAT_CHAIN: return "OB_HAV_MAT_CHAIN";
	case OblivionHavokMaterial.OB_HAV_MAT_SNOW: return "OB_HAV_MAT_SNOW";
	case OblivionHavokMaterial.OB_HAV_MAT_STONE_STAIRS: return "OB_HAV_MAT_STONE_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_CLOTH_STAIRS: return "OB_HAV_MAT_CLOTH_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_DIRT_STAIRS: return "OB_HAV_MAT_DIRT_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_GLASS_STAIRS: return "OB_HAV_MAT_GLASS_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_GRASS_STAIRS: return "OB_HAV_MAT_GRASS_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_METAL_STAIRS: return "OB_HAV_MAT_METAL_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_ORGANIC_STAIRS: return "OB_HAV_MAT_ORGANIC_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_SKIN_STAIRS: return "OB_HAV_MAT_SKIN_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_WATER_STAIRS: return "OB_HAV_MAT_WATER_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_WOOD_STAIRS: return "OB_HAV_MAT_WOOD_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_HEAVY_STONE_STAIRS: return "OB_HAV_MAT_HEAVY_STONE_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_HEAVY_METAL_STAIRS: return "OB_HAV_MAT_HEAVY_METAL_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_HEAVY_WOOD_STAIRS: return "OB_HAV_MAT_HEAVY_WOOD_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_CHAIN_STAIRS: return "OB_HAV_MAT_CHAIN_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_SNOW_STAIRS: return "OB_HAV_MAT_SNOW_STAIRS";
	case OblivionHavokMaterial.OB_HAV_MAT_ELEVATOR: return "OB_HAV_MAT_ELEVATOR";
	case OblivionHavokMaterial.OB_HAV_MAT_RUBBER: return "OB_HAV_MAT_RUBBER";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Havok. Material descriptor for a Havok shape in Fallout 3 and Fallout
 * NV.
 */
public enum Fallout3HavokMaterial : uint {
	FO_HAV_MAT_STONE = 0, /*!< Stone */
	FO_HAV_MAT_CLOTH = 1, /*!< Cloth */
	FO_HAV_MAT_DIRT = 2, /*!< Dirt */
	FO_HAV_MAT_GLASS = 3, /*!< Glass */
	FO_HAV_MAT_GRASS = 4, /*!< Grass */
	FO_HAV_MAT_METAL = 5, /*!< Metal */
	FO_HAV_MAT_ORGANIC = 6, /*!< Organic */
	FO_HAV_MAT_SKIN = 7, /*!< Skin */
	FO_HAV_MAT_WATER = 8, /*!< Water */
	FO_HAV_MAT_WOOD = 9, /*!< Wood */
	FO_HAV_MAT_HEAVY_STONE = 10, /*!< Heavy Stone */
	FO_HAV_MAT_HEAVY_METAL = 11, /*!< Heavy Metal */
	FO_HAV_MAT_HEAVY_WOOD = 12, /*!< Heavy Wood */
	FO_HAV_MAT_CHAIN = 13, /*!< Chain */
	FO_HAV_MAT_BOTTLECAP = 14, /*!< Bottlecap */
	FO_HAV_MAT_ELEVATOR = 15, /*!< Elevator */
	FO_HAV_MAT_HOLLOW_METAL = 16, /*!< Hollow Metal */
	FO_HAV_MAT_SHEET_METAL = 17, /*!< Sheet Metal */
	FO_HAV_MAT_SAND = 18, /*!< Sand */
	FO_HAV_MAT_BROKEN_CONCRETE = 19, /*!< Broken Concrete */
	FO_HAV_MAT_VEHICLE_BODY = 20, /*!< Vehicle Body */
	FO_HAV_MAT_VEHICLE_PART_SOLID = 21, /*!< Vehicle Part Solid */
	FO_HAV_MAT_VEHICLE_PART_HOLLOW = 22, /*!< Vehicle Part Hollow */
	FO_HAV_MAT_BARREL = 23, /*!< Barrel */
	FO_HAV_MAT_BOTTLE = 24, /*!< Bottle */
	FO_HAV_MAT_SODA_CAN = 25, /*!< Soda Can */
	FO_HAV_MAT_PISTOL = 26, /*!< Pistol */
	FO_HAV_MAT_RIFLE = 27, /*!< Rifle */
	FO_HAV_MAT_SHOPPING_CART = 28, /*!< Shopping Cart */
	FO_HAV_MAT_LUNCHBOX = 29, /*!< Lunchbox */
	FO_HAV_MAT_BABY_RATTLE = 30, /*!< Baby Rattle */
	FO_HAV_MAT_RUBBER_BALL = 31, /*!< Rubber Ball */
	FO_HAV_MAT_STONE_PLATFORM = 32, /*!< Stone */
	FO_HAV_MAT_CLOTH_PLATFORM = 33, /*!< Cloth */
	FO_HAV_MAT_DIRT_PLATFORM = 34, /*!< Dirt */
	FO_HAV_MAT_GLASS_PLATFORM = 35, /*!< Glass */
	FO_HAV_MAT_GRASS_PLATFORM = 36, /*!< Grass */
	FO_HAV_MAT_METAL_PLATFORM = 37, /*!< Metal */
	FO_HAV_MAT_ORGANIC_PLATFORM = 38, /*!< Organic */
	FO_HAV_MAT_SKIN_PLATFORM = 39, /*!< Skin */
	FO_HAV_MAT_WATER_PLATFORM = 40, /*!< Water */
	FO_HAV_MAT_WOOD_PLATFORM = 41, /*!< Wood */
	FO_HAV_MAT_HEAVY_STONE_PLATFORM = 42, /*!< Heavy Stone */
	FO_HAV_MAT_HEAVY_METAL_PLATFORM = 43, /*!< Heavy Metal */
	FO_HAV_MAT_HEAVY_WOOD_PLATFORM = 44, /*!< Heavy Wood */
	FO_HAV_MAT_CHAIN_PLATFORM = 45, /*!< Chain */
	FO_HAV_MAT_BOTTLECAP_PLATFORM = 46, /*!< Bottlecap */
	FO_HAV_MAT_ELEVATOR_PLATFORM = 47, /*!< Elevator */
	FO_HAV_MAT_HOLLOW_METAL_PLATFORM = 48, /*!< Hollow Metal */
	FO_HAV_MAT_SHEET_METAL_PLATFORM = 49, /*!< Sheet Metal */
	FO_HAV_MAT_SAND_PLATFORM = 50, /*!< Sand */
	FO_HAV_MAT_BROKEN_CONCRETE_PLATFORM = 51, /*!< Broken Concrete */
	FO_HAV_MAT_VEHICLE_BODY_PLATFORM = 52, /*!< Vehicle Body */
	FO_HAV_MAT_VEHICLE_PART_SOLID_PLATFORM = 53, /*!< Vehicle Part Solid */
	FO_HAV_MAT_VEHICLE_PART_HOLLOW_PLATFORM = 54, /*!< Vehicle Part Hollow */
	FO_HAV_MAT_BARREL_PLATFORM = 55, /*!< Barrel */
	FO_HAV_MAT_BOTTLE_PLATFORM = 56, /*!< Bottle */
	FO_HAV_MAT_SODA_CAN_PLATFORM = 57, /*!< Soda Can */
	FO_HAV_MAT_PISTOL_PLATFORM = 58, /*!< Pistol */
	FO_HAV_MAT_RIFLE_PLATFORM = 59, /*!< Rifle */
	FO_HAV_MAT_SHOPPING_CART_PLATFORM = 60, /*!< Shopping Cart */
	FO_HAV_MAT_LUNCHBOX_PLATFORM = 61, /*!< Lunchbox */
	FO_HAV_MAT_BABY_RATTLE_PLATFORM = 62, /*!< Baby Rattle */
	FO_HAV_MAT_RUBBER_BALL_PLATFORM = 63, /*!< Rubber Ball */
	FO_HAV_MAT_STONE_STAIRS = 64, /*!< Stone */
	FO_HAV_MAT_CLOTH_STAIRS = 65, /*!< Cloth */
	FO_HAV_MAT_DIRT_STAIRS = 66, /*!< Dirt */
	FO_HAV_MAT_GLASS_STAIRS = 67, /*!< Glass */
	FO_HAV_MAT_GRASS_STAIRS = 68, /*!< Grass */
	FO_HAV_MAT_METAL_STAIRS = 69, /*!< Metal */
	FO_HAV_MAT_ORGANIC_STAIRS = 70, /*!< Organic */
	FO_HAV_MAT_SKIN_STAIRS = 71, /*!< Skin */
	FO_HAV_MAT_WATER_STAIRS = 72, /*!< Water */
	FO_HAV_MAT_WOOD_STAIRS = 73, /*!< Wood */
	FO_HAV_MAT_HEAVY_STONE_STAIRS = 74, /*!< Heavy Stone */
	FO_HAV_MAT_HEAVY_METAL_STAIRS = 75, /*!< Heavy Metal */
	FO_HAV_MAT_HEAVY_WOOD_STAIRS = 76, /*!< Heavy Wood */
	FO_HAV_MAT_CHAIN_STAIRS = 77, /*!< Chain */
	FO_HAV_MAT_BOTTLECAP_STAIRS = 78, /*!< Bottlecap */
	FO_HAV_MAT_ELEVATOR_STAIRS = 79, /*!< Elevator */
	FO_HAV_MAT_HOLLOW_METAL_STAIRS = 80, /*!< Hollow Metal */
	FO_HAV_MAT_SHEET_METAL_STAIRS = 81, /*!< Sheet Metal */
	FO_HAV_MAT_SAND_STAIRS = 82, /*!< Sand */
	FO_HAV_MAT_BROKEN_CONCRETE_STAIRS = 83, /*!< Broken Concrete */
	FO_HAV_MAT_VEHICLE_BODY_STAIRS = 84, /*!< Vehicle Body */
	FO_HAV_MAT_VEHICLE_PART_SOLID_STAIRS = 85, /*!< Vehicle Part Solid */
	FO_HAV_MAT_VEHICLE_PART_HOLLOW_STAIRS = 86, /*!< Vehicle Part Hollow */
	FO_HAV_MAT_BARREL_STAIRS = 87, /*!< Barrel */
	FO_HAV_MAT_BOTTLE_STAIRS = 88, /*!< Bottle */
	FO_HAV_MAT_SODA_CAN_STAIRS = 89, /*!< Soda Can */
	FO_HAV_MAT_PISTOL_STAIRS = 90, /*!< Pistol */
	FO_HAV_MAT_RIFLE_STAIRS = 91, /*!< Rifle */
	FO_HAV_MAT_SHOPPING_CART_STAIRS = 92, /*!< Shopping Cart */
	FO_HAV_MAT_LUNCHBOX_STAIRS = 93, /*!< Lunchbox */
	FO_HAV_MAT_BABY_RATTLE_STAIRS = 94, /*!< Baby Rattle */
	FO_HAV_MAT_RUBBER_BALL_STAIRS = 95, /*!< Rubber Ball */
	FO_HAV_MAT_STONE_STAIRS_PLATFORM = 96, /*!< Stone */
	FO_HAV_MAT_CLOTH_STAIRS_PLATFORM = 97, /*!< Cloth */
	FO_HAV_MAT_DIRT_STAIRS_PLATFORM = 98, /*!< Dirt */
	FO_HAV_MAT_GLASS_STAIRS_PLATFORM = 99, /*!< Glass */
	FO_HAV_MAT_GRASS_STAIRS_PLATFORM = 100, /*!< Grass */
	FO_HAV_MAT_METAL_STAIRS_PLATFORM = 101, /*!< Metal */
	FO_HAV_MAT_ORGANIC_STAIRS_PLATFORM = 102, /*!< Organic */
	FO_HAV_MAT_SKIN_STAIRS_PLATFORM = 103, /*!< Skin */
	FO_HAV_MAT_WATER_STAIRS_PLATFORM = 104, /*!< Water */
	FO_HAV_MAT_WOOD_STAIRS_PLATFORM = 105, /*!< Wood */
	FO_HAV_MAT_HEAVY_STONE_STAIRS_PLATFORM = 106, /*!< Heavy Stone */
	FO_HAV_MAT_HEAVY_METAL_STAIRS_PLATFORM = 107, /*!< Heavy Metal */
	FO_HAV_MAT_HEAVY_WOOD_STAIRS_PLATFORM = 108, /*!< Heavy Wood */
	FO_HAV_MAT_CHAIN_STAIRS_PLATFORM = 109, /*!< Chain */
	FO_HAV_MAT_BOTTLECAP_STAIRS_PLATFORM = 110, /*!< Bottlecap */
	FO_HAV_MAT_ELEVATOR_STAIRS_PLATFORM = 111, /*!< Elevator */
	FO_HAV_MAT_HOLLOW_METAL_STAIRS_PLATFORM = 112, /*!< Hollow Metal */
	FO_HAV_MAT_SHEET_METAL_STAIRS_PLATFORM = 113, /*!< Sheet Metal */
	FO_HAV_MAT_SAND_STAIRS_PLATFORM = 114, /*!< Sand */
	FO_HAV_MAT_BROKEN_CONCRETE_STAIRS_PLATFORM = 115, /*!< Broken Concrete */
	FO_HAV_MAT_VEHICLE_BODY_STAIRS_PLATFORM = 116, /*!< Vehicle Body */
	FO_HAV_MAT_VEHICLE_PART_SOLID_STAIRS_PLATFORM = 117, /*!< Vehicle Part Solid */
	FO_HAV_MAT_VEHICLE_PART_HOLLOW_STAIRS_PLATFORM = 118, /*!< Vehicle Part Hollow */
	FO_HAV_MAT_BARREL_STAIRS_PLATFORM = 119, /*!< Barrel */
	FO_HAV_MAT_BOTTLE_STAIRS_PLATFORM = 120, /*!< Bottle */
	FO_HAV_MAT_SODA_CAN_STAIRS_PLATFORM = 121, /*!< Soda Can */
	FO_HAV_MAT_PISTOL_STAIRS_PLATFORM = 122, /*!< Pistol */
	FO_HAV_MAT_RIFLE_STAIRS_PLATFORM = 123, /*!< Rifle */
	FO_HAV_MAT_SHOPPING_CART_STAIRS_PLATFORM = 124, /*!< Shopping Cart */
	FO_HAV_MAT_LUNCHBOX_STAIRS_PLATFORM = 125, /*!< Lunchbox */
	FO_HAV_MAT_BABY_RATTLE_STAIRS_PLATFORM = 126, /*!< Baby Rattle */
	FO_HAV_MAT_RUBBER_BALL_STAIRS_PLATFORM = 127, /*!< Rubber Ball */
}
static partial class Nif { //--Fallout3HavokMaterial--//
public static void NifStream(out Fallout3HavokMaterial val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (Fallout3HavokMaterial)temp; }
public static void NifStream(Fallout3HavokMaterial val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(Fallout3HavokMaterial val) { switch (val) {
	case Fallout3HavokMaterial.FO_HAV_MAT_STONE: return "FO_HAV_MAT_STONE";
	case Fallout3HavokMaterial.FO_HAV_MAT_CLOTH: return "FO_HAV_MAT_CLOTH";
	case Fallout3HavokMaterial.FO_HAV_MAT_DIRT: return "FO_HAV_MAT_DIRT";
	case Fallout3HavokMaterial.FO_HAV_MAT_GLASS: return "FO_HAV_MAT_GLASS";
	case Fallout3HavokMaterial.FO_HAV_MAT_GRASS: return "FO_HAV_MAT_GRASS";
	case Fallout3HavokMaterial.FO_HAV_MAT_METAL: return "FO_HAV_MAT_METAL";
	case Fallout3HavokMaterial.FO_HAV_MAT_ORGANIC: return "FO_HAV_MAT_ORGANIC";
	case Fallout3HavokMaterial.FO_HAV_MAT_SKIN: return "FO_HAV_MAT_SKIN";
	case Fallout3HavokMaterial.FO_HAV_MAT_WATER: return "FO_HAV_MAT_WATER";
	case Fallout3HavokMaterial.FO_HAV_MAT_WOOD: return "FO_HAV_MAT_WOOD";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_STONE: return "FO_HAV_MAT_HEAVY_STONE";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_METAL: return "FO_HAV_MAT_HEAVY_METAL";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_WOOD: return "FO_HAV_MAT_HEAVY_WOOD";
	case Fallout3HavokMaterial.FO_HAV_MAT_CHAIN: return "FO_HAV_MAT_CHAIN";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLECAP: return "FO_HAV_MAT_BOTTLECAP";
	case Fallout3HavokMaterial.FO_HAV_MAT_ELEVATOR: return "FO_HAV_MAT_ELEVATOR";
	case Fallout3HavokMaterial.FO_HAV_MAT_HOLLOW_METAL: return "FO_HAV_MAT_HOLLOW_METAL";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHEET_METAL: return "FO_HAV_MAT_SHEET_METAL";
	case Fallout3HavokMaterial.FO_HAV_MAT_SAND: return "FO_HAV_MAT_SAND";
	case Fallout3HavokMaterial.FO_HAV_MAT_BROKEN_CONCRETE: return "FO_HAV_MAT_BROKEN_CONCRETE";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_BODY: return "FO_HAV_MAT_VEHICLE_BODY";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_SOLID: return "FO_HAV_MAT_VEHICLE_PART_SOLID";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_HOLLOW: return "FO_HAV_MAT_VEHICLE_PART_HOLLOW";
	case Fallout3HavokMaterial.FO_HAV_MAT_BARREL: return "FO_HAV_MAT_BARREL";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLE: return "FO_HAV_MAT_BOTTLE";
	case Fallout3HavokMaterial.FO_HAV_MAT_SODA_CAN: return "FO_HAV_MAT_SODA_CAN";
	case Fallout3HavokMaterial.FO_HAV_MAT_PISTOL: return "FO_HAV_MAT_PISTOL";
	case Fallout3HavokMaterial.FO_HAV_MAT_RIFLE: return "FO_HAV_MAT_RIFLE";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHOPPING_CART: return "FO_HAV_MAT_SHOPPING_CART";
	case Fallout3HavokMaterial.FO_HAV_MAT_LUNCHBOX: return "FO_HAV_MAT_LUNCHBOX";
	case Fallout3HavokMaterial.FO_HAV_MAT_BABY_RATTLE: return "FO_HAV_MAT_BABY_RATTLE";
	case Fallout3HavokMaterial.FO_HAV_MAT_RUBBER_BALL: return "FO_HAV_MAT_RUBBER_BALL";
	case Fallout3HavokMaterial.FO_HAV_MAT_STONE_PLATFORM: return "FO_HAV_MAT_STONE_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_CLOTH_PLATFORM: return "FO_HAV_MAT_CLOTH_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_DIRT_PLATFORM: return "FO_HAV_MAT_DIRT_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_GLASS_PLATFORM: return "FO_HAV_MAT_GLASS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_GRASS_PLATFORM: return "FO_HAV_MAT_GRASS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_METAL_PLATFORM: return "FO_HAV_MAT_METAL_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_ORGANIC_PLATFORM: return "FO_HAV_MAT_ORGANIC_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SKIN_PLATFORM: return "FO_HAV_MAT_SKIN_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_WATER_PLATFORM: return "FO_HAV_MAT_WATER_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_WOOD_PLATFORM: return "FO_HAV_MAT_WOOD_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_STONE_PLATFORM: return "FO_HAV_MAT_HEAVY_STONE_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_METAL_PLATFORM: return "FO_HAV_MAT_HEAVY_METAL_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_WOOD_PLATFORM: return "FO_HAV_MAT_HEAVY_WOOD_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_CHAIN_PLATFORM: return "FO_HAV_MAT_CHAIN_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLECAP_PLATFORM: return "FO_HAV_MAT_BOTTLECAP_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_ELEVATOR_PLATFORM: return "FO_HAV_MAT_ELEVATOR_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HOLLOW_METAL_PLATFORM: return "FO_HAV_MAT_HOLLOW_METAL_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHEET_METAL_PLATFORM: return "FO_HAV_MAT_SHEET_METAL_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SAND_PLATFORM: return "FO_HAV_MAT_SAND_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BROKEN_CONCRETE_PLATFORM: return "FO_HAV_MAT_BROKEN_CONCRETE_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_BODY_PLATFORM: return "FO_HAV_MAT_VEHICLE_BODY_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_SOLID_PLATFORM: return "FO_HAV_MAT_VEHICLE_PART_SOLID_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_HOLLOW_PLATFORM: return "FO_HAV_MAT_VEHICLE_PART_HOLLOW_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BARREL_PLATFORM: return "FO_HAV_MAT_BARREL_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLE_PLATFORM: return "FO_HAV_MAT_BOTTLE_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SODA_CAN_PLATFORM: return "FO_HAV_MAT_SODA_CAN_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_PISTOL_PLATFORM: return "FO_HAV_MAT_PISTOL_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_RIFLE_PLATFORM: return "FO_HAV_MAT_RIFLE_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHOPPING_CART_PLATFORM: return "FO_HAV_MAT_SHOPPING_CART_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_LUNCHBOX_PLATFORM: return "FO_HAV_MAT_LUNCHBOX_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BABY_RATTLE_PLATFORM: return "FO_HAV_MAT_BABY_RATTLE_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_RUBBER_BALL_PLATFORM: return "FO_HAV_MAT_RUBBER_BALL_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_STONE_STAIRS: return "FO_HAV_MAT_STONE_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_CLOTH_STAIRS: return "FO_HAV_MAT_CLOTH_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_DIRT_STAIRS: return "FO_HAV_MAT_DIRT_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_GLASS_STAIRS: return "FO_HAV_MAT_GLASS_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_GRASS_STAIRS: return "FO_HAV_MAT_GRASS_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_METAL_STAIRS: return "FO_HAV_MAT_METAL_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_ORGANIC_STAIRS: return "FO_HAV_MAT_ORGANIC_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_SKIN_STAIRS: return "FO_HAV_MAT_SKIN_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_WATER_STAIRS: return "FO_HAV_MAT_WATER_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_WOOD_STAIRS: return "FO_HAV_MAT_WOOD_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_STONE_STAIRS: return "FO_HAV_MAT_HEAVY_STONE_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_METAL_STAIRS: return "FO_HAV_MAT_HEAVY_METAL_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_WOOD_STAIRS: return "FO_HAV_MAT_HEAVY_WOOD_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_CHAIN_STAIRS: return "FO_HAV_MAT_CHAIN_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLECAP_STAIRS: return "FO_HAV_MAT_BOTTLECAP_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_ELEVATOR_STAIRS: return "FO_HAV_MAT_ELEVATOR_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_HOLLOW_METAL_STAIRS: return "FO_HAV_MAT_HOLLOW_METAL_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHEET_METAL_STAIRS: return "FO_HAV_MAT_SHEET_METAL_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_SAND_STAIRS: return "FO_HAV_MAT_SAND_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_BROKEN_CONCRETE_STAIRS: return "FO_HAV_MAT_BROKEN_CONCRETE_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_BODY_STAIRS: return "FO_HAV_MAT_VEHICLE_BODY_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_SOLID_STAIRS: return "FO_HAV_MAT_VEHICLE_PART_SOLID_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_HOLLOW_STAIRS: return "FO_HAV_MAT_VEHICLE_PART_HOLLOW_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_BARREL_STAIRS: return "FO_HAV_MAT_BARREL_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLE_STAIRS: return "FO_HAV_MAT_BOTTLE_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_SODA_CAN_STAIRS: return "FO_HAV_MAT_SODA_CAN_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_PISTOL_STAIRS: return "FO_HAV_MAT_PISTOL_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_RIFLE_STAIRS: return "FO_HAV_MAT_RIFLE_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHOPPING_CART_STAIRS: return "FO_HAV_MAT_SHOPPING_CART_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_LUNCHBOX_STAIRS: return "FO_HAV_MAT_LUNCHBOX_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_BABY_RATTLE_STAIRS: return "FO_HAV_MAT_BABY_RATTLE_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_RUBBER_BALL_STAIRS: return "FO_HAV_MAT_RUBBER_BALL_STAIRS";
	case Fallout3HavokMaterial.FO_HAV_MAT_STONE_STAIRS_PLATFORM: return "FO_HAV_MAT_STONE_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_CLOTH_STAIRS_PLATFORM: return "FO_HAV_MAT_CLOTH_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_DIRT_STAIRS_PLATFORM: return "FO_HAV_MAT_DIRT_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_GLASS_STAIRS_PLATFORM: return "FO_HAV_MAT_GLASS_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_GRASS_STAIRS_PLATFORM: return "FO_HAV_MAT_GRASS_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_METAL_STAIRS_PLATFORM: return "FO_HAV_MAT_METAL_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_ORGANIC_STAIRS_PLATFORM: return "FO_HAV_MAT_ORGANIC_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SKIN_STAIRS_PLATFORM: return "FO_HAV_MAT_SKIN_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_WATER_STAIRS_PLATFORM: return "FO_HAV_MAT_WATER_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_WOOD_STAIRS_PLATFORM: return "FO_HAV_MAT_WOOD_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_STONE_STAIRS_PLATFORM: return "FO_HAV_MAT_HEAVY_STONE_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_METAL_STAIRS_PLATFORM: return "FO_HAV_MAT_HEAVY_METAL_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HEAVY_WOOD_STAIRS_PLATFORM: return "FO_HAV_MAT_HEAVY_WOOD_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_CHAIN_STAIRS_PLATFORM: return "FO_HAV_MAT_CHAIN_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLECAP_STAIRS_PLATFORM: return "FO_HAV_MAT_BOTTLECAP_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_ELEVATOR_STAIRS_PLATFORM: return "FO_HAV_MAT_ELEVATOR_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_HOLLOW_METAL_STAIRS_PLATFORM: return "FO_HAV_MAT_HOLLOW_METAL_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHEET_METAL_STAIRS_PLATFORM: return "FO_HAV_MAT_SHEET_METAL_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SAND_STAIRS_PLATFORM: return "FO_HAV_MAT_SAND_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BROKEN_CONCRETE_STAIRS_PLATFORM: return "FO_HAV_MAT_BROKEN_CONCRETE_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_BODY_STAIRS_PLATFORM: return "FO_HAV_MAT_VEHICLE_BODY_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_SOLID_STAIRS_PLATFORM: return "FO_HAV_MAT_VEHICLE_PART_SOLID_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_VEHICLE_PART_HOLLOW_STAIRS_PLATFORM: return "FO_HAV_MAT_VEHICLE_PART_HOLLOW_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BARREL_STAIRS_PLATFORM: return "FO_HAV_MAT_BARREL_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BOTTLE_STAIRS_PLATFORM: return "FO_HAV_MAT_BOTTLE_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SODA_CAN_STAIRS_PLATFORM: return "FO_HAV_MAT_SODA_CAN_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_PISTOL_STAIRS_PLATFORM: return "FO_HAV_MAT_PISTOL_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_RIFLE_STAIRS_PLATFORM: return "FO_HAV_MAT_RIFLE_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_SHOPPING_CART_STAIRS_PLATFORM: return "FO_HAV_MAT_SHOPPING_CART_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_LUNCHBOX_STAIRS_PLATFORM: return "FO_HAV_MAT_LUNCHBOX_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_BABY_RATTLE_STAIRS_PLATFORM: return "FO_HAV_MAT_BABY_RATTLE_STAIRS_PLATFORM";
	case Fallout3HavokMaterial.FO_HAV_MAT_RUBBER_BALL_STAIRS_PLATFORM: return "FO_HAV_MAT_RUBBER_BALL_STAIRS_PLATFORM";
	default: return $"Invalid Value! - {val}";
}}}

/*! Bethesda Havok. Material descriptor for a Havok shape in Skyrim. */
public enum SkyrimHavokMaterial : uint {
	SKY_HAV_MAT_BROKEN_STONE = 131151687, /*!< Broken Stone */
	SKY_HAV_MAT_LIGHT_WOOD = 365420259, /*!< Light Wood */
	SKY_HAV_MAT_SNOW = 398949039, /*!< Snow */
	SKY_HAV_MAT_GRAVEL = 428587608, /*!< Gravel */
	SKY_HAV_MAT_MATERIAL_CHAIN_METAL = 438912228, /*!< Material Chain Metal */
	SKY_HAV_MAT_BOTTLE = 493553910, /*!< Bottle */
	SKY_HAV_MAT_WOOD = 500811281, /*!< Wood */
	SKY_HAV_MAT_SKIN = 591247106, /*!< Skin */
	SKY_HAV_MAT_UNKNOWN_617099282 = 617099282, /*!< Unknown in Creation Kit v1.9.32.0. Found in Dawnguard DLC in meshes\dlc01\clutter\dlc01deerskin.nif. */
	SKY_HAV_MAT_BARREL = 732141076, /*!< Barrel */
	SKY_HAV_MAT_MATERIAL_CERAMIC_MEDIUM = 781661019, /*!< Material Ceramic Medium */
	SKY_HAV_MAT_MATERIAL_BASKET = 790784366, /*!< Material Basket */
	SKY_HAV_MAT_ICE = 873356572, /*!< Ice */
	SKY_HAV_MAT_STAIRS_STONE = 899511101, /*!< Stairs Stone */
	SKY_HAV_MAT_WATER = 1024582599, /*!< Water */
	SKY_HAV_MAT_UNKNOWN_1028101969 = 1028101969, /*!< Unknown in Creation Kit v1.6.89.0. Found in actors\draugr\character assets\skeletons.nif. */
	SKY_HAV_MAT_MATERIAL_BLADE_1HAND = 1060167844, /*!< Material Blade 1 Hand */
	SKY_HAV_MAT_MATERIAL_BOOK = 1264672850, /*!< Material Book */
	SKY_HAV_MAT_MATERIAL_CARPET = 1286705471, /*!< Material Carpet */
	SKY_HAV_MAT_SOLID_METAL = 1288358971, /*!< Solid Metal */
	SKY_HAV_MAT_MATERIAL_AXE_1HAND = 1305674443, /*!< Material Axe 1Hand */
	SKY_HAV_MAT_UNKNOWN_1440721808 = 1440721808, /*!< Unknown in Creation Kit v1.6.89.0. Found in armor\draugr\draugrbootsfemale_go.nif or armor\amuletsandrings\amuletgnd.nif. */
	SKY_HAV_MAT_STAIRS_WOOD = 1461712277, /*!< Stairs Wood */
	SKY_HAV_MAT_MUD = 1486385281, /*!< Mud */
	SKY_HAV_MAT_MATERIAL_BOULDER_SMALL = 1550912982, /*!< Material Boulder Small */
	SKY_HAV_MAT_STAIRS_SNOW = 1560365355, /*!< Stairs Snow */
	SKY_HAV_MAT_HEAVY_STONE = 1570821952, /*!< Heavy Stone */
	SKY_HAV_MAT_UNKNOWN_1574477864 = 1574477864, /*!< Unknown in Creation Kit v1.6.89.0. Found in actors\dragon\character assets\skeleton.nif. */
	SKY_HAV_MAT_UNKNOWN_1591009235 = 1591009235, /*!< Unknown in Creation Kit v1.6.89.0. Found in trap objects or clutter\displaycases\displaycaselgangled01.nif or actors\deer\character assets\skeleton.nif. */
	SKY_HAV_MAT_MATERIAL_BOWS_STAVES = 1607128641, /*!< Material Bows Staves */
	SKY_HAV_MAT_MATERIAL_WOOD_AS_STAIRS = 1803571212, /*!< Material Wood As Stairs */
	SKY_HAV_MAT_GRASS = 1848600814, /*!< Grass */
	SKY_HAV_MAT_MATERIAL_BOULDER_LARGE = 1885326971, /*!< Material Boulder Large */
	SKY_HAV_MAT_MATERIAL_STONE_AS_STAIRS = 1886078335, /*!< Material Stone As Stairs */
	SKY_HAV_MAT_MATERIAL_BLADE_2HAND = 2022742644, /*!< Material Blade 2Hand */
	SKY_HAV_MAT_MATERIAL_BOTTLE_SMALL = 2025794648, /*!< Material Bottle Small */
	SKY_HAV_MAT_SAND = 2168343821, /*!< Sand */
	SKY_HAV_MAT_HEAVY_METAL = 2229413539, /*!< Heavy Metal */
	SKY_HAV_MAT_UNKNOWN_2290050264 = 2290050264, /*!< Unknown in Creation Kit v1.9.32.0. Found in Dawnguard DLC in meshes\dlc01\clutter\dlc01sabrecatpelt.nif. */
	SKY_HAV_MAT_DRAGON = 2518321175, /*!< Dragon */
	SKY_HAV_MAT_MATERIAL_BLADE_1HAND_SMALL = 2617944780, /*!< Material Blade 1Hand Small */
	SKY_HAV_MAT_MATERIAL_SKIN_SMALL = 2632367422, /*!< Material Skin Small */
	SKY_HAV_MAT_STAIRS_BROKEN_STONE = 2892392795, /*!< Stairs Broken Stone */
	SKY_HAV_MAT_MATERIAL_SKIN_LARGE = 2965929619, /*!< Material Skin Large */
	SKY_HAV_MAT_ORGANIC = 2974920155, /*!< Organic */
	SKY_HAV_MAT_MATERIAL_BONE = 3049421844, /*!< Material Bone */
	SKY_HAV_MAT_HEAVY_WOOD = 3070783559, /*!< Heavy Wood */
	SKY_HAV_MAT_MATERIAL_CHAIN = 3074114406, /*!< Material Chain */
	SKY_HAV_MAT_DIRT = 3106094762, /*!< Dirt */
	SKY_HAV_MAT_MATERIAL_ARMOR_LIGHT = 3424720541, /*!< Material Armor Light */
	SKY_HAV_MAT_MATERIAL_SHIELD_LIGHT = 3448167928, /*!< Material Shield Light */
	SKY_HAV_MAT_MATERIAL_COIN = 3589100606, /*!< Material Coin */
	SKY_HAV_MAT_MATERIAL_SHIELD_HEAVY = 3702389584, /*!< Material Shield Heavy */
	SKY_HAV_MAT_MATERIAL_ARMOR_HEAVY = 3708432437, /*!< Material Armor Heavy */
	SKY_HAV_MAT_MATERIAL_ARROW = 3725505938, /*!< Material Arrow */
	SKY_HAV_MAT_GLASS = 3739830338, /*!< Glass */
	SKY_HAV_MAT_STONE = 3741512247, /*!< Stone */
	SKY_HAV_MAT_CLOTH = 3839073443, /*!< Cloth */
	SKY_HAV_MAT_MATERIAL_BLUNT_2HAND = 3969592277, /*!< Material Blunt 2Hand */
	SKY_HAV_MAT_UNKNOWN_4239621792 = 4239621792, /*!< Unknown in Creation Kit v1.9.32.0. Found in Dawnguard DLC in meshes\dlc01\prototype\dlc1protoswingingbridge.nif. */
	SKY_HAV_MAT_MATERIAL_BOULDER_MEDIUM = 4283869410, /*!< Material Boulder Medium */
}
static partial class Nif { //--SkyrimHavokMaterial--//
public static void NifStream(out SkyrimHavokMaterial val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (SkyrimHavokMaterial)temp; }
public static void NifStream(SkyrimHavokMaterial val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(SkyrimHavokMaterial val) { switch (val) {
	case SkyrimHavokMaterial.SKY_HAV_MAT_BROKEN_STONE: return "SKY_HAV_MAT_BROKEN_STONE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_LIGHT_WOOD: return "SKY_HAV_MAT_LIGHT_WOOD";
	case SkyrimHavokMaterial.SKY_HAV_MAT_SNOW: return "SKY_HAV_MAT_SNOW";
	case SkyrimHavokMaterial.SKY_HAV_MAT_GRAVEL: return "SKY_HAV_MAT_GRAVEL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_CHAIN_METAL: return "SKY_HAV_MAT_MATERIAL_CHAIN_METAL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_BOTTLE: return "SKY_HAV_MAT_BOTTLE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_WOOD: return "SKY_HAV_MAT_WOOD";
	case SkyrimHavokMaterial.SKY_HAV_MAT_SKIN: return "SKY_HAV_MAT_SKIN";
	case SkyrimHavokMaterial.SKY_HAV_MAT_UNKNOWN_617099282: return "SKY_HAV_MAT_UNKNOWN_617099282";
	case SkyrimHavokMaterial.SKY_HAV_MAT_BARREL: return "SKY_HAV_MAT_BARREL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_CERAMIC_MEDIUM: return "SKY_HAV_MAT_MATERIAL_CERAMIC_MEDIUM";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BASKET: return "SKY_HAV_MAT_MATERIAL_BASKET";
	case SkyrimHavokMaterial.SKY_HAV_MAT_ICE: return "SKY_HAV_MAT_ICE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_STAIRS_STONE: return "SKY_HAV_MAT_STAIRS_STONE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_WATER: return "SKY_HAV_MAT_WATER";
	case SkyrimHavokMaterial.SKY_HAV_MAT_UNKNOWN_1028101969: return "SKY_HAV_MAT_UNKNOWN_1028101969";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BLADE_1HAND: return "SKY_HAV_MAT_MATERIAL_BLADE_1HAND";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BOOK: return "SKY_HAV_MAT_MATERIAL_BOOK";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_CARPET: return "SKY_HAV_MAT_MATERIAL_CARPET";
	case SkyrimHavokMaterial.SKY_HAV_MAT_SOLID_METAL: return "SKY_HAV_MAT_SOLID_METAL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_AXE_1HAND: return "SKY_HAV_MAT_MATERIAL_AXE_1HAND";
	case SkyrimHavokMaterial.SKY_HAV_MAT_UNKNOWN_1440721808: return "SKY_HAV_MAT_UNKNOWN_1440721808";
	case SkyrimHavokMaterial.SKY_HAV_MAT_STAIRS_WOOD: return "SKY_HAV_MAT_STAIRS_WOOD";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MUD: return "SKY_HAV_MAT_MUD";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BOULDER_SMALL: return "SKY_HAV_MAT_MATERIAL_BOULDER_SMALL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_STAIRS_SNOW: return "SKY_HAV_MAT_STAIRS_SNOW";
	case SkyrimHavokMaterial.SKY_HAV_MAT_HEAVY_STONE: return "SKY_HAV_MAT_HEAVY_STONE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_UNKNOWN_1574477864: return "SKY_HAV_MAT_UNKNOWN_1574477864";
	case SkyrimHavokMaterial.SKY_HAV_MAT_UNKNOWN_1591009235: return "SKY_HAV_MAT_UNKNOWN_1591009235";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BOWS_STAVES: return "SKY_HAV_MAT_MATERIAL_BOWS_STAVES";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_WOOD_AS_STAIRS: return "SKY_HAV_MAT_MATERIAL_WOOD_AS_STAIRS";
	case SkyrimHavokMaterial.SKY_HAV_MAT_GRASS: return "SKY_HAV_MAT_GRASS";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BOULDER_LARGE: return "SKY_HAV_MAT_MATERIAL_BOULDER_LARGE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_STONE_AS_STAIRS: return "SKY_HAV_MAT_MATERIAL_STONE_AS_STAIRS";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BLADE_2HAND: return "SKY_HAV_MAT_MATERIAL_BLADE_2HAND";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BOTTLE_SMALL: return "SKY_HAV_MAT_MATERIAL_BOTTLE_SMALL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_SAND: return "SKY_HAV_MAT_SAND";
	case SkyrimHavokMaterial.SKY_HAV_MAT_HEAVY_METAL: return "SKY_HAV_MAT_HEAVY_METAL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_UNKNOWN_2290050264: return "SKY_HAV_MAT_UNKNOWN_2290050264";
	case SkyrimHavokMaterial.SKY_HAV_MAT_DRAGON: return "SKY_HAV_MAT_DRAGON";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BLADE_1HAND_SMALL: return "SKY_HAV_MAT_MATERIAL_BLADE_1HAND_SMALL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_SKIN_SMALL: return "SKY_HAV_MAT_MATERIAL_SKIN_SMALL";
	case SkyrimHavokMaterial.SKY_HAV_MAT_STAIRS_BROKEN_STONE: return "SKY_HAV_MAT_STAIRS_BROKEN_STONE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_SKIN_LARGE: return "SKY_HAV_MAT_MATERIAL_SKIN_LARGE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_ORGANIC: return "SKY_HAV_MAT_ORGANIC";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BONE: return "SKY_HAV_MAT_MATERIAL_BONE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_HEAVY_WOOD: return "SKY_HAV_MAT_HEAVY_WOOD";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_CHAIN: return "SKY_HAV_MAT_MATERIAL_CHAIN";
	case SkyrimHavokMaterial.SKY_HAV_MAT_DIRT: return "SKY_HAV_MAT_DIRT";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_ARMOR_LIGHT: return "SKY_HAV_MAT_MATERIAL_ARMOR_LIGHT";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_SHIELD_LIGHT: return "SKY_HAV_MAT_MATERIAL_SHIELD_LIGHT";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_COIN: return "SKY_HAV_MAT_MATERIAL_COIN";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_SHIELD_HEAVY: return "SKY_HAV_MAT_MATERIAL_SHIELD_HEAVY";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_ARMOR_HEAVY: return "SKY_HAV_MAT_MATERIAL_ARMOR_HEAVY";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_ARROW: return "SKY_HAV_MAT_MATERIAL_ARROW";
	case SkyrimHavokMaterial.SKY_HAV_MAT_GLASS: return "SKY_HAV_MAT_GLASS";
	case SkyrimHavokMaterial.SKY_HAV_MAT_STONE: return "SKY_HAV_MAT_STONE";
	case SkyrimHavokMaterial.SKY_HAV_MAT_CLOTH: return "SKY_HAV_MAT_CLOTH";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BLUNT_2HAND: return "SKY_HAV_MAT_MATERIAL_BLUNT_2HAND";
	case SkyrimHavokMaterial.SKY_HAV_MAT_UNKNOWN_4239621792: return "SKY_HAV_MAT_UNKNOWN_4239621792";
	case SkyrimHavokMaterial.SKY_HAV_MAT_MATERIAL_BOULDER_MEDIUM: return "SKY_HAV_MAT_MATERIAL_BOULDER_MEDIUM";
	default: return $"Invalid Value! - {val}";
}}}

/*! Bethesda Havok. Describes the collision layer a body belongs to in Oblivion. */
public enum OblivionLayer : uint {
	OL_UNIDENTIFIED = 0, /*!< Unidentified (white) */
	OL_STATIC = 1, /*!< Static (red) */
	OL_ANIM_STATIC = 2, /*!< AnimStatic (magenta) */
	OL_TRANSPARENT = 3, /*!< Transparent (light pink) */
	OL_CLUTTER = 4, /*!< Clutter (light blue) */
	OL_WEAPON = 5, /*!< Weapon (orange) */
	OL_PROJECTILE = 6, /*!< Projectile (light orange) */
	OL_SPELL = 7, /*!< Spell (cyan) */
	OL_BIPED = 8, /*!< Biped (green) Seems to apply to all creatures/NPCs */
	OL_TREES = 9, /*!< Trees (light brown) */
	OL_PROPS = 10, /*!< Props (magenta) */
	OL_WATER = 11, /*!< Water (cyan) */
	OL_TRIGGER = 12, /*!< Trigger (light grey) */
	OL_TERRAIN = 13, /*!< Terrain (light yellow) */
	OL_TRAP = 14, /*!< Trap (light grey) */
	OL_NONCOLLIDABLE = 15, /*!< NonCollidable (white) */
	OL_CLOUD_TRAP = 16, /*!< CloudTrap (greenish grey) */
	OL_GROUND = 17, /*!< Ground (none) */
	OL_PORTAL = 18, /*!< Portal (green) */
	OL_STAIRS = 19, /*!< Stairs (white) */
	OL_CHAR_CONTROLLER = 20, /*!< CharController (yellow) */
	OL_AVOID_BOX = 21, /*!< AvoidBox (dark yellow) */
	OL_UNKNOWN1 = 22, /*!< ? (white) */
	OL_UNKNOWN2 = 23, /*!< ? (white) */
	OL_CAMERA_PICK = 24, /*!< CameraPick (white) */
	OL_ITEM_PICK = 25, /*!< ItemPick (white) */
	OL_LINE_OF_SIGHT = 26, /*!< LineOfSight (white) */
	OL_PATH_PICK = 27, /*!< PathPick (white) */
	OL_CUSTOM_PICK_1 = 28, /*!< CustomPick1 (white) */
	OL_CUSTOM_PICK_2 = 29, /*!< CustomPick2 (white) */
	OL_SPELL_EXPLOSION = 30, /*!< SpellExplosion (white) */
	OL_DROPPING_PICK = 31, /*!< DroppingPick (white) */
	OL_OTHER = 32, /*!< Other (white) */
	OL_HEAD = 33, /*!< Head */
	OL_BODY = 34, /*!< Body */
	OL_SPINE1 = 35, /*!< Spine1 */
	OL_SPINE2 = 36, /*!< Spine2 */
	OL_L_UPPER_ARM = 37, /*!< LUpperArm */
	OL_L_FOREARM = 38, /*!< LForeArm */
	OL_L_HAND = 39, /*!< LHand */
	OL_L_THIGH = 40, /*!< LThigh */
	OL_L_CALF = 41, /*!< LCalf */
	OL_L_FOOT = 42, /*!< LFoot */
	OL_R_UPPER_ARM = 43, /*!< RUpperArm */
	OL_R_FOREARM = 44, /*!< RForeArm */
	OL_R_HAND = 45, /*!< RHand */
	OL_R_THIGH = 46, /*!< RThigh */
	OL_R_CALF = 47, /*!< RCalf */
	OL_R_FOOT = 48, /*!< RFoot */
	OL_TAIL = 49, /*!< Tail */
	OL_SIDE_WEAPON = 50, /*!< SideWeapon */
	OL_SHIELD = 51, /*!< Shield */
	OL_QUIVER = 52, /*!< Quiver */
	OL_BACK_WEAPON = 53, /*!< BackWeapon */
	OL_BACK_WEAPON2 = 54, /*!< BackWeapon (?) */
	OL_PONYTAIL = 55, /*!< PonyTail */
	OL_WING = 56, /*!< Wing */
	OL_NULL = 57, /*!< Null */
}
static partial class Nif { //--OblivionLayer--//
public static void NifStream(out OblivionLayer val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (OblivionLayer)temp; }
public static void NifStream(OblivionLayer val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(OblivionLayer val) { switch (val) {
	case OblivionLayer.OL_UNIDENTIFIED: return "OL_UNIDENTIFIED";
	case OblivionLayer.OL_STATIC: return "OL_STATIC";
	case OblivionLayer.OL_ANIM_STATIC: return "OL_ANIM_STATIC";
	case OblivionLayer.OL_TRANSPARENT: return "OL_TRANSPARENT";
	case OblivionLayer.OL_CLUTTER: return "OL_CLUTTER";
	case OblivionLayer.OL_WEAPON: return "OL_WEAPON";
	case OblivionLayer.OL_PROJECTILE: return "OL_PROJECTILE";
	case OblivionLayer.OL_SPELL: return "OL_SPELL";
	case OblivionLayer.OL_BIPED: return "OL_BIPED";
	case OblivionLayer.OL_TREES: return "OL_TREES";
	case OblivionLayer.OL_PROPS: return "OL_PROPS";
	case OblivionLayer.OL_WATER: return "OL_WATER";
	case OblivionLayer.OL_TRIGGER: return "OL_TRIGGER";
	case OblivionLayer.OL_TERRAIN: return "OL_TERRAIN";
	case OblivionLayer.OL_TRAP: return "OL_TRAP";
	case OblivionLayer.OL_NONCOLLIDABLE: return "OL_NONCOLLIDABLE";
	case OblivionLayer.OL_CLOUD_TRAP: return "OL_CLOUD_TRAP";
	case OblivionLayer.OL_GROUND: return "OL_GROUND";
	case OblivionLayer.OL_PORTAL: return "OL_PORTAL";
	case OblivionLayer.OL_STAIRS: return "OL_STAIRS";
	case OblivionLayer.OL_CHAR_CONTROLLER: return "OL_CHAR_CONTROLLER";
	case OblivionLayer.OL_AVOID_BOX: return "OL_AVOID_BOX";
	case OblivionLayer.OL_UNKNOWN1: return "OL_UNKNOWN1";
	case OblivionLayer.OL_UNKNOWN2: return "OL_UNKNOWN2";
	case OblivionLayer.OL_CAMERA_PICK: return "OL_CAMERA_PICK";
	case OblivionLayer.OL_ITEM_PICK: return "OL_ITEM_PICK";
	case OblivionLayer.OL_LINE_OF_SIGHT: return "OL_LINE_OF_SIGHT";
	case OblivionLayer.OL_PATH_PICK: return "OL_PATH_PICK";
	case OblivionLayer.OL_CUSTOM_PICK_1: return "OL_CUSTOM_PICK_1";
	case OblivionLayer.OL_CUSTOM_PICK_2: return "OL_CUSTOM_PICK_2";
	case OblivionLayer.OL_SPELL_EXPLOSION: return "OL_SPELL_EXPLOSION";
	case OblivionLayer.OL_DROPPING_PICK: return "OL_DROPPING_PICK";
	case OblivionLayer.OL_OTHER: return "OL_OTHER";
	case OblivionLayer.OL_HEAD: return "OL_HEAD";
	case OblivionLayer.OL_BODY: return "OL_BODY";
	case OblivionLayer.OL_SPINE1: return "OL_SPINE1";
	case OblivionLayer.OL_SPINE2: return "OL_SPINE2";
	case OblivionLayer.OL_L_UPPER_ARM: return "OL_L_UPPER_ARM";
	case OblivionLayer.OL_L_FOREARM: return "OL_L_FOREARM";
	case OblivionLayer.OL_L_HAND: return "OL_L_HAND";
	case OblivionLayer.OL_L_THIGH: return "OL_L_THIGH";
	case OblivionLayer.OL_L_CALF: return "OL_L_CALF";
	case OblivionLayer.OL_L_FOOT: return "OL_L_FOOT";
	case OblivionLayer.OL_R_UPPER_ARM: return "OL_R_UPPER_ARM";
	case OblivionLayer.OL_R_FOREARM: return "OL_R_FOREARM";
	case OblivionLayer.OL_R_HAND: return "OL_R_HAND";
	case OblivionLayer.OL_R_THIGH: return "OL_R_THIGH";
	case OblivionLayer.OL_R_CALF: return "OL_R_CALF";
	case OblivionLayer.OL_R_FOOT: return "OL_R_FOOT";
	case OblivionLayer.OL_TAIL: return "OL_TAIL";
	case OblivionLayer.OL_SIDE_WEAPON: return "OL_SIDE_WEAPON";
	case OblivionLayer.OL_SHIELD: return "OL_SHIELD";
	case OblivionLayer.OL_QUIVER: return "OL_QUIVER";
	case OblivionLayer.OL_BACK_WEAPON: return "OL_BACK_WEAPON";
	case OblivionLayer.OL_BACK_WEAPON2: return "OL_BACK_WEAPON2";
	case OblivionLayer.OL_PONYTAIL: return "OL_PONYTAIL";
	case OblivionLayer.OL_WING: return "OL_WING";
	case OblivionLayer.OL_NULL: return "OL_NULL";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Havok. Describes the collision layer a body belongs to in Fallout 3 and
 * Fallout NV.
 */
public enum Fallout3Layer : uint {
	FOL_UNIDENTIFIED = 0, /*!< Unidentified (white) */
	FOL_STATIC = 1, /*!< Static (red) */
	FOL_ANIM_STATIC = 2, /*!< AnimStatic (magenta) */
	FOL_TRANSPARENT = 3, /*!< Transparent (light pink) */
	FOL_CLUTTER = 4, /*!< Clutter (light blue) */
	FOL_WEAPON = 5, /*!< Weapon (orange) */
	FOL_PROJECTILE = 6, /*!< Projectile (light orange) */
	FOL_SPELL = 7, /*!< Spell (cyan) */
	FOL_BIPED = 8, /*!< Biped (green) Seems to apply to all creatures/NPCs */
	FOL_TREES = 9, /*!< Trees (light brown) */
	FOL_PROPS = 10, /*!< Props (magenta) */
	FOL_WATER = 11, /*!< Water (cyan) */
	FOL_TRIGGER = 12, /*!< Trigger (light grey) */
	FOL_TERRAIN = 13, /*!< Terrain (light yellow) */
	FOL_TRAP = 14, /*!< Trap (light grey) */
	FOL_NONCOLLIDABLE = 15, /*!< NonCollidable (white) */
	FOL_CLOUD_TRAP = 16, /*!< CloudTrap (greenish grey) */
	FOL_GROUND = 17, /*!< Ground (none) */
	FOL_PORTAL = 18, /*!< Portal (green) */
	FOL_DEBRIS_SMALL = 19, /*!< DebrisSmall (white) */
	FOL_DEBRIS_LARGE = 20, /*!< DebrisLarge (white) */
	FOL_ACOUSTIC_SPACE = 21, /*!< AcousticSpace (white) */
	FOL_ACTORZONE = 22, /*!< Actorzone (white) */
	FOL_PROJECTILEZONE = 23, /*!< Projectilezone (white) */
	FOL_GASTRAP = 24, /*!< GasTrap (yellowish green) */
	FOL_SHELLCASING = 25, /*!< ShellCasing (white) */
	FOL_TRANSPARENT_SMALL = 26, /*!< TransparentSmall (white) */
	FOL_INVISIBLE_WALL = 27, /*!< InvisibleWall (white) */
	FOL_TRANSPARENT_SMALL_ANIM = 28, /*!< TransparentSmallAnim (white) */
	FOL_DEADBIP = 29, /*!< Dead Biped (green) */
	FOL_CHARCONTROLLER = 30, /*!< CharController (yellow) */
	FOL_AVOIDBOX = 31, /*!< Avoidbox (orange) */
	FOL_COLLISIONBOX = 32, /*!< Collisionbox (white) */
	FOL_CAMERASPHERE = 33, /*!< Camerasphere (white) */
	FOL_DOORDETECTION = 34, /*!< Doordetection (white) */
	FOL_CAMERAPICK = 35, /*!< Camerapick (white) */
	FOL_ITEMPICK = 36, /*!< Itempick (white) */
	FOL_LINEOFSIGHT = 37, /*!< LineOfSight (white) */
	FOL_PATHPICK = 38, /*!< Pathpick (white) */
	FOL_CUSTOMPICK1 = 39, /*!< Custompick1 (white) */
	FOL_CUSTOMPICK2 = 40, /*!< Custompick2 (white) */
	FOL_SPELLEXPLOSION = 41, /*!< SpellExplosion (white) */
	FOL_DROPPINGPICK = 42, /*!< Droppingpick (white) */
	FOL_NULL = 43, /*!< Null (white) */
}
static partial class Nif { //--Fallout3Layer--//
public static void NifStream(out Fallout3Layer val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (Fallout3Layer)temp; }
public static void NifStream(Fallout3Layer val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(Fallout3Layer val) { switch (val) {
	case Fallout3Layer.FOL_UNIDENTIFIED: return "FOL_UNIDENTIFIED";
	case Fallout3Layer.FOL_STATIC: return "FOL_STATIC";
	case Fallout3Layer.FOL_ANIM_STATIC: return "FOL_ANIM_STATIC";
	case Fallout3Layer.FOL_TRANSPARENT: return "FOL_TRANSPARENT";
	case Fallout3Layer.FOL_CLUTTER: return "FOL_CLUTTER";
	case Fallout3Layer.FOL_WEAPON: return "FOL_WEAPON";
	case Fallout3Layer.FOL_PROJECTILE: return "FOL_PROJECTILE";
	case Fallout3Layer.FOL_SPELL: return "FOL_SPELL";
	case Fallout3Layer.FOL_BIPED: return "FOL_BIPED";
	case Fallout3Layer.FOL_TREES: return "FOL_TREES";
	case Fallout3Layer.FOL_PROPS: return "FOL_PROPS";
	case Fallout3Layer.FOL_WATER: return "FOL_WATER";
	case Fallout3Layer.FOL_TRIGGER: return "FOL_TRIGGER";
	case Fallout3Layer.FOL_TERRAIN: return "FOL_TERRAIN";
	case Fallout3Layer.FOL_TRAP: return "FOL_TRAP";
	case Fallout3Layer.FOL_NONCOLLIDABLE: return "FOL_NONCOLLIDABLE";
	case Fallout3Layer.FOL_CLOUD_TRAP: return "FOL_CLOUD_TRAP";
	case Fallout3Layer.FOL_GROUND: return "FOL_GROUND";
	case Fallout3Layer.FOL_PORTAL: return "FOL_PORTAL";
	case Fallout3Layer.FOL_DEBRIS_SMALL: return "FOL_DEBRIS_SMALL";
	case Fallout3Layer.FOL_DEBRIS_LARGE: return "FOL_DEBRIS_LARGE";
	case Fallout3Layer.FOL_ACOUSTIC_SPACE: return "FOL_ACOUSTIC_SPACE";
	case Fallout3Layer.FOL_ACTORZONE: return "FOL_ACTORZONE";
	case Fallout3Layer.FOL_PROJECTILEZONE: return "FOL_PROJECTILEZONE";
	case Fallout3Layer.FOL_GASTRAP: return "FOL_GASTRAP";
	case Fallout3Layer.FOL_SHELLCASING: return "FOL_SHELLCASING";
	case Fallout3Layer.FOL_TRANSPARENT_SMALL: return "FOL_TRANSPARENT_SMALL";
	case Fallout3Layer.FOL_INVISIBLE_WALL: return "FOL_INVISIBLE_WALL";
	case Fallout3Layer.FOL_TRANSPARENT_SMALL_ANIM: return "FOL_TRANSPARENT_SMALL_ANIM";
	case Fallout3Layer.FOL_DEADBIP: return "FOL_DEADBIP";
	case Fallout3Layer.FOL_CHARCONTROLLER: return "FOL_CHARCONTROLLER";
	case Fallout3Layer.FOL_AVOIDBOX: return "FOL_AVOIDBOX";
	case Fallout3Layer.FOL_COLLISIONBOX: return "FOL_COLLISIONBOX";
	case Fallout3Layer.FOL_CAMERASPHERE: return "FOL_CAMERASPHERE";
	case Fallout3Layer.FOL_DOORDETECTION: return "FOL_DOORDETECTION";
	case Fallout3Layer.FOL_CAMERAPICK: return "FOL_CAMERAPICK";
	case Fallout3Layer.FOL_ITEMPICK: return "FOL_ITEMPICK";
	case Fallout3Layer.FOL_LINEOFSIGHT: return "FOL_LINEOFSIGHT";
	case Fallout3Layer.FOL_PATHPICK: return "FOL_PATHPICK";
	case Fallout3Layer.FOL_CUSTOMPICK1: return "FOL_CUSTOMPICK1";
	case Fallout3Layer.FOL_CUSTOMPICK2: return "FOL_CUSTOMPICK2";
	case Fallout3Layer.FOL_SPELLEXPLOSION: return "FOL_SPELLEXPLOSION";
	case Fallout3Layer.FOL_DROPPINGPICK: return "FOL_DROPPINGPICK";
	case Fallout3Layer.FOL_NULL: return "FOL_NULL";
	default: return $"Invalid Value! - {val}";
}}}

/*! Bethesda Havok. Describes the collision layer a body belongs to in Skyrim. */
public enum SkyrimLayer : uint {
	SKYL_UNIDENTIFIED = 0, /*!< Unidentified */
	SKYL_STATIC = 1, /*!< Static */
	SKYL_ANIMSTATIC = 2, /*!< Anim Static */
	SKYL_TRANSPARENT = 3, /*!< Transparent */
	SKYL_CLUTTER = 4, /*!< Clutter. Object with this layer will float on water surface. */
	SKYL_WEAPON = 5, /*!< Weapon */
	SKYL_PROJECTILE = 6, /*!< Projectile */
	SKYL_SPELL = 7, /*!< Spell */
	SKYL_BIPED = 8, /*!< Biped. Seems to apply to all creatures/NPCs */
	SKYL_TREES = 9, /*!< Trees */
	SKYL_PROPS = 10, /*!< Props */
	SKYL_WATER = 11, /*!< Water */
	SKYL_TRIGGER = 12, /*!< Trigger */
	SKYL_TERRAIN = 13, /*!< Terrain */
	SKYL_TRAP = 14, /*!< Trap */
	SKYL_NONCOLLIDABLE = 15, /*!< NonCollidable */
	SKYL_CLOUD_TRAP = 16, /*!< CloudTrap */
	SKYL_GROUND = 17, /*!< Ground. It seems that produces no sound when collide. */
	SKYL_PORTAL = 18, /*!< Portal */
	SKYL_DEBRIS_SMALL = 19, /*!< Debris Small */
	SKYL_DEBRIS_LARGE = 20, /*!< Debris Large */
	SKYL_ACOUSTIC_SPACE = 21, /*!< Acoustic Space */
	SKYL_ACTORZONE = 22, /*!< Actor Zone */
	SKYL_PROJECTILEZONE = 23, /*!< Projectile Zone */
	SKYL_GASTRAP = 24, /*!< Gas Trap */
	SKYL_SHELLCASING = 25, /*!< Shell Casing */
	SKYL_TRANSPARENT_SMALL = 26, /*!< Transparent Small */
	SKYL_INVISIBLE_WALL = 27, /*!< Invisible Wall */
	SKYL_TRANSPARENT_SMALL_ANIM = 28, /*!< Transparent Small Anim */
	SKYL_WARD = 29, /*!< Ward */
	SKYL_CHARCONTROLLER = 30, /*!< Char Controller */
	SKYL_STAIRHELPER = 31, /*!< Stair Helper */
	SKYL_DEADBIP = 32, /*!< Dead Bip */
	SKYL_BIPED_NO_CC = 33, /*!< Biped No CC */
	SKYL_AVOIDBOX = 34, /*!< Avoid Box */
	SKYL_COLLISIONBOX = 35, /*!< Collision Box */
	SKYL_CAMERASHPERE = 36, /*!< Camera Sphere */
	SKYL_DOORDETECTION = 37, /*!< Door Detection */
	SKYL_CONEPROJECTILE = 38, /*!< Cone Projectile */
	SKYL_CAMERAPICK = 39, /*!< Camera Pick */
	SKYL_ITEMPICK = 40, /*!< Item Pick */
	SKYL_LINEOFSIGHT = 41, /*!< Line of Sight */
	SKYL_PATHPICK = 42, /*!< Path Pick */
	SKYL_CUSTOMPICK1 = 43, /*!< Custom Pick 1 */
	SKYL_CUSTOMPICK2 = 44, /*!< Custom Pick 2 */
	SKYL_SPELLEXPLOSION = 45, /*!< Spell Explosion */
	SKYL_DROPPINGPICK = 46, /*!< Dropping Pick */
	SKYL_NULL = 47, /*!< Null */
}
static partial class Nif { //--SkyrimLayer--//
public static void NifStream(out SkyrimLayer val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (SkyrimLayer)temp; }
public static void NifStream(SkyrimLayer val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(SkyrimLayer val) { switch (val) {
	case SkyrimLayer.SKYL_UNIDENTIFIED: return "SKYL_UNIDENTIFIED";
	case SkyrimLayer.SKYL_STATIC: return "SKYL_STATIC";
	case SkyrimLayer.SKYL_ANIMSTATIC: return "SKYL_ANIMSTATIC";
	case SkyrimLayer.SKYL_TRANSPARENT: return "SKYL_TRANSPARENT";
	case SkyrimLayer.SKYL_CLUTTER: return "SKYL_CLUTTER";
	case SkyrimLayer.SKYL_WEAPON: return "SKYL_WEAPON";
	case SkyrimLayer.SKYL_PROJECTILE: return "SKYL_PROJECTILE";
	case SkyrimLayer.SKYL_SPELL: return "SKYL_SPELL";
	case SkyrimLayer.SKYL_BIPED: return "SKYL_BIPED";
	case SkyrimLayer.SKYL_TREES: return "SKYL_TREES";
	case SkyrimLayer.SKYL_PROPS: return "SKYL_PROPS";
	case SkyrimLayer.SKYL_WATER: return "SKYL_WATER";
	case SkyrimLayer.SKYL_TRIGGER: return "SKYL_TRIGGER";
	case SkyrimLayer.SKYL_TERRAIN: return "SKYL_TERRAIN";
	case SkyrimLayer.SKYL_TRAP: return "SKYL_TRAP";
	case SkyrimLayer.SKYL_NONCOLLIDABLE: return "SKYL_NONCOLLIDABLE";
	case SkyrimLayer.SKYL_CLOUD_TRAP: return "SKYL_CLOUD_TRAP";
	case SkyrimLayer.SKYL_GROUND: return "SKYL_GROUND";
	case SkyrimLayer.SKYL_PORTAL: return "SKYL_PORTAL";
	case SkyrimLayer.SKYL_DEBRIS_SMALL: return "SKYL_DEBRIS_SMALL";
	case SkyrimLayer.SKYL_DEBRIS_LARGE: return "SKYL_DEBRIS_LARGE";
	case SkyrimLayer.SKYL_ACOUSTIC_SPACE: return "SKYL_ACOUSTIC_SPACE";
	case SkyrimLayer.SKYL_ACTORZONE: return "SKYL_ACTORZONE";
	case SkyrimLayer.SKYL_PROJECTILEZONE: return "SKYL_PROJECTILEZONE";
	case SkyrimLayer.SKYL_GASTRAP: return "SKYL_GASTRAP";
	case SkyrimLayer.SKYL_SHELLCASING: return "SKYL_SHELLCASING";
	case SkyrimLayer.SKYL_TRANSPARENT_SMALL: return "SKYL_TRANSPARENT_SMALL";
	case SkyrimLayer.SKYL_INVISIBLE_WALL: return "SKYL_INVISIBLE_WALL";
	case SkyrimLayer.SKYL_TRANSPARENT_SMALL_ANIM: return "SKYL_TRANSPARENT_SMALL_ANIM";
	case SkyrimLayer.SKYL_WARD: return "SKYL_WARD";
	case SkyrimLayer.SKYL_CHARCONTROLLER: return "SKYL_CHARCONTROLLER";
	case SkyrimLayer.SKYL_STAIRHELPER: return "SKYL_STAIRHELPER";
	case SkyrimLayer.SKYL_DEADBIP: return "SKYL_DEADBIP";
	case SkyrimLayer.SKYL_BIPED_NO_CC: return "SKYL_BIPED_NO_CC";
	case SkyrimLayer.SKYL_AVOIDBOX: return "SKYL_AVOIDBOX";
	case SkyrimLayer.SKYL_COLLISIONBOX: return "SKYL_COLLISIONBOX";
	case SkyrimLayer.SKYL_CAMERASHPERE: return "SKYL_CAMERASHPERE";
	case SkyrimLayer.SKYL_DOORDETECTION: return "SKYL_DOORDETECTION";
	case SkyrimLayer.SKYL_CONEPROJECTILE: return "SKYL_CONEPROJECTILE";
	case SkyrimLayer.SKYL_CAMERAPICK: return "SKYL_CAMERAPICK";
	case SkyrimLayer.SKYL_ITEMPICK: return "SKYL_ITEMPICK";
	case SkyrimLayer.SKYL_LINEOFSIGHT: return "SKYL_LINEOFSIGHT";
	case SkyrimLayer.SKYL_PATHPICK: return "SKYL_PATHPICK";
	case SkyrimLayer.SKYL_CUSTOMPICK1: return "SKYL_CUSTOMPICK1";
	case SkyrimLayer.SKYL_CUSTOMPICK2: return "SKYL_CUSTOMPICK2";
	case SkyrimLayer.SKYL_SPELLEXPLOSION: return "SKYL_SPELLEXPLOSION";
	case SkyrimLayer.SKYL_DROPPINGPICK: return "SKYL_DROPPINGPICK";
	case SkyrimLayer.SKYL_NULL: return "SKYL_NULL";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Havok.
 *         A byte describing if MOPP Data is organized into chunks (PS3) or not
 * (PC)
 */
public enum MoppDataBuildType : uint {
	BUILT_WITH_CHUNK_SUBDIVISION = 0, /*!< Organized in chunks for PS3. */
	BUILT_WITHOUT_CHUNK_SUBDIVISION = 1, /*!< Not organized in chunks for PC. (Default) */
	BUILD_NOT_SET = 2, /*!< Build type not set yet. */
}
static partial class Nif { //--MoppDataBuildType--//
public static void NifStream(out MoppDataBuildType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (MoppDataBuildType)temp; }
public static void NifStream(MoppDataBuildType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(MoppDataBuildType val) { switch (val) {
	case MoppDataBuildType.BUILT_WITH_CHUNK_SUBDIVISION: return "BUILT_WITH_CHUNK_SUBDIVISION";
	case MoppDataBuildType.BUILT_WITHOUT_CHUNK_SUBDIVISION: return "BUILT_WITHOUT_CHUNK_SUBDIVISION";
	case MoppDataBuildType.BUILD_NOT_SET: return "BUILD_NOT_SET";
	default: return $"Invalid Value! - {val}";
}}}

/*! Target platform for NiPersistentSrcTextureRendererData (later than 30.1). */
public enum PlatformID : uint {
	PLATFORM_ANY = 0, /*!< PLATFORM_ANY */
	PLATFORM_XENON = 1, /*!< PLATFORM_XENON */
	PLATFORM_PS3 = 2, /*!< PLATFORM_PS3 */
	PLATFORM_DX9 = 3, /*!< PLATFORM_DX9 */
	PLATFORM_WII = 4, /*!< PLATFORM_WII */
	PLATFORM_D3D10 = 5, /*!< PLATFORM_D3D10 */
}
static partial class Nif { //--PlatformID--//
public static void NifStream(out PlatformID val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PlatformID)temp; }
public static void NifStream(PlatformID val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PlatformID val) { switch (val) {
	case PlatformID.PLATFORM_ANY: return "PLATFORM_ANY";
	case PlatformID.PLATFORM_XENON: return "PLATFORM_XENON";
	case PlatformID.PLATFORM_PS3: return "PLATFORM_PS3";
	case PlatformID.PLATFORM_DX9: return "PLATFORM_DX9";
	case PlatformID.PLATFORM_WII: return "PLATFORM_WII";
	case PlatformID.PLATFORM_D3D10: return "PLATFORM_D3D10";
	default: return $"Invalid Value! - {val}";
}}}

/*! Target renderer for NiPersistentSrcTextureRendererData (until 30.1). */
public enum RendererID : uint {
	RENDERER_XBOX360 = 0, /*!< RENDERER_XBOX360 */
	RENDERER_PS3 = 1, /*!< RENDERER_PS3 */
	RENDERER_DX9 = 2, /*!< RENDERER_DX9 */
	RENDERER_D3D10 = 3, /*!< RENDERER_D3D10 */
	RENDERER_WII = 4, /*!< RENDERER_WII */
	RENDERER_GENERIC = 5, /*!< RENDERER_GENERIC */
	RENDERER_D3D11 = 6, /*!< RENDERER_D3D11 */
}
static partial class Nif { //--RendererID--//
public static void NifStream(out RendererID val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (RendererID)temp; }
public static void NifStream(RendererID val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(RendererID val) { switch (val) {
	case RendererID.RENDERER_XBOX360: return "RENDERER_XBOX360";
	case RendererID.RENDERER_PS3: return "RENDERER_PS3";
	case RendererID.RENDERER_DX9: return "RENDERER_DX9";
	case RendererID.RENDERER_D3D10: return "RENDERER_D3D10";
	case RendererID.RENDERER_WII: return "RENDERER_WII";
	case RendererID.RENDERER_GENERIC: return "RENDERER_GENERIC";
	case RendererID.RENDERER_D3D11: return "RENDERER_D3D11";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the pixel format used by the NiPixelData object to store a texture. */
public enum PixelFormat : uint {
	PX_FMT_RGB = 0, /*!< 24-bit RGB. 8 bits per red, blue, and green component. */
	PX_FMT_RGBA = 1, /*!< 32-bit RGB with alpha. 8 bits per red, blue, green, and alpha component. */
	PX_FMT_PAL = 2, /*!< 8-bit palette index. */
	PX_FMT_PALA = 3, /*!< 8-bit palette index with alpha. */
	PX_FMT_DXT1 = 4, /*!< DXT1 compressed texture. */
	PX_FMT_DXT3 = 5, /*!< DXT3 compressed texture. */
	PX_FMT_DXT5 = 6, /*!< DXT5 compressed texture. */
	PX_FMT_RGB24NONINT = 7, /*!< (Deprecated) 24-bit noninterleaved texture, an old PS2 format. */
	PX_FMT_BUMP = 8, /*!< Uncompressed dU/dV gradient bump map. */
	PX_FMT_BUMPLUMA = 9, /*!< Uncompressed dU/dV gradient bump map with luma channel representing shininess. */
	PX_FMT_RENDERSPEC = 10, /*!< Generic descriptor for any renderer-specific format not described by other formats. */
	PX_FMT_1CH = 11, /*!< Generic descriptor for formats with 1 component. */
	PX_FMT_2CH = 12, /*!< Generic descriptor for formats with 2 components. */
	PX_FMT_3CH = 13, /*!< Generic descriptor for formats with 3 components. */
	PX_FMT_4CH = 14, /*!< Generic descriptor for formats with 4 components. */
	PX_FMT_DEPTH_STENCIL = 15, /*!< Indicates the NiPixelFormat is meant to be used on a depth/stencil surface. */
	PX_FMT_UNKNOWN = 16, /*!< PX_FMT_UNKNOWN */
}
static partial class Nif { //--PixelFormat--//
public static void NifStream(out PixelFormat val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PixelFormat)temp; }
public static void NifStream(PixelFormat val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PixelFormat val) { switch (val) {
	case PixelFormat.PX_FMT_RGB: return "PX_FMT_RGB";
	case PixelFormat.PX_FMT_RGBA: return "PX_FMT_RGBA";
	case PixelFormat.PX_FMT_PAL: return "PX_FMT_PAL";
	case PixelFormat.PX_FMT_PALA: return "PX_FMT_PALA";
	case PixelFormat.PX_FMT_DXT1: return "PX_FMT_DXT1";
	case PixelFormat.PX_FMT_DXT3: return "PX_FMT_DXT3";
	case PixelFormat.PX_FMT_DXT5: return "PX_FMT_DXT5";
	case PixelFormat.PX_FMT_RGB24NONINT: return "PX_FMT_RGB24NONINT";
	case PixelFormat.PX_FMT_BUMP: return "PX_FMT_BUMP";
	case PixelFormat.PX_FMT_BUMPLUMA: return "PX_FMT_BUMPLUMA";
	case PixelFormat.PX_FMT_RENDERSPEC: return "PX_FMT_RENDERSPEC";
	case PixelFormat.PX_FMT_1CH: return "PX_FMT_1CH";
	case PixelFormat.PX_FMT_2CH: return "PX_FMT_2CH";
	case PixelFormat.PX_FMT_3CH: return "PX_FMT_3CH";
	case PixelFormat.PX_FMT_4CH: return "PX_FMT_4CH";
	case PixelFormat.PX_FMT_DEPTH_STENCIL: return "PX_FMT_DEPTH_STENCIL";
	case PixelFormat.PX_FMT_UNKNOWN: return "PX_FMT_UNKNOWN";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes whether pixels have been tiled from their standard row-major format to
 * a format optimized for a particular platform.
 */
public enum PixelTiling : uint {
	PX_TILE_NONE = 0, /*!< PX_TILE_NONE */
	PX_TILE_XENON = 1, /*!< PX_TILE_XENON */
	PX_TILE_WII = 2, /*!< PX_TILE_WII */
	PX_TILE_NV_SWIZZLED = 3, /*!< PX_TILE_NV_SWIZZLED */
}
static partial class Nif { //--PixelTiling--//
public static void NifStream(out PixelTiling val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PixelTiling)temp; }
public static void NifStream(PixelTiling val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PixelTiling val) { switch (val) {
	case PixelTiling.PX_TILE_NONE: return "PX_TILE_NONE";
	case PixelTiling.PX_TILE_XENON: return "PX_TILE_XENON";
	case PixelTiling.PX_TILE_WII: return "PX_TILE_WII";
	case PixelTiling.PX_TILE_NV_SWIZZLED: return "PX_TILE_NV_SWIZZLED";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the pixel format used by the NiPixelData object to store a texture. */
public enum PixelComponent : uint {
	PX_COMP_RED = 0, /*!< PX_COMP_RED */
	PX_COMP_GREEN = 1, /*!< PX_COMP_GREEN */
	PX_COMP_BLUE = 2, /*!< PX_COMP_BLUE */
	PX_COMP_ALPHA = 3, /*!< PX_COMP_ALPHA */
	PX_COMP_COMPRESSED = 4, /*!< PX_COMP_COMPRESSED */
	PX_COMP_OFFSET_U = 5, /*!< PX_COMP_OFFSET_U */
	PX_COMP_OFFSET_V = 6, /*!< PX_COMP_OFFSET_V */
	PX_COMP_OFFSET_W = 7, /*!< PX_COMP_OFFSET_W */
	PX_COMP_OFFSET_Q = 8, /*!< PX_COMP_OFFSET_Q */
	PX_COMP_LUMA = 9, /*!< PX_COMP_LUMA */
	PX_COMP_HEIGHT = 10, /*!< PX_COMP_HEIGHT */
	PX_COMP_VECTOR_X = 11, /*!< PX_COMP_VECTOR_X */
	PX_COMP_VECTOR_Y = 12, /*!< PX_COMP_VECTOR_Y */
	PX_COMP_VECTOR_Z = 13, /*!< PX_COMP_VECTOR_Z */
	PX_COMP_PADDING = 14, /*!< PX_COMP_PADDING */
	PX_COMP_INTENSITY = 15, /*!< PX_COMP_INTENSITY */
	PX_COMP_INDEX = 16, /*!< PX_COMP_INDEX */
	PX_COMP_DEPTH = 17, /*!< PX_COMP_DEPTH */
	PX_COMP_STENCIL = 18, /*!< PX_COMP_STENCIL */
	PX_COMP_EMPTY = 19, /*!< PX_COMP_EMPTY */
}
static partial class Nif { //--PixelComponent--//
public static void NifStream(out PixelComponent val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PixelComponent)temp; }
public static void NifStream(PixelComponent val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PixelComponent val) { switch (val) {
	case PixelComponent.PX_COMP_RED: return "PX_COMP_RED";
	case PixelComponent.PX_COMP_GREEN: return "PX_COMP_GREEN";
	case PixelComponent.PX_COMP_BLUE: return "PX_COMP_BLUE";
	case PixelComponent.PX_COMP_ALPHA: return "PX_COMP_ALPHA";
	case PixelComponent.PX_COMP_COMPRESSED: return "PX_COMP_COMPRESSED";
	case PixelComponent.PX_COMP_OFFSET_U: return "PX_COMP_OFFSET_U";
	case PixelComponent.PX_COMP_OFFSET_V: return "PX_COMP_OFFSET_V";
	case PixelComponent.PX_COMP_OFFSET_W: return "PX_COMP_OFFSET_W";
	case PixelComponent.PX_COMP_OFFSET_Q: return "PX_COMP_OFFSET_Q";
	case PixelComponent.PX_COMP_LUMA: return "PX_COMP_LUMA";
	case PixelComponent.PX_COMP_HEIGHT: return "PX_COMP_HEIGHT";
	case PixelComponent.PX_COMP_VECTOR_X: return "PX_COMP_VECTOR_X";
	case PixelComponent.PX_COMP_VECTOR_Y: return "PX_COMP_VECTOR_Y";
	case PixelComponent.PX_COMP_VECTOR_Z: return "PX_COMP_VECTOR_Z";
	case PixelComponent.PX_COMP_PADDING: return "PX_COMP_PADDING";
	case PixelComponent.PX_COMP_INTENSITY: return "PX_COMP_INTENSITY";
	case PixelComponent.PX_COMP_INDEX: return "PX_COMP_INDEX";
	case PixelComponent.PX_COMP_DEPTH: return "PX_COMP_DEPTH";
	case PixelComponent.PX_COMP_STENCIL: return "PX_COMP_STENCIL";
	case PixelComponent.PX_COMP_EMPTY: return "PX_COMP_EMPTY";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes how each pixel should be accessed on NiPixelFormat. */
public enum PixelRepresentation : uint {
	PX_REP_NORM_INT = 0, /*!< PX_REP_NORM_INT */
	PX_REP_HALF = 1, /*!< PX_REP_HALF */
	PX_REP_FLOAT = 2, /*!< PX_REP_FLOAT */
	PX_REP_INDEX = 3, /*!< PX_REP_INDEX */
	PX_REP_COMPRESSED = 4, /*!< PX_REP_COMPRESSED */
	PX_REP_UNKNOWN = 5, /*!< PX_REP_UNKNOWN */
	PX_REP_INT = 6, /*!< PX_REP_INT */
}
static partial class Nif { //--PixelRepresentation--//
public static void NifStream(out PixelRepresentation val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PixelRepresentation)temp; }
public static void NifStream(PixelRepresentation val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PixelRepresentation val) { switch (val) {
	case PixelRepresentation.PX_REP_NORM_INT: return "PX_REP_NORM_INT";
	case PixelRepresentation.PX_REP_HALF: return "PX_REP_HALF";
	case PixelRepresentation.PX_REP_FLOAT: return "PX_REP_FLOAT";
	case PixelRepresentation.PX_REP_INDEX: return "PX_REP_INDEX";
	case PixelRepresentation.PX_REP_COMPRESSED: return "PX_REP_COMPRESSED";
	case PixelRepresentation.PX_REP_UNKNOWN: return "PX_REP_UNKNOWN";
	case PixelRepresentation.PX_REP_INT: return "PX_REP_INT";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the color depth in an NiTexture. */
public enum PixelLayout : uint {
	PX_LAY_PALETTIZED_8 = 0, /*!< Texture is in 8-bit palettized format. */
	PX_LAY_HIGH_COLOR_16 = 1, /*!< Texture is in 16-bit high color format. */
	PX_LAY_TRUE_COLOR_32 = 2, /*!< Texture is in 32-bit true color format. */
	PX_LAY_COMPRESSED = 3, /*!< Texture is compressed. */
	PX_LAY_BUMPMAP = 4, /*!< Texture is a grayscale bump map. */
	PX_LAY_PALETTIZED_4 = 5, /*!< Texture is in 4-bit palettized format. */
	PX_LAY_DEFAULT = 6, /*!< Use default setting. */
	PX_LAY_SINGLE_COLOR_8 = 7, /*!< PX_LAY_SINGLE_COLOR_8 */
	PX_LAY_SINGLE_COLOR_16 = 8, /*!< PX_LAY_SINGLE_COLOR_16 */
	PX_LAY_SINGLE_COLOR_32 = 9, /*!< PX_LAY_SINGLE_COLOR_32 */
	PX_LAY_DOUBLE_COLOR_32 = 10, /*!< PX_LAY_DOUBLE_COLOR_32 */
	PX_LAY_DOUBLE_COLOR_64 = 11, /*!< PX_LAY_DOUBLE_COLOR_64 */
	PX_LAY_FLOAT_COLOR_32 = 12, /*!< PX_LAY_FLOAT_COLOR_32 */
	PX_LAY_FLOAT_COLOR_64 = 13, /*!< PX_LAY_FLOAT_COLOR_64 */
	PX_LAY_FLOAT_COLOR_128 = 14, /*!< PX_LAY_FLOAT_COLOR_128 */
	PX_LAY_SINGLE_COLOR_4 = 15, /*!< PX_LAY_SINGLE_COLOR_4 */
	PX_LAY_DEPTH_24_X8 = 16, /*!< PX_LAY_DEPTH_24_X8 */
}
static partial class Nif { //--PixelLayout--//
public static void NifStream(out PixelLayout val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PixelLayout)temp; }
public static void NifStream(PixelLayout val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PixelLayout val) { switch (val) {
	case PixelLayout.PX_LAY_PALETTIZED_8: return "PX_LAY_PALETTIZED_8";
	case PixelLayout.PX_LAY_HIGH_COLOR_16: return "PX_LAY_HIGH_COLOR_16";
	case PixelLayout.PX_LAY_TRUE_COLOR_32: return "PX_LAY_TRUE_COLOR_32";
	case PixelLayout.PX_LAY_COMPRESSED: return "PX_LAY_COMPRESSED";
	case PixelLayout.PX_LAY_BUMPMAP: return "PX_LAY_BUMPMAP";
	case PixelLayout.PX_LAY_PALETTIZED_4: return "PX_LAY_PALETTIZED_4";
	case PixelLayout.PX_LAY_DEFAULT: return "PX_LAY_DEFAULT";
	case PixelLayout.PX_LAY_SINGLE_COLOR_8: return "PX_LAY_SINGLE_COLOR_8";
	case PixelLayout.PX_LAY_SINGLE_COLOR_16: return "PX_LAY_SINGLE_COLOR_16";
	case PixelLayout.PX_LAY_SINGLE_COLOR_32: return "PX_LAY_SINGLE_COLOR_32";
	case PixelLayout.PX_LAY_DOUBLE_COLOR_32: return "PX_LAY_DOUBLE_COLOR_32";
	case PixelLayout.PX_LAY_DOUBLE_COLOR_64: return "PX_LAY_DOUBLE_COLOR_64";
	case PixelLayout.PX_LAY_FLOAT_COLOR_32: return "PX_LAY_FLOAT_COLOR_32";
	case PixelLayout.PX_LAY_FLOAT_COLOR_64: return "PX_LAY_FLOAT_COLOR_64";
	case PixelLayout.PX_LAY_FLOAT_COLOR_128: return "PX_LAY_FLOAT_COLOR_128";
	case PixelLayout.PX_LAY_SINGLE_COLOR_4: return "PX_LAY_SINGLE_COLOR_4";
	case PixelLayout.PX_LAY_DEPTH_24_X8: return "PX_LAY_DEPTH_24_X8";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes how mipmaps are handled in an NiTexture. */
public enum MipMapFormat : uint {
	MIP_FMT_NO = 0, /*!< Texture does not use mip maps. */
	MIP_FMT_YES = 1, /*!< Texture uses mip maps. */
	MIP_FMT_DEFAULT = 2, /*!< Use default setting. */
}
static partial class Nif { //--MipMapFormat--//
public static void NifStream(out MipMapFormat val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (MipMapFormat)temp; }
public static void NifStream(MipMapFormat val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(MipMapFormat val) { switch (val) {
	case MipMapFormat.MIP_FMT_NO: return "MIP_FMT_NO";
	case MipMapFormat.MIP_FMT_YES: return "MIP_FMT_YES";
	case MipMapFormat.MIP_FMT_DEFAULT: return "MIP_FMT_DEFAULT";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes how transparency is handled in an NiTexture. */
public enum AlphaFormat : uint {
	ALPHA_NONE = 0, /*!< No alpha. */
	ALPHA_BINARY = 1, /*!< 1-bit alpha. */
	ALPHA_SMOOTH = 2, /*!< Interpolated 4- or 8-bit alpha. */
	ALPHA_DEFAULT = 3, /*!< Use default setting. */
}
static partial class Nif { //--AlphaFormat--//
public static void NifStream(out AlphaFormat val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (AlphaFormat)temp; }
public static void NifStream(AlphaFormat val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(AlphaFormat val) { switch (val) {
	case AlphaFormat.ALPHA_NONE: return "ALPHA_NONE";
	case AlphaFormat.ALPHA_BINARY: return "ALPHA_BINARY";
	case AlphaFormat.ALPHA_SMOOTH: return "ALPHA_SMOOTH";
	case AlphaFormat.ALPHA_DEFAULT: return "ALPHA_DEFAULT";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes the availiable texture clamp modes, i.e. the behavior of UV mapping
 * outside the [0,1] range.
 */
public enum TexClampMode : uint {
	CLAMP_S_CLAMP_T = 0, /*!< Clamp in both directions. */
	CLAMP_S_WRAP_T = 1, /*!< Clamp in the S(U) direction but wrap in the T(V) direction. */
	WRAP_S_CLAMP_T = 2, /*!< Wrap in the S(U) direction but clamp in the T(V) direction. */
	WRAP_S_WRAP_T = 3, /*!< Wrap in both directions. */
}
static partial class Nif { //--TexClampMode--//
public static void NifStream(out TexClampMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (TexClampMode)temp; }
public static void NifStream(TexClampMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(TexClampMode val) { switch (val) {
	case TexClampMode.CLAMP_S_CLAMP_T: return "CLAMP_S_CLAMP_T";
	case TexClampMode.CLAMP_S_WRAP_T: return "CLAMP_S_WRAP_T";
	case TexClampMode.WRAP_S_CLAMP_T: return "WRAP_S_CLAMP_T";
	case TexClampMode.WRAP_S_WRAP_T: return "WRAP_S_WRAP_T";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes the availiable texture filter modes, i.e. the way the pixels in a
 * texture are displayed on screen.
 */
public enum TexFilterMode : uint {
	FILTER_NEAREST = 0, /*!< Nearest neighbor. Uses nearest texel with no mipmapping. */
	FILTER_BILERP = 1, /*!< Bilinear. Linear interpolation with no mipmapping. */
	FILTER_TRILERP = 2, /*!< Trilinear. Linear intepolation between 8 texels (4 nearest texels between 2 nearest mip levels). */
	FILTER_NEAREST_MIPNEAREST = 3, /*!< Nearest texel on nearest mip level. */
	FILTER_NEAREST_MIPLERP = 4, /*!< Linear interpolates nearest texel between two nearest mip levels. */
	FILTER_BILERP_MIPNEAREST = 5, /*!< Linear interpolates on nearest mip level. */
	FILTER_ANISOTROPIC = 6, /*!< Anisotropic filtering. One or many trilinear samples depending on anisotropy. */
}
static partial class Nif { //--TexFilterMode--//
public static void NifStream(out TexFilterMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (TexFilterMode)temp; }
public static void NifStream(TexFilterMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(TexFilterMode val) { switch (val) {
	case TexFilterMode.FILTER_NEAREST: return "FILTER_NEAREST";
	case TexFilterMode.FILTER_BILERP: return "FILTER_BILERP";
	case TexFilterMode.FILTER_TRILERP: return "FILTER_TRILERP";
	case TexFilterMode.FILTER_NEAREST_MIPNEAREST: return "FILTER_NEAREST_MIPNEAREST";
	case TexFilterMode.FILTER_NEAREST_MIPLERP: return "FILTER_NEAREST_MIPLERP";
	case TexFilterMode.FILTER_BILERP_MIPNEAREST: return "FILTER_BILERP_MIPNEAREST";
	case TexFilterMode.FILTER_ANISOTROPIC: return "FILTER_ANISOTROPIC";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes how to apply vertex colors for NiVertexColorProperty. */
public enum VertMode : uint {
	VERT_MODE_SRC_IGNORE = 0, /*!< Emissive, ambient, and diffuse colors are all specified by the NiMaterialProperty. */
	VERT_MODE_SRC_EMISSIVE = 1, /*!< Emissive colors are specified by the source vertex colors. Ambient+Diffuse are specified by the NiMaterialProperty. */
	VERT_MODE_SRC_AMB_DIF = 2, /*!< Ambient+Diffuse colors are specified by the source vertex colors. Emissive is specified by the NiMaterialProperty. (Default) */
}
static partial class Nif { //--VertMode--//
public static void NifStream(out VertMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (VertMode)temp; }
public static void NifStream(VertMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(VertMode val) { switch (val) {
	case VertMode.VERT_MODE_SRC_IGNORE: return "VERT_MODE_SRC_IGNORE";
	case VertMode.VERT_MODE_SRC_EMISSIVE: return "VERT_MODE_SRC_EMISSIVE";
	case VertMode.VERT_MODE_SRC_AMB_DIF: return "VERT_MODE_SRC_AMB_DIF";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes which lighting equation components influence the final vertex color
 * for NiVertexColorProperty.
 */
public enum LightMode : uint {
	LIGHT_MODE_EMISSIVE = 0, /*!< Emissive. */
	LIGHT_MODE_EMI_AMB_DIF = 1, /*!< Emissive + Ambient + Diffuse. (Default) */
}
static partial class Nif { //--LightMode--//
public static void NifStream(out LightMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (LightMode)temp; }
public static void NifStream(LightMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(LightMode val) { switch (val) {
	case LightMode.LIGHT_MODE_EMISSIVE: return "LIGHT_MODE_EMISSIVE";
	case LightMode.LIGHT_MODE_EMI_AMB_DIF: return "LIGHT_MODE_EMI_AMB_DIF";
	default: return $"Invalid Value! - {val}";
}}}

/*! The animation cyle behavior. */
public enum CycleType : uint {
	CYCLE_LOOP = 0, /*!< Loop */
	CYCLE_REVERSE = 1, /*!< Reverse */
	CYCLE_CLAMP = 2, /*!< Clamp */
}
static partial class Nif { //--CycleType--//
public static void NifStream(out CycleType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (CycleType)temp; }
public static void NifStream(CycleType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(CycleType val) { switch (val) {
	case CycleType.CYCLE_LOOP: return "CYCLE_LOOP";
	case CycleType.CYCLE_REVERSE: return "CYCLE_REVERSE";
	case CycleType.CYCLE_CLAMP: return "CYCLE_CLAMP";
	default: return $"Invalid Value! - {val}";
}}}

/*! The force field type. */
public enum FieldType : uint {
	FIELD_WIND = 0, /*!< Wind (fixed direction) */
	FIELD_POINT = 1, /*!< Point (fixed origin) */
}
static partial class Nif { //--FieldType--//
public static void NifStream(out FieldType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (FieldType)temp; }
public static void NifStream(FieldType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(FieldType val) { switch (val) {
	case FieldType.FIELD_WIND: return "FIELD_WIND";
	case FieldType.FIELD_POINT: return "FIELD_POINT";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Determines the way the billboard will react to the camera.
 *         Billboard mode is stored in lowest 3 bits although Oblivion vanilla nifs
 * uses values higher than 7.
 */
public enum BillboardMode : uint {
	ALWAYS_FACE_CAMERA = 0, /*!< Align billboard and camera forward vector. Minimized rotation. */
	ROTATE_ABOUT_UP = 1, /*!< Align billboard and camera forward vector while allowing rotation around the up axis. */
	RIGID_FACE_CAMERA = 2, /*!< Align billboard and camera forward vector. Non-minimized rotation. */
	ALWAYS_FACE_CENTER = 3, /*!< Billboard forward vector always faces camera ceneter. Minimized rotation. */
	RIGID_FACE_CENTER = 4, /*!< Billboard forward vector always faces camera ceneter. Non-minimized rotation. */
	BSROTATE_ABOUT_UP = 5, /*!< The billboard will only rotate around its local Z axis (it always stays in its local X-Y plane). */
	ROTATE_ABOUT_UP2 = 9, /*!< The billboard will only rotate around the up axis (same as ROTATE_ABOUT_UP?). */
}
static partial class Nif { //--BillboardMode--//
public static void NifStream(out BillboardMode val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (BillboardMode)temp; }
public static void NifStream(BillboardMode val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(BillboardMode val) { switch (val) {
	case BillboardMode.ALWAYS_FACE_CAMERA: return "ALWAYS_FACE_CAMERA";
	case BillboardMode.ROTATE_ABOUT_UP: return "ROTATE_ABOUT_UP";
	case BillboardMode.RIGID_FACE_CAMERA: return "RIGID_FACE_CAMERA";
	case BillboardMode.ALWAYS_FACE_CENTER: return "ALWAYS_FACE_CENTER";
	case BillboardMode.RIGID_FACE_CENTER: return "RIGID_FACE_CENTER";
	case BillboardMode.BSROTATE_ABOUT_UP: return "BSROTATE_ABOUT_UP";
	case BillboardMode.ROTATE_ABOUT_UP2: return "ROTATE_ABOUT_UP2";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes stencil buffer test modes for NiStencilProperty. */
public enum StencilCompareMode : uint {
	TEST_NEVER = 0, /*!< Always false. Ref value is ignored. */
	TEST_LESS = 1, /*!< VRef ‹ VBuf */
	TEST_EQUAL = 2, /*!< VRef = VBuf */
	TEST_LESS_EQUAL = 3, /*!< VRef ≤ VBuf */
	TEST_GREATER = 4, /*!< VRef › VBuf */
	TEST_NOT_EQUAL = 5, /*!< VRef ≠ VBuf */
	TEST_GREATER_EQUAL = 6, /*!< VRef ≥ VBuf */
	TEST_ALWAYS = 7, /*!< Always true. Buffer is ignored. */
}
static partial class Nif { //--StencilCompareMode--//
public static void NifStream(out StencilCompareMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (StencilCompareMode)temp; }
public static void NifStream(StencilCompareMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(StencilCompareMode val) { switch (val) {
	case StencilCompareMode.TEST_NEVER: return "TEST_NEVER";
	case StencilCompareMode.TEST_LESS: return "TEST_LESS";
	case StencilCompareMode.TEST_EQUAL: return "TEST_EQUAL";
	case StencilCompareMode.TEST_LESS_EQUAL: return "TEST_LESS_EQUAL";
	case StencilCompareMode.TEST_GREATER: return "TEST_GREATER";
	case StencilCompareMode.TEST_NOT_EQUAL: return "TEST_NOT_EQUAL";
	case StencilCompareMode.TEST_GREATER_EQUAL: return "TEST_GREATER_EQUAL";
	case StencilCompareMode.TEST_ALWAYS: return "TEST_ALWAYS";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes the actions which can occur as a result of tests for
 * NiStencilProperty.
 */
public enum StencilAction : uint {
	ACTION_KEEP = 0, /*!< Keep the current value in the stencil buffer. */
	ACTION_ZERO = 1, /*!< Write zero to the stencil buffer. */
	ACTION_REPLACE = 2, /*!< Write the reference value to the stencil buffer. */
	ACTION_INCREMENT = 3, /*!< Increment the value in the stencil buffer. */
	ACTION_DECREMENT = 4, /*!< Decrement the value in the stencil buffer. */
	ACTION_INVERT = 5, /*!< Bitwise invert the value in the stencil buffer. */
}
static partial class Nif { //--StencilAction--//
public static void NifStream(out StencilAction val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (StencilAction)temp; }
public static void NifStream(StencilAction val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(StencilAction val) { switch (val) {
	case StencilAction.ACTION_KEEP: return "ACTION_KEEP";
	case StencilAction.ACTION_ZERO: return "ACTION_ZERO";
	case StencilAction.ACTION_REPLACE: return "ACTION_REPLACE";
	case StencilAction.ACTION_INCREMENT: return "ACTION_INCREMENT";
	case StencilAction.ACTION_DECREMENT: return "ACTION_DECREMENT";
	case StencilAction.ACTION_INVERT: return "ACTION_INVERT";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the face culling options for NiStencilProperty. */
public enum StencilDrawMode : uint {
	DRAW_CCW_OR_BOTH = 0, /*!< Application default, chooses between DRAW_CCW or DRAW_BOTH. */
	DRAW_CCW = 1, /*!< Draw only the triangles whose vertices are ordered CCW with respect to the viewer. (Standard behavior) */
	DRAW_CW = 2, /*!< Draw only the triangles whose vertices are ordered CW with respect to the viewer. (Effectively flips faces) */
	DRAW_BOTH = 3, /*!< Draw all triangles, regardless of orientation. (Effectively force double-sided) */
}
static partial class Nif { //--StencilDrawMode--//
public static void NifStream(out StencilDrawMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (StencilDrawMode)temp; }
public static void NifStream(StencilDrawMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(StencilDrawMode val) { switch (val) {
	case StencilDrawMode.DRAW_CCW_OR_BOTH: return "DRAW_CCW_OR_BOTH";
	case StencilDrawMode.DRAW_CCW: return "DRAW_CCW";
	case StencilDrawMode.DRAW_CW: return "DRAW_CW";
	case StencilDrawMode.DRAW_BOTH: return "DRAW_BOTH";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes Z-buffer test modes for NiZBufferProperty.
 *         "Less than" = closer to camera, "Greater than" = further from camera.
 */
public enum ZCompareMode : uint {
	ZCOMP_ALWAYS = 0, /*!< Always true. Buffer is ignored. */
	ZCOMP_LESS = 1, /*!< VRef ‹ VBuf */
	ZCOMP_EQUAL = 2, /*!< VRef = VBuf */
	ZCOMP_LESS_EQUAL = 3, /*!< VRef ≤ VBuf */
	ZCOMP_GREATER = 4, /*!< VRef › VBuf */
	ZCOMP_NOT_EQUAL = 5, /*!< VRef ≠ VBuf */
	ZCOMP_GREATER_EQUAL = 6, /*!< VRef ≥ VBuf */
	ZCOMP_NEVER = 7, /*!< Always false. Ref value is ignored. */
}
static partial class Nif { //--ZCompareMode--//
public static void NifStream(out ZCompareMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (ZCompareMode)temp; }
public static void NifStream(ZCompareMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(ZCompareMode val) { switch (val) {
	case ZCompareMode.ZCOMP_ALWAYS: return "ZCOMP_ALWAYS";
	case ZCompareMode.ZCOMP_LESS: return "ZCOMP_LESS";
	case ZCompareMode.ZCOMP_EQUAL: return "ZCOMP_EQUAL";
	case ZCompareMode.ZCOMP_LESS_EQUAL: return "ZCOMP_LESS_EQUAL";
	case ZCompareMode.ZCOMP_GREATER: return "ZCOMP_GREATER";
	case ZCompareMode.ZCOMP_NOT_EQUAL: return "ZCOMP_NOT_EQUAL";
	case ZCompareMode.ZCOMP_GREATER_EQUAL: return "ZCOMP_GREATER_EQUAL";
	case ZCompareMode.ZCOMP_NEVER: return "ZCOMP_NEVER";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Havok, based on hkpMotion::MotionType. Motion type of a rigid body
 * determines what happens when it is simulated.
 */
public enum hkMotionType : uint {
	MO_SYS_INVALID = 0, /*!< Invalid */
	MO_SYS_DYNAMIC = 1, /*!< A fully-simulated, movable rigid body. At construction time the engine checks the input inertia and selects MO_SYS_SPHERE_INERTIA or MO_SYS_BOX_INERTIA as appropriate. */
	MO_SYS_SPHERE_INERTIA = 2, /*!< Simulation is performed using a sphere inertia tensor. */
	MO_SYS_SPHERE_STABILIZED = 3, /*!< This is the same as MO_SYS_SPHERE_INERTIA, except that simulation of the rigid body is "softened". */
	MO_SYS_BOX_INERTIA = 4, /*!< Simulation is performed using a box inertia tensor. */
	MO_SYS_BOX_STABILIZED = 5, /*!< This is the same as MO_SYS_BOX_INERTIA, except that simulation of the rigid body is "softened". */
	MO_SYS_KEYFRAMED = 6, /*!< Simulation is not performed as a normal rigid body. The keyframed rigid body has an infinite mass when viewed by the rest of the system. (used for creatures) */
	MO_SYS_FIXED = 7, /*!< This motion type is used for the static elements of a game scene, e.g. the landscape. Faster than MO_SYS_KEYFRAMED at velocity 0. (used for weapons) */
	MO_SYS_THIN_BOX = 8, /*!< A box inertia motion which is optimized for thin boxes and has less stability problems */
	MO_SYS_CHARACTER = 9, /*!< A specialized motion used for character controllers */
}
static partial class Nif { //--hkMotionType--//
public static void NifStream(out hkMotionType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (hkMotionType)temp; }
public static void NifStream(hkMotionType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(hkMotionType val) { switch (val) {
	case hkMotionType.MO_SYS_INVALID: return "MO_SYS_INVALID";
	case hkMotionType.MO_SYS_DYNAMIC: return "MO_SYS_DYNAMIC";
	case hkMotionType.MO_SYS_SPHERE_INERTIA: return "MO_SYS_SPHERE_INERTIA";
	case hkMotionType.MO_SYS_SPHERE_STABILIZED: return "MO_SYS_SPHERE_STABILIZED";
	case hkMotionType.MO_SYS_BOX_INERTIA: return "MO_SYS_BOX_INERTIA";
	case hkMotionType.MO_SYS_BOX_STABILIZED: return "MO_SYS_BOX_STABILIZED";
	case hkMotionType.MO_SYS_KEYFRAMED: return "MO_SYS_KEYFRAMED";
	case hkMotionType.MO_SYS_FIXED: return "MO_SYS_FIXED";
	case hkMotionType.MO_SYS_THIN_BOX: return "MO_SYS_THIN_BOX";
	case hkMotionType.MO_SYS_CHARACTER: return "MO_SYS_CHARACTER";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Havok, based on hkpRigidBodyDeactivator::DeactivatorType.
 *         Deactivator Type determines which mechanism Havok will use to classify
 * the body as deactivated.
 */
public enum hkDeactivatorType : uint {
	DEACTIVATOR_INVALID = 0, /*!< Invalid */
	DEACTIVATOR_NEVER = 1, /*!< This will force the rigid body to never deactivate. */
	DEACTIVATOR_SPATIAL = 2, /*!< Tells Havok to use a spatial deactivation scheme. This makes use of high and low frequencies of positional motion to determine when deactivation should occur. */
}
static partial class Nif { //--hkDeactivatorType--//
public static void NifStream(out hkDeactivatorType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (hkDeactivatorType)temp; }
public static void NifStream(hkDeactivatorType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(hkDeactivatorType val) { switch (val) {
	case hkDeactivatorType.DEACTIVATOR_INVALID: return "DEACTIVATOR_INVALID";
	case hkDeactivatorType.DEACTIVATOR_NEVER: return "DEACTIVATOR_NEVER";
	case hkDeactivatorType.DEACTIVATOR_SPATIAL: return "DEACTIVATOR_SPATIAL";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Havok, based on hkpRigidBodyCinfo::SolverDeactivation.
 *         A list of possible solver deactivation settings. This value defines how
 * aggressively the solver deactivates objects.
 *         Note: Solver deactivation does not save CPU, but reduces creeping of
 * movable objects in a pile quite dramatically.
 */
public enum hkSolverDeactivation : uint {
	SOLVER_DEACTIVATION_INVALID = 0, /*!< Invalid */
	SOLVER_DEACTIVATION_OFF = 1, /*!< No solver deactivation. */
	SOLVER_DEACTIVATION_LOW = 2, /*!< Very conservative deactivation, typically no visible artifacts. */
	SOLVER_DEACTIVATION_MEDIUM = 3, /*!< Normal deactivation, no serious visible artifacts in most cases. */
	SOLVER_DEACTIVATION_HIGH = 4, /*!< Fast deactivation, visible artifacts. */
	SOLVER_DEACTIVATION_MAX = 5, /*!< Very fast deactivation, visible artifacts. */
}
static partial class Nif { //--hkSolverDeactivation--//
public static void NifStream(out hkSolverDeactivation val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (hkSolverDeactivation)temp; }
public static void NifStream(hkSolverDeactivation val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(hkSolverDeactivation val) { switch (val) {
	case hkSolverDeactivation.SOLVER_DEACTIVATION_INVALID: return "SOLVER_DEACTIVATION_INVALID";
	case hkSolverDeactivation.SOLVER_DEACTIVATION_OFF: return "SOLVER_DEACTIVATION_OFF";
	case hkSolverDeactivation.SOLVER_DEACTIVATION_LOW: return "SOLVER_DEACTIVATION_LOW";
	case hkSolverDeactivation.SOLVER_DEACTIVATION_MEDIUM: return "SOLVER_DEACTIVATION_MEDIUM";
	case hkSolverDeactivation.SOLVER_DEACTIVATION_HIGH: return "SOLVER_DEACTIVATION_HIGH";
	case hkSolverDeactivation.SOLVER_DEACTIVATION_MAX: return "SOLVER_DEACTIVATION_MAX";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Havok, based on hkpCollidableQualityType. Describes the priority and
 * quality of collisions for a body,
 *             e.g. you may expect critical game play objects to have solid high-
 * priority collisions so that they never sink into ground,
 *             or may allow penetrations for visual debris objects.
 *         Notes:
 *             - Fixed and keyframed objects cannot interact with each other.
 *             - Debris can interpenetrate but still responds to Bullet hits.
 *             - Critical objects are forced to not interpenetrate.
 *             - Moving objects can interpenetrate slightly with other Moving or
 * Debris objects but nothing else.
 */
public enum hkQualityType : uint {
	MO_QUAL_INVALID = 0, /*!< Automatically assigned to MO_QUAL_FIXED, MO_QUAL_KEYFRAMED or MO_QUAL_DEBRIS */
	MO_QUAL_FIXED = 1, /*!< Static body. */
	MO_QUAL_KEYFRAMED = 2, /*!< Animated body with infinite mass. */
	MO_QUAL_DEBRIS = 3, /*!< Low importance bodies adding visual detail. */
	MO_QUAL_MOVING = 4, /*!< Moving bodies which should not penetrate or leave the world, but can. */
	MO_QUAL_CRITICAL = 5, /*!< Gameplay critical bodies which cannot penetrate or leave the world under any circumstance. */
	MO_QUAL_BULLET = 6, /*!< Fast-moving bodies, such as projectiles. */
	MO_QUAL_USER = 7, /*!< For user. */
	MO_QUAL_CHARACTER = 8, /*!< For use with rigid body character controllers. */
	MO_QUAL_KEYFRAMED_REPORT = 9, /*!< Moving bodies with infinite mass which should report contact points and TOI collisions against all other bodies. */
}
static partial class Nif { //--hkQualityType--//
public static void NifStream(out hkQualityType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (hkQualityType)temp; }
public static void NifStream(hkQualityType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(hkQualityType val) { switch (val) {
	case hkQualityType.MO_QUAL_INVALID: return "MO_QUAL_INVALID";
	case hkQualityType.MO_QUAL_FIXED: return "MO_QUAL_FIXED";
	case hkQualityType.MO_QUAL_KEYFRAMED: return "MO_QUAL_KEYFRAMED";
	case hkQualityType.MO_QUAL_DEBRIS: return "MO_QUAL_DEBRIS";
	case hkQualityType.MO_QUAL_MOVING: return "MO_QUAL_MOVING";
	case hkQualityType.MO_QUAL_CRITICAL: return "MO_QUAL_CRITICAL";
	case hkQualityType.MO_QUAL_BULLET: return "MO_QUAL_BULLET";
	case hkQualityType.MO_QUAL_USER: return "MO_QUAL_USER";
	case hkQualityType.MO_QUAL_CHARACTER: return "MO_QUAL_CHARACTER";
	case hkQualityType.MO_QUAL_KEYFRAMED_REPORT: return "MO_QUAL_KEYFRAMED_REPORT";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the type of gravitational force. */
public enum ForceType : uint {
	FORCE_PLANAR = 0, /*!< FORCE_PLANAR */
	FORCE_SPHERICAL = 1, /*!< FORCE_SPHERICAL */
	FORCE_UNKNOWN = 2, /*!< FORCE_UNKNOWN */
}
static partial class Nif { //--ForceType--//
public static void NifStream(out ForceType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (ForceType)temp; }
public static void NifStream(ForceType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(ForceType val) { switch (val) {
	case ForceType.FORCE_PLANAR: return "FORCE_PLANAR";
	case ForceType.FORCE_SPHERICAL: return "FORCE_SPHERICAL";
	case ForceType.FORCE_UNKNOWN: return "FORCE_UNKNOWN";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes which aspect of the NiTextureTransform the
 * NiTextureTransformController will modify.
 */
public enum TransformMember : uint {
	TT_TRANSLATE_U = 0, /*!< Control the translation of the U coordinates. */
	TT_TRANSLATE_V = 1, /*!< Control the translation of the V coordinates. */
	TT_ROTATE = 2, /*!< Control the rotation of the coordinates. */
	TT_SCALE_U = 3, /*!< Control the scale of the U coordinates. */
	TT_SCALE_V = 4, /*!< Control the scale of the V coordinates. */
}
static partial class Nif { //--TransformMember--//
public static void NifStream(out TransformMember val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (TransformMember)temp; }
public static void NifStream(TransformMember val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(TransformMember val) { switch (val) {
	case TransformMember.TT_TRANSLATE_U: return "TT_TRANSLATE_U";
	case TransformMember.TT_TRANSLATE_V: return "TT_TRANSLATE_V";
	case TransformMember.TT_ROTATE: return "TT_ROTATE";
	case TransformMember.TT_SCALE_U: return "TT_SCALE_U";
	case TransformMember.TT_SCALE_V: return "TT_SCALE_V";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the decay function of bomb forces. */
public enum DecayType : uint {
	DECAY_NONE = 0, /*!< No decay. */
	DECAY_LINEAR = 1, /*!< Linear decay. */
	DECAY_EXPONENTIAL = 2, /*!< Exponential decay. */
}
static partial class Nif { //--DecayType--//
public static void NifStream(out DecayType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (DecayType)temp; }
public static void NifStream(DecayType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(DecayType val) { switch (val) {
	case DecayType.DECAY_NONE: return "DECAY_NONE";
	case DecayType.DECAY_LINEAR: return "DECAY_LINEAR";
	case DecayType.DECAY_EXPONENTIAL: return "DECAY_EXPONENTIAL";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the symmetry type of bomb forces. */
public enum SymmetryType : uint {
	SPHERICAL_SYMMETRY = 0, /*!< Spherical Symmetry. */
	CYLINDRICAL_SYMMETRY = 1, /*!< Cylindrical Symmetry. */
	PLANAR_SYMMETRY = 2, /*!< Planar Symmetry. */
}
static partial class Nif { //--SymmetryType--//
public static void NifStream(out SymmetryType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (SymmetryType)temp; }
public static void NifStream(SymmetryType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(SymmetryType val) { switch (val) {
	case SymmetryType.SPHERICAL_SYMMETRY: return "SPHERICAL_SYMMETRY";
	case SymmetryType.CYLINDRICAL_SYMMETRY: return "CYLINDRICAL_SYMMETRY";
	case SymmetryType.PLANAR_SYMMETRY: return "PLANAR_SYMMETRY";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Controls the way the a particle mesh emitter determines the starting speed and
 * direction of the particles that are emitted.
 */
public enum VelocityType : uint {
	VELOCITY_USE_NORMALS = 0, /*!< Uses the normals of the meshes to determine staring velocity. */
	VELOCITY_USE_RANDOM = 1, /*!< Starts particles with a random velocity. */
	VELOCITY_USE_DIRECTION = 2, /*!< Uses the emission axis to determine initial particle direction? */
}
static partial class Nif { //--VelocityType--//
public static void NifStream(out VelocityType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (VelocityType)temp; }
public static void NifStream(VelocityType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(VelocityType val) { switch (val) {
	case VelocityType.VELOCITY_USE_NORMALS: return "VELOCITY_USE_NORMALS";
	case VelocityType.VELOCITY_USE_RANDOM: return "VELOCITY_USE_RANDOM";
	case VelocityType.VELOCITY_USE_DIRECTION: return "VELOCITY_USE_DIRECTION";
	default: return $"Invalid Value! - {val}";
}}}

/*! Controls which parts of the mesh that the particles are emitted from. */
public enum EmitFrom : uint {
	EMIT_FROM_VERTICES = 0, /*!< Emit particles from the vertices of the mesh. */
	EMIT_FROM_FACE_CENTER = 1, /*!< Emit particles from the center of the faces of the mesh. */
	EMIT_FROM_EDGE_CENTER = 2, /*!< Emit particles from the center of the edges of the mesh. */
	EMIT_FROM_FACE_SURFACE = 3, /*!< Perhaps randomly emit particles from anywhere on the faces of the mesh? */
	EMIT_FROM_EDGE_SURFACE = 4, /*!< Perhaps randomly emit particles from anywhere on the edges of the mesh? */
}
static partial class Nif { //--EmitFrom--//
public static void NifStream(out EmitFrom val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (EmitFrom)temp; }
public static void NifStream(EmitFrom val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(EmitFrom val) { switch (val) {
	case EmitFrom.EMIT_FROM_VERTICES: return "EMIT_FROM_VERTICES";
	case EmitFrom.EMIT_FROM_FACE_CENTER: return "EMIT_FROM_FACE_CENTER";
	case EmitFrom.EMIT_FROM_EDGE_CENTER: return "EMIT_FROM_EDGE_CENTER";
	case EmitFrom.EMIT_FROM_FACE_SURFACE: return "EMIT_FROM_FACE_SURFACE";
	case EmitFrom.EMIT_FROM_EDGE_SURFACE: return "EMIT_FROM_EDGE_SURFACE";
	default: return $"Invalid Value! - {val}";
}}}

/*! The type of information that is stored in a texture used by an NiTextureEffect. */
public enum TextureType : uint {
	TEX_PROJECTED_LIGHT = 0, /*!< Apply a projected light texture. Each light effect is summed before multiplying by the base texture. */
	TEX_PROJECTED_SHADOW = 1, /*!< Apply a projected shadow texture. Each shadow effect is multiplied by the base texture. */
	TEX_ENVIRONMENT_MAP = 2, /*!< Apply an environment map texture. Added to the base texture and light/shadow/decal maps. */
	TEX_FOG_MAP = 3, /*!< Apply a fog map texture. Alpha channel is used to blend the color channel with the base texture. */
}
static partial class Nif { //--TextureType--//
public static void NifStream(out TextureType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (TextureType)temp; }
public static void NifStream(TextureType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(TextureType val) { switch (val) {
	case TextureType.TEX_PROJECTED_LIGHT: return "TEX_PROJECTED_LIGHT";
	case TextureType.TEX_PROJECTED_SHADOW: return "TEX_PROJECTED_SHADOW";
	case TextureType.TEX_ENVIRONMENT_MAP: return "TEX_ENVIRONMENT_MAP";
	case TextureType.TEX_FOG_MAP: return "TEX_FOG_MAP";
	default: return $"Invalid Value! - {val}";
}}}

/*! Determines the way that UV texture coordinates are generated. */
public enum CoordGenType : uint {
	CG_WORLD_PARALLEL = 0, /*!< Use planar mapping. */
	CG_WORLD_PERSPECTIVE = 1, /*!< Use perspective mapping. */
	CG_SPHERE_MAP = 2, /*!< Use spherical mapping. */
	CG_SPECULAR_CUBE_MAP = 3, /*!< Use specular cube mapping. For NiSourceCubeMap only. */
	CG_DIFFUSE_CUBE_MAP = 4, /*!< Use diffuse cube mapping. For NiSourceCubeMap only. */
}
static partial class Nif { //--CoordGenType--//
public static void NifStream(out CoordGenType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (CoordGenType)temp; }
public static void NifStream(CoordGenType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(CoordGenType val) { switch (val) {
	case CoordGenType.CG_WORLD_PARALLEL: return "CG_WORLD_PARALLEL";
	case CoordGenType.CG_WORLD_PERSPECTIVE: return "CG_WORLD_PERSPECTIVE";
	case CoordGenType.CG_SPHERE_MAP: return "CG_SPHERE_MAP";
	case CoordGenType.CG_SPECULAR_CUBE_MAP: return "CG_SPECULAR_CUBE_MAP";
	case CoordGenType.CG_DIFFUSE_CUBE_MAP: return "CG_DIFFUSE_CUBE_MAP";
	default: return $"Invalid Value! - {val}";
}}}

public enum EndianType : uint {
	ENDIAN_BIG = 0, /*!< The numbers are stored in big endian format, such as those used by PowerPC Mac processors. */
	ENDIAN_LITTLE = 1, /*!< The numbers are stored in little endian format, such as those used by Intel and AMD x86 processors. */
}
static partial class Nif { //--EndianType--//
public static void NifStream(out EndianType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (EndianType)temp; }
public static void NifStream(EndianType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(EndianType val) { switch (val) {
	case EndianType.ENDIAN_BIG: return "ENDIAN_BIG";
	case EndianType.ENDIAN_LITTLE: return "ENDIAN_LITTLE";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Used by NiMaterialColorControllers to select which type of color in the
 * controlled object that will be animated.
 */
public enum MaterialColor : uint {
	TC_AMBIENT = 0, /*!< Control the ambient color. */
	TC_DIFFUSE = 1, /*!< Control the diffuse color. */
	TC_SPECULAR = 2, /*!< Control the specular color. */
	TC_SELF_ILLUM = 3, /*!< Control the self illumination color. */
}
static partial class Nif { //--MaterialColor--//
public static void NifStream(out MaterialColor val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (MaterialColor)temp; }
public static void NifStream(MaterialColor val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(MaterialColor val) { switch (val) {
	case MaterialColor.TC_AMBIENT: return "TC_AMBIENT";
	case MaterialColor.TC_DIFFUSE: return "TC_DIFFUSE";
	case MaterialColor.TC_SPECULAR: return "TC_SPECULAR";
	case MaterialColor.TC_SELF_ILLUM: return "TC_SELF_ILLUM";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Used by NiLightColorControllers to select which type of color in the controlled
 * object that will be animated.
 */
public enum LightColor : uint {
	LC_DIFFUSE = 0, /*!< Control the diffuse color. */
	LC_AMBIENT = 1, /*!< Control the ambient color. */
}
static partial class Nif { //--LightColor--//
public static void NifStream(out LightColor val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (LightColor)temp; }
public static void NifStream(LightColor val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(LightColor val) { switch (val) {
	case LightColor.LC_DIFFUSE: return "LC_DIFFUSE";
	case LightColor.LC_AMBIENT: return "LC_AMBIENT";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Used by NiGeometryData to control the volatility of the mesh.
 *         Consistency Type is masked to only the upper 4 bits (0xF000). Dirty mask
 * is the lower 12 (0x0FFF) but only used at runtime.
 */
public enum ConsistencyType : uint {
	CT_MUTABLE = 0x0000, /*!< Mutable Mesh */
	CT_STATIC = 0x4000, /*!< Static Mesh */
	CT_VOLATILE = 0x8000, /*!< Volatile Mesh */
}
static partial class Nif { //--ConsistencyType--//
public static void NifStream(out ConsistencyType val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (ConsistencyType)temp; }
public static void NifStream(ConsistencyType val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(ConsistencyType val) { switch (val) {
	case ConsistencyType.CT_MUTABLE: return "CT_MUTABLE";
	case ConsistencyType.CT_STATIC: return "CT_STATIC";
	case ConsistencyType.CT_VOLATILE: return "CT_VOLATILE";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes the way that NiSortAdjustNode modifies the sorting behavior for the
 * subtree below it.
 */
public enum SortingMode : uint {
	SORTING_INHERIT = 0, /*!< Inherit. Acts identical to NiNode. */
	SORTING_OFF = 1, /*!< Disables sort on all geometry under this node. */
}
static partial class Nif { //--SortingMode--//
public static void NifStream(out SortingMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (SortingMode)temp; }
public static void NifStream(SortingMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(SortingMode val) { switch (val) {
	case SortingMode.SORTING_INHERIT: return "SORTING_INHERIT";
	case SortingMode.SORTING_OFF: return "SORTING_OFF";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * The propagation mode controls scene graph traversal during collision detection
 * operations for NiCollisionData.
 */
public enum PropagationMode : uint {
	PROPAGATE_ON_SUCCESS = 0, /*!< Propagation only occurs as a result of a successful collision. */
	PROPAGATE_ON_FAILURE = 1, /*!< (Deprecated) Propagation only occurs as a result of a failed collision. */
	PROPAGATE_ALWAYS = 2, /*!< Propagation always occurs regardless of collision result. */
	PROPAGATE_NEVER = 3, /*!< Propagation never occurs regardless of collision result. */
}
static partial class Nif { //--PropagationMode--//
public static void NifStream(out PropagationMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PropagationMode)temp; }
public static void NifStream(PropagationMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PropagationMode val) { switch (val) {
	case PropagationMode.PROPAGATE_ON_SUCCESS: return "PROPAGATE_ON_SUCCESS";
	case PropagationMode.PROPAGATE_ON_FAILURE: return "PROPAGATE_ON_FAILURE";
	case PropagationMode.PROPAGATE_ALWAYS: return "PROPAGATE_ALWAYS";
	case PropagationMode.PROPAGATE_NEVER: return "PROPAGATE_NEVER";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * The collision mode controls the type of collision operation that is to take
 * place for NiCollisionData.
 */
public enum CollisionMode : uint {
	CM_USE_OBB = 0, /*!< Use Bounding Box */
	CM_USE_TRI = 1, /*!< Use Triangles */
	CM_USE_ABV = 2, /*!< Use Alternate Bounding Volumes */
	CM_NOTEST = 3, /*!< Indicates that no collision test should be made. */
	CM_USE_NIBOUND = 4, /*!< Use NiBound */
}
static partial class Nif { //--CollisionMode--//
public static void NifStream(out CollisionMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (CollisionMode)temp; }
public static void NifStream(CollisionMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(CollisionMode val) { switch (val) {
	case CollisionMode.CM_USE_OBB: return "CM_USE_OBB";
	case CollisionMode.CM_USE_TRI: return "CM_USE_TRI";
	case CollisionMode.CM_USE_ABV: return "CM_USE_ABV";
	case CollisionMode.CM_NOTEST: return "CM_NOTEST";
	case CollisionMode.CM_USE_NIBOUND: return "CM_USE_NIBOUND";
	default: return $"Invalid Value! - {val}";
}}}

public enum BoundVolumeType : uint {
	BASE_BV = 0xffffffff, /*!< Default */
	SPHERE_BV = 0, /*!< Sphere */
	BOX_BV = 1, /*!< Box */
	CAPSULE_BV = 2, /*!< Capsule */
	UNION_BV = 4, /*!< Union */
	HALFSPACE_BV = 5, /*!< Half Space */
}
static partial class Nif { //--BoundVolumeType--//
public static void NifStream(out BoundVolumeType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (BoundVolumeType)temp; }
public static void NifStream(BoundVolumeType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(BoundVolumeType val) { switch (val) {
	case BoundVolumeType.BASE_BV: return "BASE_BV";
	case BoundVolumeType.SPHERE_BV: return "SPHERE_BV";
	case BoundVolumeType.BOX_BV: return "BOX_BV";
	case BoundVolumeType.CAPSULE_BV: return "CAPSULE_BV";
	case BoundVolumeType.UNION_BV: return "UNION_BV";
	case BoundVolumeType.HALFSPACE_BV: return "HALFSPACE_BV";
	default: return $"Invalid Value! - {val}";
}}}

/*! Bethesda Havok. */
public enum hkResponseType : uint {
	RESPONSE_INVALID = 0, /*!< Invalid Response */
	RESPONSE_SIMPLE_CONTACT = 1, /*!< Do normal collision resolution */
	RESPONSE_REPORTING = 2, /*!< No collision resolution is performed but listeners are called */
	RESPONSE_NONE = 3, /*!< Do nothing, ignore all the results. */
}
static partial class Nif { //--hkResponseType--//
public static void NifStream(out hkResponseType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (hkResponseType)temp; }
public static void NifStream(hkResponseType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(hkResponseType val) { switch (val) {
	case hkResponseType.RESPONSE_INVALID: return "RESPONSE_INVALID";
	case hkResponseType.RESPONSE_SIMPLE_CONTACT: return "RESPONSE_SIMPLE_CONTACT";
	case hkResponseType.RESPONSE_REPORTING: return "RESPONSE_REPORTING";
	case hkResponseType.RESPONSE_NONE: return "RESPONSE_NONE";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Biped bodypart data used for visibility control of triangles.  Options are
 * Fallout 3, except where marked for Skyrim (uses SBP prefix)
 *         Skyrim BP names are listed only for vanilla names, different creatures
 * have different defnitions for naming.
 */
public enum BSDismemberBodyPartType : uint {
	BP_TORSO = 0, /*!< Torso */
	BP_HEAD = 1, /*!< Head */
	BP_HEAD2 = 2, /*!< Head 2 */
	BP_LEFTARM = 3, /*!< Left Arm */
	BP_LEFTARM2 = 4, /*!< Left Arm 2 */
	BP_RIGHTARM = 5, /*!< Right Arm */
	BP_RIGHTARM2 = 6, /*!< Right Arm 2 */
	BP_LEFTLEG = 7, /*!< Left Leg */
	BP_LEFTLEG2 = 8, /*!< Left Leg 2 */
	BP_LEFTLEG3 = 9, /*!< Left Leg 3 */
	BP_RIGHTLEG = 10, /*!< Right Leg */
	BP_RIGHTLEG2 = 11, /*!< Right Leg 2 */
	BP_RIGHTLEG3 = 12, /*!< Right Leg 3 */
	BP_BRAIN = 13, /*!< Brain */
	SBP_30_HEAD = 30, /*!< Skyrim, Head(Human), Body(Atronachs,Beasts), Mask(Dragonpriest) */
	SBP_31_HAIR = 31, /*!< Skyrim, Hair(human), Far(Dragon), Mask2(Dragonpriest),SkinnedFX(Spriggan) */
	SBP_32_BODY = 32, /*!< Skyrim, Main body, extras(Spriggan) */
	SBP_33_HANDS = 33, /*!< Skyrim, Hands L/R, BodyToo(Dragonpriest), Legs(Draugr), Arms(Giant) */
	SBP_34_FOREARMS = 34, /*!< Skyrim, Forearms L/R, Beard(Draugr) */
	SBP_35_AMULET = 35, /*!< Skyrim, Amulet */
	SBP_36_RING = 36, /*!< Skyrim, Ring */
	SBP_37_FEET = 37, /*!< Skyrim, Feet L/R */
	SBP_38_CALVES = 38, /*!< Skyrim, Calves L/R */
	SBP_39_SHIELD = 39, /*!< Skyrim, Shield */
	SBP_40_TAIL = 40, /*!< Skyrim, Tail(Argonian/Khajiit), Skeleton01(Dragon), FX01(AtronachStorm),FXMist (Dragonpriest), Spit(Chaurus,Spider),SmokeFins(IceWraith) */
	SBP_41_LONGHAIR = 41, /*!< Skyrim, Long Hair(Human), Skeleton02(Dragon),FXParticles(Dragonpriest) */
	SBP_42_CIRCLET = 42, /*!< Skyrim, Circlet(Human, MouthFireEffect(Dragon) */
	SBP_43_EARS = 43, /*!< Skyrim, Ears */
	SBP_44_DRAGON_BLOODHEAD_OR_MOD_MOUTH = 44, /*!< Skyrim, Bloodied dragon head, or NPC face/mouth */
	SBP_45_DRAGON_BLOODWINGL_OR_MOD_NECK = 45, /*!< Skyrim, Left Bloodied dragon wing, Saddle(Horse), or NPC cape, scarf, shawl, neck-tie, etc. */
	SBP_46_DRAGON_BLOODWINGR_OR_MOD_CHEST_PRIMARY = 46, /*!< Skyrim, Right Bloodied dragon wing, or NPC chest primary or outergarment */
	SBP_47_DRAGON_BLOODTAIL_OR_MOD_BACK = 47, /*!< Skyrim, Bloodied dragon tail, or NPC backpack/wings/... */
	SBP_48_MOD_MISC1 = 48, /*!< Anything that does not fit in the list */
	SBP_49_MOD_PELVIS_PRIMARY = 49, /*!< Pelvis primary or outergarment */
	SBP_50_DECAPITATEDHEAD = 50, /*!< Skyrim, Decapitated Head */
	SBP_51_DECAPITATE = 51, /*!< Skyrim, Decapitate, neck gore */
	SBP_52_MOD_PELVIS_SECONDARY = 52, /*!< Pelvis secondary or undergarment */
	SBP_53_MOD_LEG_RIGHT = 53, /*!< Leg primary or outergarment or right leg */
	SBP_54_MOD_LEG_LEFT = 54, /*!< Leg secondary or undergarment or left leg */
	SBP_55_MOD_FACE_JEWELRY = 55, /*!< Face alternate or jewelry */
	SBP_56_MOD_CHEST_SECONDARY = 56, /*!< Chest secondary or undergarment */
	SBP_57_MOD_SHOULDER = 57, /*!< Shoulder */
	SBP_58_MOD_ARM_LEFT = 58, /*!< Arm secondary or undergarment or left arm */
	SBP_59_MOD_ARM_RIGHT = 59, /*!< Arm primary or outergarment or right arm */
	SBP_60_MOD_MISC2 = 60, /*!< Anything that does not fit in the list */
	SBP_61_FX01 = 61, /*!< Skyrim, FX01(Humanoid) */
	BP_SECTIONCAP_HEAD = 101, /*!< Section Cap | Head */
	BP_SECTIONCAP_HEAD2 = 102, /*!< Section Cap | Head 2 */
	BP_SECTIONCAP_LEFTARM = 103, /*!< Section Cap | Left Arm */
	BP_SECTIONCAP_LEFTARM2 = 104, /*!< Section Cap | Left Arm 2 */
	BP_SECTIONCAP_RIGHTARM = 105, /*!< Section Cap | Right Arm */
	BP_SECTIONCAP_RIGHTARM2 = 106, /*!< Section Cap | Right Arm 2 */
	BP_SECTIONCAP_LEFTLEG = 107, /*!< Section Cap | Left Leg */
	BP_SECTIONCAP_LEFTLEG2 = 108, /*!< Section Cap | Left Leg 2 */
	BP_SECTIONCAP_LEFTLEG3 = 109, /*!< Section Cap | Left Leg 3 */
	BP_SECTIONCAP_RIGHTLEG = 110, /*!< Section Cap | Right Leg */
	BP_SECTIONCAP_RIGHTLEG2 = 111, /*!< Section Cap | Right Leg 2 */
	BP_SECTIONCAP_RIGHTLEG3 = 112, /*!< Section Cap | Right Leg 3 */
	BP_SECTIONCAP_BRAIN = 113, /*!< Section Cap | Brain */
	SBP_130_HEAD = 130, /*!< Skyrim, Head slot, use on full-face helmets */
	SBP_131_HAIR = 131, /*!< Skyrim, Hair slot 1, use on hoods */
	SBP_141_LONGHAIR = 141, /*!< Skyrim, Hair slot 2, use for longer hair */
	SBP_142_CIRCLET = 142, /*!< Skyrim, Circlet slot 1, use for circlets */
	SBP_143_EARS = 143, /*!< Skyrim, Ear slot */
	SBP_150_DECAPITATEDHEAD = 150, /*!< Skyrim, neck gore on head side */
	BP_TORSOCAP_HEAD = 201, /*!< Torso Cap | Head */
	BP_TORSOCAP_HEAD2 = 202, /*!< Torso Cap | Head 2 */
	BP_TORSOCAP_LEFTARM = 203, /*!< Torso Cap | Left Arm */
	BP_TORSOCAP_LEFTARM2 = 204, /*!< Torso Cap | Left Arm 2 */
	BP_TORSOCAP_RIGHTARM = 205, /*!< Torso Cap | Right Arm */
	BP_TORSOCAP_RIGHTARM2 = 206, /*!< Torso Cap | Right Arm 2 */
	BP_TORSOCAP_LEFTLEG = 207, /*!< Torso Cap | Left Leg */
	BP_TORSOCAP_LEFTLEG2 = 208, /*!< Torso Cap | Left Leg 2 */
	BP_TORSOCAP_LEFTLEG3 = 209, /*!< Torso Cap | Left Leg 3 */
	BP_TORSOCAP_RIGHTLEG = 210, /*!< Torso Cap | Right Leg */
	BP_TORSOCAP_RIGHTLEG2 = 211, /*!< Torso Cap | Right Leg 2 */
	BP_TORSOCAP_RIGHTLEG3 = 212, /*!< Torso Cap | Right Leg 3 */
	BP_TORSOCAP_BRAIN = 213, /*!< Torso Cap | Brain */
	SBP_230_HEAD = 230, /*!< Skyrim, Head slot, use for neck on character head */
	BP_TORSOSECTION_HEAD = 1000, /*!< Torso Section | Head */
	BP_TORSOSECTION_HEAD2 = 2000, /*!< Torso Section | Head 2 */
	BP_TORSOSECTION_LEFTARM = 3000, /*!< Torso Section | Left Arm */
	BP_TORSOSECTION_LEFTARM2 = 4000, /*!< Torso Section | Left Arm 2 */
	BP_TORSOSECTION_RIGHTARM = 5000, /*!< Torso Section | Right Arm */
	BP_TORSOSECTION_RIGHTARM2 = 6000, /*!< Torso Section | Right Arm 2 */
	BP_TORSOSECTION_LEFTLEG = 7000, /*!< Torso Section | Left Leg */
	BP_TORSOSECTION_LEFTLEG2 = 8000, /*!< Torso Section | Left Leg 2 */
	BP_TORSOSECTION_LEFTLEG3 = 9000, /*!< Torso Section | Left Leg 3 */
	BP_TORSOSECTION_RIGHTLEG = 10000, /*!< Torso Section | Right Leg */
	BP_TORSOSECTION_RIGHTLEG2 = 11000, /*!< Torso Section | Right Leg 2 */
	BP_TORSOSECTION_RIGHTLEG3 = 12000, /*!< Torso Section | Right Leg 3 */
	BP_TORSOSECTION_BRAIN = 13000, /*!< Torso Section | Brain */
}
static partial class Nif { //--BSDismemberBodyPartType--//
public static void NifStream(out BSDismemberBodyPartType val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (BSDismemberBodyPartType)temp; }
public static void NifStream(BSDismemberBodyPartType val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(BSDismemberBodyPartType val) { switch (val) {
	case BSDismemberBodyPartType.BP_TORSO: return "BP_TORSO";
	case BSDismemberBodyPartType.BP_HEAD: return "BP_HEAD";
	case BSDismemberBodyPartType.BP_HEAD2: return "BP_HEAD2";
	case BSDismemberBodyPartType.BP_LEFTARM: return "BP_LEFTARM";
	case BSDismemberBodyPartType.BP_LEFTARM2: return "BP_LEFTARM2";
	case BSDismemberBodyPartType.BP_RIGHTARM: return "BP_RIGHTARM";
	case BSDismemberBodyPartType.BP_RIGHTARM2: return "BP_RIGHTARM2";
	case BSDismemberBodyPartType.BP_LEFTLEG: return "BP_LEFTLEG";
	case BSDismemberBodyPartType.BP_LEFTLEG2: return "BP_LEFTLEG2";
	case BSDismemberBodyPartType.BP_LEFTLEG3: return "BP_LEFTLEG3";
	case BSDismemberBodyPartType.BP_RIGHTLEG: return "BP_RIGHTLEG";
	case BSDismemberBodyPartType.BP_RIGHTLEG2: return "BP_RIGHTLEG2";
	case BSDismemberBodyPartType.BP_RIGHTLEG3: return "BP_RIGHTLEG3";
	case BSDismemberBodyPartType.BP_BRAIN: return "BP_BRAIN";
	case BSDismemberBodyPartType.SBP_30_HEAD: return "SBP_30_HEAD";
	case BSDismemberBodyPartType.SBP_31_HAIR: return "SBP_31_HAIR";
	case BSDismemberBodyPartType.SBP_32_BODY: return "SBP_32_BODY";
	case BSDismemberBodyPartType.SBP_33_HANDS: return "SBP_33_HANDS";
	case BSDismemberBodyPartType.SBP_34_FOREARMS: return "SBP_34_FOREARMS";
	case BSDismemberBodyPartType.SBP_35_AMULET: return "SBP_35_AMULET";
	case BSDismemberBodyPartType.SBP_36_RING: return "SBP_36_RING";
	case BSDismemberBodyPartType.SBP_37_FEET: return "SBP_37_FEET";
	case BSDismemberBodyPartType.SBP_38_CALVES: return "SBP_38_CALVES";
	case BSDismemberBodyPartType.SBP_39_SHIELD: return "SBP_39_SHIELD";
	case BSDismemberBodyPartType.SBP_40_TAIL: return "SBP_40_TAIL";
	case BSDismemberBodyPartType.SBP_41_LONGHAIR: return "SBP_41_LONGHAIR";
	case BSDismemberBodyPartType.SBP_42_CIRCLET: return "SBP_42_CIRCLET";
	case BSDismemberBodyPartType.SBP_43_EARS: return "SBP_43_EARS";
	case BSDismemberBodyPartType.SBP_44_DRAGON_BLOODHEAD_OR_MOD_MOUTH: return "SBP_44_DRAGON_BLOODHEAD_OR_MOD_MOUTH";
	case BSDismemberBodyPartType.SBP_45_DRAGON_BLOODWINGL_OR_MOD_NECK: return "SBP_45_DRAGON_BLOODWINGL_OR_MOD_NECK";
	case BSDismemberBodyPartType.SBP_46_DRAGON_BLOODWINGR_OR_MOD_CHEST_PRIMARY: return "SBP_46_DRAGON_BLOODWINGR_OR_MOD_CHEST_PRIMARY";
	case BSDismemberBodyPartType.SBP_47_DRAGON_BLOODTAIL_OR_MOD_BACK: return "SBP_47_DRAGON_BLOODTAIL_OR_MOD_BACK";
	case BSDismemberBodyPartType.SBP_48_MOD_MISC1: return "SBP_48_MOD_MISC1";
	case BSDismemberBodyPartType.SBP_49_MOD_PELVIS_PRIMARY: return "SBP_49_MOD_PELVIS_PRIMARY";
	case BSDismemberBodyPartType.SBP_50_DECAPITATEDHEAD: return "SBP_50_DECAPITATEDHEAD";
	case BSDismemberBodyPartType.SBP_51_DECAPITATE: return "SBP_51_DECAPITATE";
	case BSDismemberBodyPartType.SBP_52_MOD_PELVIS_SECONDARY: return "SBP_52_MOD_PELVIS_SECONDARY";
	case BSDismemberBodyPartType.SBP_53_MOD_LEG_RIGHT: return "SBP_53_MOD_LEG_RIGHT";
	case BSDismemberBodyPartType.SBP_54_MOD_LEG_LEFT: return "SBP_54_MOD_LEG_LEFT";
	case BSDismemberBodyPartType.SBP_55_MOD_FACE_JEWELRY: return "SBP_55_MOD_FACE_JEWELRY";
	case BSDismemberBodyPartType.SBP_56_MOD_CHEST_SECONDARY: return "SBP_56_MOD_CHEST_SECONDARY";
	case BSDismemberBodyPartType.SBP_57_MOD_SHOULDER: return "SBP_57_MOD_SHOULDER";
	case BSDismemberBodyPartType.SBP_58_MOD_ARM_LEFT: return "SBP_58_MOD_ARM_LEFT";
	case BSDismemberBodyPartType.SBP_59_MOD_ARM_RIGHT: return "SBP_59_MOD_ARM_RIGHT";
	case BSDismemberBodyPartType.SBP_60_MOD_MISC2: return "SBP_60_MOD_MISC2";
	case BSDismemberBodyPartType.SBP_61_FX01: return "SBP_61_FX01";
	case BSDismemberBodyPartType.BP_SECTIONCAP_HEAD: return "BP_SECTIONCAP_HEAD";
	case BSDismemberBodyPartType.BP_SECTIONCAP_HEAD2: return "BP_SECTIONCAP_HEAD2";
	case BSDismemberBodyPartType.BP_SECTIONCAP_LEFTARM: return "BP_SECTIONCAP_LEFTARM";
	case BSDismemberBodyPartType.BP_SECTIONCAP_LEFTARM2: return "BP_SECTIONCAP_LEFTARM2";
	case BSDismemberBodyPartType.BP_SECTIONCAP_RIGHTARM: return "BP_SECTIONCAP_RIGHTARM";
	case BSDismemberBodyPartType.BP_SECTIONCAP_RIGHTARM2: return "BP_SECTIONCAP_RIGHTARM2";
	case BSDismemberBodyPartType.BP_SECTIONCAP_LEFTLEG: return "BP_SECTIONCAP_LEFTLEG";
	case BSDismemberBodyPartType.BP_SECTIONCAP_LEFTLEG2: return "BP_SECTIONCAP_LEFTLEG2";
	case BSDismemberBodyPartType.BP_SECTIONCAP_LEFTLEG3: return "BP_SECTIONCAP_LEFTLEG3";
	case BSDismemberBodyPartType.BP_SECTIONCAP_RIGHTLEG: return "BP_SECTIONCAP_RIGHTLEG";
	case BSDismemberBodyPartType.BP_SECTIONCAP_RIGHTLEG2: return "BP_SECTIONCAP_RIGHTLEG2";
	case BSDismemberBodyPartType.BP_SECTIONCAP_RIGHTLEG3: return "BP_SECTIONCAP_RIGHTLEG3";
	case BSDismemberBodyPartType.BP_SECTIONCAP_BRAIN: return "BP_SECTIONCAP_BRAIN";
	case BSDismemberBodyPartType.SBP_130_HEAD: return "SBP_130_HEAD";
	case BSDismemberBodyPartType.SBP_131_HAIR: return "SBP_131_HAIR";
	case BSDismemberBodyPartType.SBP_141_LONGHAIR: return "SBP_141_LONGHAIR";
	case BSDismemberBodyPartType.SBP_142_CIRCLET: return "SBP_142_CIRCLET";
	case BSDismemberBodyPartType.SBP_143_EARS: return "SBP_143_EARS";
	case BSDismemberBodyPartType.SBP_150_DECAPITATEDHEAD: return "SBP_150_DECAPITATEDHEAD";
	case BSDismemberBodyPartType.BP_TORSOCAP_HEAD: return "BP_TORSOCAP_HEAD";
	case BSDismemberBodyPartType.BP_TORSOCAP_HEAD2: return "BP_TORSOCAP_HEAD2";
	case BSDismemberBodyPartType.BP_TORSOCAP_LEFTARM: return "BP_TORSOCAP_LEFTARM";
	case BSDismemberBodyPartType.BP_TORSOCAP_LEFTARM2: return "BP_TORSOCAP_LEFTARM2";
	case BSDismemberBodyPartType.BP_TORSOCAP_RIGHTARM: return "BP_TORSOCAP_RIGHTARM";
	case BSDismemberBodyPartType.BP_TORSOCAP_RIGHTARM2: return "BP_TORSOCAP_RIGHTARM2";
	case BSDismemberBodyPartType.BP_TORSOCAP_LEFTLEG: return "BP_TORSOCAP_LEFTLEG";
	case BSDismemberBodyPartType.BP_TORSOCAP_LEFTLEG2: return "BP_TORSOCAP_LEFTLEG2";
	case BSDismemberBodyPartType.BP_TORSOCAP_LEFTLEG3: return "BP_TORSOCAP_LEFTLEG3";
	case BSDismemberBodyPartType.BP_TORSOCAP_RIGHTLEG: return "BP_TORSOCAP_RIGHTLEG";
	case BSDismemberBodyPartType.BP_TORSOCAP_RIGHTLEG2: return "BP_TORSOCAP_RIGHTLEG2";
	case BSDismemberBodyPartType.BP_TORSOCAP_RIGHTLEG3: return "BP_TORSOCAP_RIGHTLEG3";
	case BSDismemberBodyPartType.BP_TORSOCAP_BRAIN: return "BP_TORSOCAP_BRAIN";
	case BSDismemberBodyPartType.SBP_230_HEAD: return "SBP_230_HEAD";
	case BSDismemberBodyPartType.BP_TORSOSECTION_HEAD: return "BP_TORSOSECTION_HEAD";
	case BSDismemberBodyPartType.BP_TORSOSECTION_HEAD2: return "BP_TORSOSECTION_HEAD2";
	case BSDismemberBodyPartType.BP_TORSOSECTION_LEFTARM: return "BP_TORSOSECTION_LEFTARM";
	case BSDismemberBodyPartType.BP_TORSOSECTION_LEFTARM2: return "BP_TORSOSECTION_LEFTARM2";
	case BSDismemberBodyPartType.BP_TORSOSECTION_RIGHTARM: return "BP_TORSOSECTION_RIGHTARM";
	case BSDismemberBodyPartType.BP_TORSOSECTION_RIGHTARM2: return "BP_TORSOSECTION_RIGHTARM2";
	case BSDismemberBodyPartType.BP_TORSOSECTION_LEFTLEG: return "BP_TORSOSECTION_LEFTLEG";
	case BSDismemberBodyPartType.BP_TORSOSECTION_LEFTLEG2: return "BP_TORSOSECTION_LEFTLEG2";
	case BSDismemberBodyPartType.BP_TORSOSECTION_LEFTLEG3: return "BP_TORSOSECTION_LEFTLEG3";
	case BSDismemberBodyPartType.BP_TORSOSECTION_RIGHTLEG: return "BP_TORSOSECTION_RIGHTLEG";
	case BSDismemberBodyPartType.BP_TORSOSECTION_RIGHTLEG2: return "BP_TORSOSECTION_RIGHTLEG2";
	case BSDismemberBodyPartType.BP_TORSOSECTION_RIGHTLEG3: return "BP_TORSOSECTION_RIGHTLEG3";
	case BSDismemberBodyPartType.BP_TORSOSECTION_BRAIN: return "BP_TORSOSECTION_BRAIN";
	default: return $"Invalid Value! - {val}";
}}}

/*! Values for configuring the shader type in a BSLightingShaderProperty */
public enum BSLightingShaderPropertyShaderType : uint {
	ST_DEFAULT = 0, /*!< ST_Default */
	ST_ENVIRONMENT_MAP = 1, /*!< Enables EnvMap Mask(TS6), EnvMap Scale */
	ST_GLOW_SHADER = 2, /*!< Enables Glow(TS3) */
	ST_PARALLAX = 3, /*!< Enables Height(TS4) */
	ST_FACE_TINT = 4, /*!< Enables Detail(TS4), Tint(TS7) */
	ST_SKIN_TINT = 5, /*!< Enables Skin Tint Color */
	ST_HAIR_TINT = 6, /*!< Enables Hair Tint Color */
	ST_PARALLAX_OCC = 7, /*!< Enables Height(TS4), Max Passes, Scale. Unimplemented. */
	ST_MULTITEXTURE_LANDSCAPE = 8, /*!< ST_Multitexture Landscape */
	ST_LOD_LANDSCAPE = 9, /*!< ST_LOD Landscape */
	ST_SNOW = 10, /*!< ST_Snow */
	ST_MULTILAYER_PARALLAX = 11, /*!< Enables EnvMap Mask(TS6), Layer(TS7), Parallax Layer Thickness, Parallax Refraction Scale, Parallax Inner Layer U Scale, Parallax Inner Layer V Scale, EnvMap Scale */
	ST_TREE_ANIM = 12, /*!< ST_Tree Anim */
	ST_LOD_OBJECTS = 13, /*!< ST_LOD Objects */
	ST_SPARKLE_SNOW = 14, /*!< Enables SparkleParams */
	ST_LOD_OBJECTS_HD = 15, /*!< ST_LOD Objects HD */
	ST_EYE_ENVMAP = 16, /*!< Enables EnvMap Mask(TS6), Eye EnvMap Scale */
	ST_CLOUD = 17, /*!< ST_Cloud */
	ST_LOD_LANDSCAPE_NOISE = 18, /*!< ST_LOD Landscape Noise */
	ST_MULTITEXTURE_LANDSCAPE_LOD_BLEND = 19, /*!< ST_Multitexture Landscape LOD Blend */
	ST_FO4_DISMEMBERMENT = 20, /*!< ST_FO4 Dismemberment */
}
static partial class Nif { //--BSLightingShaderPropertyShaderType--//
public static void NifStream(out BSLightingShaderPropertyShaderType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (BSLightingShaderPropertyShaderType)temp; }
public static void NifStream(BSLightingShaderPropertyShaderType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(BSLightingShaderPropertyShaderType val) { switch (val) {
	case BSLightingShaderPropertyShaderType.ST_DEFAULT: return "ST_Default";
	case BSLightingShaderPropertyShaderType.ST_ENVIRONMENT_MAP: return "ST_Environment Map";
	case BSLightingShaderPropertyShaderType.ST_GLOW_SHADER: return "ST_Glow Shader";
	case BSLightingShaderPropertyShaderType.ST_PARALLAX: return "ST_Parallax";
	case BSLightingShaderPropertyShaderType.ST_FACE_TINT: return "ST_Face Tint";
	case BSLightingShaderPropertyShaderType.ST_SKIN_TINT: return "ST_Skin Tint";
	case BSLightingShaderPropertyShaderType.ST_HAIR_TINT: return "ST_Hair Tint";
	case BSLightingShaderPropertyShaderType.ST_PARALLAX_OCC: return "ST_Parallax Occ";
	case BSLightingShaderPropertyShaderType.ST_MULTITEXTURE_LANDSCAPE: return "ST_Multitexture Landscape";
	case BSLightingShaderPropertyShaderType.ST_LOD_LANDSCAPE: return "ST_LOD Landscape";
	case BSLightingShaderPropertyShaderType.ST_SNOW: return "ST_Snow";
	case BSLightingShaderPropertyShaderType.ST_MULTILAYER_PARALLAX: return "ST_MultiLayer Parallax";
	case BSLightingShaderPropertyShaderType.ST_TREE_ANIM: return "ST_Tree Anim";
	case BSLightingShaderPropertyShaderType.ST_LOD_OBJECTS: return "ST_LOD Objects";
	case BSLightingShaderPropertyShaderType.ST_SPARKLE_SNOW: return "ST_Sparkle Snow";
	case BSLightingShaderPropertyShaderType.ST_LOD_OBJECTS_HD: return "ST_LOD Objects HD";
	case BSLightingShaderPropertyShaderType.ST_EYE_ENVMAP: return "ST_Eye Envmap";
	case BSLightingShaderPropertyShaderType.ST_CLOUD: return "ST_Cloud";
	case BSLightingShaderPropertyShaderType.ST_LOD_LANDSCAPE_NOISE: return "ST_LOD Landscape Noise";
	case BSLightingShaderPropertyShaderType.ST_MULTITEXTURE_LANDSCAPE_LOD_BLEND: return "ST_Multitexture Landscape LOD Blend";
	case BSLightingShaderPropertyShaderType.ST_FO4_DISMEMBERMENT: return "ST_FO4 Dismemberment";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * An unsigned 32-bit integer, describing which float variable in
 * BSEffectShaderProperty to animate.
 */
public enum EffectShaderControlledVariable : uint {
	ESCV_EMISSIVEMULTIPLE = 0, /*!< EmissiveMultiple. */
	ESCV_FALLOFF_START_ANGLE = 1, /*!< Falloff Start Angle (degrees). */
	ESCV_FALLOFF_STOP_ANGLE = 2, /*!< Falloff Stop Angle (degrees). */
	ESCV_FALLOFF_START_OPACITY = 3, /*!< Falloff Start Opacity. */
	ESCV_FALLOFF_STOP_OPACITY = 4, /*!< Falloff Stop Opacity. */
	ESCV_ALPHA_TRANSPARENCY = 5, /*!< Alpha Transparency (Emissive alpha?). */
	ESCV_U_OFFSET = 6, /*!< U Offset. */
	ESCV_U_SCALE = 7, /*!< U Scale. */
	ESCV_V_OFFSET = 8, /*!< V Offset. */
	ESCV_V_SCALE = 9, /*!< V Scale. */
}
static partial class Nif { //--EffectShaderControlledVariable--//
public static void NifStream(out EffectShaderControlledVariable val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (EffectShaderControlledVariable)temp; }
public static void NifStream(EffectShaderControlledVariable val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(EffectShaderControlledVariable val) { switch (val) {
	case EffectShaderControlledVariable.ESCV_EMISSIVEMULTIPLE: return "ESCV_EmissiveMultiple";
	case EffectShaderControlledVariable.ESCV_FALLOFF_START_ANGLE: return "ESCV_Falloff Start Angle";
	case EffectShaderControlledVariable.ESCV_FALLOFF_STOP_ANGLE: return "ESCV_Falloff Stop Angle";
	case EffectShaderControlledVariable.ESCV_FALLOFF_START_OPACITY: return "ESCV_Falloff Start Opacity";
	case EffectShaderControlledVariable.ESCV_FALLOFF_STOP_OPACITY: return "ESCV_Falloff Stop Opacity";
	case EffectShaderControlledVariable.ESCV_ALPHA_TRANSPARENCY: return "ESCV_Alpha Transparency";
	case EffectShaderControlledVariable.ESCV_U_OFFSET: return "ESCV_U Offset";
	case EffectShaderControlledVariable.ESCV_U_SCALE: return "ESCV_U Scale";
	case EffectShaderControlledVariable.ESCV_V_OFFSET: return "ESCV_V Offset";
	case EffectShaderControlledVariable.ESCV_V_SCALE: return "ESCV_V Scale";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * An unsigned 32-bit integer, describing which color in BSEffectShaderProperty to
 * animate.
 */
public enum EffectShaderControlledColor : uint {
	ECSC_EMISSIVE_COLOR = 0, /*!< Emissive Color. */
}
static partial class Nif { //--EffectShaderControlledColor--//
public static void NifStream(out EffectShaderControlledColor val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (EffectShaderControlledColor)temp; }
public static void NifStream(EffectShaderControlledColor val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(EffectShaderControlledColor val) { switch (val) {
	case EffectShaderControlledColor.ECSC_EMISSIVE_COLOR: return "ECSC_Emissive Color";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * An unsigned 32-bit integer, describing which float variable in
 * BSLightingShaderProperty to animate.
 */
public enum LightingShaderControlledVariable : uint {
	LSCV_REFRACTION_STRENGTH = 0, /*!< The amount of distortion. */
	LSCV_ENVIRONMENT_MAP_SCALE = 8, /*!< Environment Map Scale. */
	LSCV_GLOSSINESS = 9, /*!< Glossiness. */
	LSCV_SPECULAR_STRENGTH = 10, /*!< Specular Strength. */
	LSCV_EMISSIVE_MULTIPLE = 11, /*!< Emissive Multiple. */
	LSCV_ALPHA = 12, /*!< Alpha. */
	LSCV_U_OFFSET = 20, /*!< U Offset. */
	LSCV_U_SCALE = 21, /*!< U Scale. */
	LSCV_V_OFFSET = 22, /*!< V Offset. */
	LSCV_V_SCALE = 23, /*!< V Scale. */
}
static partial class Nif { //--LightingShaderControlledVariable--//
public static void NifStream(out LightingShaderControlledVariable val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (LightingShaderControlledVariable)temp; }
public static void NifStream(LightingShaderControlledVariable val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(LightingShaderControlledVariable val) { switch (val) {
	case LightingShaderControlledVariable.LSCV_REFRACTION_STRENGTH: return "LSCV_Refraction Strength";
	case LightingShaderControlledVariable.LSCV_ENVIRONMENT_MAP_SCALE: return "LSCV_Environment Map Scale";
	case LightingShaderControlledVariable.LSCV_GLOSSINESS: return "LSCV_Glossiness";
	case LightingShaderControlledVariable.LSCV_SPECULAR_STRENGTH: return "LSCV_Specular Strength";
	case LightingShaderControlledVariable.LSCV_EMISSIVE_MULTIPLE: return "LSCV_Emissive Multiple";
	case LightingShaderControlledVariable.LSCV_ALPHA: return "LSCV_Alpha";
	case LightingShaderControlledVariable.LSCV_U_OFFSET: return "LSCV_U Offset";
	case LightingShaderControlledVariable.LSCV_U_SCALE: return "LSCV_U Scale";
	case LightingShaderControlledVariable.LSCV_V_OFFSET: return "LSCV_V Offset";
	case LightingShaderControlledVariable.LSCV_V_SCALE: return "LSCV_V Scale";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * An unsigned 32-bit integer, describing which color in BSLightingShaderProperty
 * to animate.
 */
public enum LightingShaderControlledColor : uint {
	LSCC_SPECULAR_COLOR = 0, /*!< Specular Color. */
	LSCC_EMISSIVE_COLOR = 1, /*!< Emissive Color. */
}
static partial class Nif { //--LightingShaderControlledColor--//
public static void NifStream(out LightingShaderControlledColor val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (LightingShaderControlledColor)temp; }
public static void NifStream(LightingShaderControlledColor val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(LightingShaderControlledColor val) { switch (val) {
	case LightingShaderControlledColor.LSCC_SPECULAR_COLOR: return "LSCC_Specular Color";
	case LightingShaderControlledColor.LSCC_EMISSIVE_COLOR: return "LSCC_Emissive Color";
	default: return $"Invalid Value! - {val}";
}}}

/*! Bethesda Havok. Describes the type of bhkConstraint. */
public enum hkConstraintType : uint {
	BALLANDSOCKET = 0, /*!< A ball and socket constraint. */
	HINGE = 1, /*!< A hinge constraint. */
	LIMITED_HINGE = 2, /*!< A limited hinge constraint. */
	PRISMATIC = 6, /*!< A prismatic constraint. */
	RAGDOLL = 7, /*!< A ragdoll constraint. */
	STIFFSPRING = 8, /*!< A stiff spring constraint. */
	MALLEABLE = 13, /*!< A malleable constraint. */
}
static partial class Nif { //--hkConstraintType--//
public static void NifStream(out hkConstraintType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (hkConstraintType)temp; }
public static void NifStream(hkConstraintType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(hkConstraintType val) { switch (val) {
	case hkConstraintType.BALLANDSOCKET: return "BallAndSocket";
	case hkConstraintType.HINGE: return "Hinge";
	case hkConstraintType.LIMITED_HINGE: return "Limited Hinge";
	case hkConstraintType.PRISMATIC: return "Prismatic";
	case hkConstraintType.RAGDOLL: return "Ragdoll";
	case hkConstraintType.STIFFSPRING: return "StiffSpring";
	case hkConstraintType.MALLEABLE: return "Malleable";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes the order of scaling and rotation matrices. Translate, Scale,
 * Rotation, Center are from TexDesc.
 *         Back = inverse of Center. FromMaya = inverse of the V axis with a
 * positive translation along V of 1 unit.
 */
public enum TransformMethod : uint {
	TM_MAYA_DEPRECATED = 0, /*!< Center * Rotation * Back * Translate * Scale */
	TM_MAX = 1, /*!< Center * Scale * Rotation * Translate * Back */
	TM_MAYA = 2, /*!< Center * Rotation * Back * FromMaya * Translate * Scale */
}
static partial class Nif { //--TransformMethod--//
public static void NifStream(out TransformMethod val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (TransformMethod)temp; }
public static void NifStream(TransformMethod val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(TransformMethod val) { switch (val) {
	case TransformMethod.TM_MAYA_DEPRECATED: return "TM_Maya Deprecated";
	case TransformMethod.TM_MAX: return "TM_Max";
	case TransformMethod.TM_MAYA: return "TM_Maya";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Animation. Animation type used on this position. This specifies the
 * function of this position.
 */
public enum AnimationType : uint {
	SIT = 1, /*!< Actor use sit animation. */
	SLEEP = 2, /*!< Actor use sleep animation. */
	LEAN = 4, /*!< Used for lean animations? */
}
static partial class Nif { //--AnimationType--//
public static void NifStream(out AnimationType val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (AnimationType)temp; }
public static void NifStream(AnimationType val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(AnimationType val) { switch (val) {
	case AnimationType.SIT: return "Sit";
	case AnimationType.SLEEP: return "Sleep";
	case AnimationType.LEAN: return "Lean";
	default: return $"Invalid Value! - {val}";
}}}

public enum MotorType : uint {
	MOTOR_NONE = 0, /*!< MOTOR_NONE */
	MOTOR_POSITION = 1, /*!< MOTOR_POSITION */
	MOTOR_VELOCITY = 2, /*!< MOTOR_VELOCITY */
	MOTOR_SPRING = 3, /*!< MOTOR_SPRING */
}
static partial class Nif { //--MotorType--//
public static void NifStream(out MotorType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (MotorType)temp; }
public static void NifStream(MotorType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(MotorType val) { switch (val) {
	case MotorType.MOTOR_NONE: return "MOTOR_NONE";
	case MotorType.MOTOR_POSITION: return "MOTOR_POSITION";
	case MotorType.MOTOR_VELOCITY: return "MOTOR_VELOCITY";
	case MotorType.MOTOR_SPRING: return "MOTOR_SPRING";
	default: return $"Invalid Value! - {val}";
}}}

/*! Determines how the raw image data is stored in NiRawImageData. */
public enum ImageType : uint {
	RGB = 1, /*!< Colors store red, blue, and green components. */
	RGBA = 2, /*!< Colors store red, blue, green, and alpha components. */
}
static partial class Nif { //--ImageType--//
public static void NifStream(out ImageType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (ImageType)temp; }
public static void NifStream(ImageType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(ImageType val) { switch (val) {
	case ImageType.RGB: return "RGB";
	case ImageType.RGBA: return "RGBA";
	default: return $"Invalid Value! - {val}";
}}}

public enum BroadPhaseType : uint {
	BROAD_PHASE_INVALID = 0, /*!< BROAD_PHASE_INVALID */
	BROAD_PHASE_ENTITY = 1, /*!< BROAD_PHASE_ENTITY */
	BROAD_PHASE_PHANTOM = 2, /*!< BROAD_PHASE_PHANTOM */
	BROAD_PHASE_BORDER = 3, /*!< BROAD_PHASE_BORDER */
}
static partial class Nif { //--BroadPhaseType--//
public static void NifStream(out BroadPhaseType val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (BroadPhaseType)temp; }
public static void NifStream(BroadPhaseType val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(BroadPhaseType val) { switch (val) {
	case BroadPhaseType.BROAD_PHASE_INVALID: return "BROAD_PHASE_INVALID";
	case BroadPhaseType.BROAD_PHASE_ENTITY: return "BROAD_PHASE_ENTITY";
	case BroadPhaseType.BROAD_PHASE_PHANTOM: return "BROAD_PHASE_PHANTOM";
	case BroadPhaseType.BROAD_PHASE_BORDER: return "BROAD_PHASE_BORDER";
	default: return $"Invalid Value! - {val}";
}}}

public enum InterpBlendFlags : uint {
	MANAGER_CONTROLLED = 1, /*!< MANAGER_CONTROLLED */
}
static partial class Nif { //--InterpBlendFlags--//
public static void NifStream(out InterpBlendFlags val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (InterpBlendFlags)temp; }
public static void NifStream(InterpBlendFlags val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(InterpBlendFlags val) { switch (val) {
	case InterpBlendFlags.MANAGER_CONTROLLED: return "MANAGER_CONTROLLED";
	default: return $"Invalid Value! - {val}";
}}}

public enum NxJointType : uint {
	NX_JOINT_PRISMATIC = 0, /*!< NX_JOINT_PRISMATIC */
	NX_JOINT_REVOLUTE = 1, /*!< NX_JOINT_REVOLUTE */
	NX_JOINT_CYLINDRICAL = 2, /*!< NX_JOINT_CYLINDRICAL */
	NX_JOINT_SPHERICAL = 3, /*!< NX_JOINT_SPHERICAL */
	NX_JOINT_POINT_ON_LINE = 4, /*!< NX_JOINT_POINT_ON_LINE */
	NX_JOINT_POINT_IN_PLANE = 5, /*!< NX_JOINT_POINT_IN_PLANE */
	NX_JOINT_DISTANCE = 6, /*!< NX_JOINT_DISTANCE */
	NX_JOINT_PULLEY = 7, /*!< NX_JOINT_PULLEY */
	NX_JOINT_FIXED = 8, /*!< NX_JOINT_FIXED */
	NX_JOINT_D6 = 9, /*!< NX_JOINT_D6 */
}
static partial class Nif { //--NxJointType--//
public static void NifStream(out NxJointType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (NxJointType)temp; }
public static void NifStream(NxJointType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(NxJointType val) { switch (val) {
	case NxJointType.NX_JOINT_PRISMATIC: return "NX_JOINT_PRISMATIC";
	case NxJointType.NX_JOINT_REVOLUTE: return "NX_JOINT_REVOLUTE";
	case NxJointType.NX_JOINT_CYLINDRICAL: return "NX_JOINT_CYLINDRICAL";
	case NxJointType.NX_JOINT_SPHERICAL: return "NX_JOINT_SPHERICAL";
	case NxJointType.NX_JOINT_POINT_ON_LINE: return "NX_JOINT_POINT_ON_LINE";
	case NxJointType.NX_JOINT_POINT_IN_PLANE: return "NX_JOINT_POINT_IN_PLANE";
	case NxJointType.NX_JOINT_DISTANCE: return "NX_JOINT_DISTANCE";
	case NxJointType.NX_JOINT_PULLEY: return "NX_JOINT_PULLEY";
	case NxJointType.NX_JOINT_FIXED: return "NX_JOINT_FIXED";
	case NxJointType.NX_JOINT_D6: return "NX_JOINT_D6";
	default: return $"Invalid Value! - {val}";
}}}

public enum NxD6JointMotion : uint {
	NX_D6JOINT_MOTION_LOCKED = 0, /*!< NX_D6JOINT_MOTION_LOCKED */
	NX_D6JOINT_MOTION_LIMITED = 1, /*!< NX_D6JOINT_MOTION_LIMITED */
	NX_D6JOINT_MOTION_FREE = 2, /*!< NX_D6JOINT_MOTION_FREE */
}
static partial class Nif { //--NxD6JointMotion--//
public static void NifStream(out NxD6JointMotion val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (NxD6JointMotion)temp; }
public static void NifStream(NxD6JointMotion val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(NxD6JointMotion val) { switch (val) {
	case NxD6JointMotion.NX_D6JOINT_MOTION_LOCKED: return "NX_D6JOINT_MOTION_LOCKED";
	case NxD6JointMotion.NX_D6JOINT_MOTION_LIMITED: return "NX_D6JOINT_MOTION_LIMITED";
	case NxD6JointMotion.NX_D6JOINT_MOTION_FREE: return "NX_D6JOINT_MOTION_FREE";
	default: return $"Invalid Value! - {val}";
}}}

public enum NxD6JointDriveType : uint {
	NX_D6JOINT_DRIVE_POSITION = 1, /*!< NX_D6JOINT_DRIVE_POSITION */
	NX_D6JOINT_DRIVE_VELOCITY = 2, /*!< NX_D6JOINT_DRIVE_VELOCITY */
}
static partial class Nif { //--NxD6JointDriveType--//
public static void NifStream(out NxD6JointDriveType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (NxD6JointDriveType)temp; }
public static void NifStream(NxD6JointDriveType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(NxD6JointDriveType val) { switch (val) {
	case NxD6JointDriveType.NX_D6JOINT_DRIVE_POSITION: return "NX_D6JOINT_DRIVE_POSITION";
	case NxD6JointDriveType.NX_D6JOINT_DRIVE_VELOCITY: return "NX_D6JOINT_DRIVE_VELOCITY";
	default: return $"Invalid Value! - {val}";
}}}

public enum NxJointProjectionMode : uint {
	NX_JPM_NONE = 0, /*!< NX_JPM_NONE */
	NX_JPM_POINT_MINDIST = 1, /*!< NX_JPM_POINT_MINDIST */
	NX_JPM_LINEAR_MINDIST = 2, /*!< NX_JPM_LINEAR_MINDIST */
}
static partial class Nif { //--NxJointProjectionMode--//
public static void NifStream(out NxJointProjectionMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (NxJointProjectionMode)temp; }
public static void NifStream(NxJointProjectionMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(NxJointProjectionMode val) { switch (val) {
	case NxJointProjectionMode.NX_JPM_NONE: return "NX_JPM_NONE";
	case NxJointProjectionMode.NX_JPM_POINT_MINDIST: return "NX_JPM_POINT_MINDIST";
	case NxJointProjectionMode.NX_JPM_LINEAR_MINDIST: return "NX_JPM_LINEAR_MINDIST";
	default: return $"Invalid Value! - {val}";
}}}

public enum NxShapeType : uint {
	NX_SHAPE_PLANE = 0, /*!< NX_SHAPE_PLANE */
	NX_SHAPE_SPHERE = 1, /*!< NX_SHAPE_SPHERE */
	NX_SHAPE_BOX = 2, /*!< NX_SHAPE_BOX */
	NX_SHAPE_CAPSULE = 3, /*!< NX_SHAPE_CAPSULE */
	NX_SHAPE_WHEEL = 4, /*!< NX_SHAPE_WHEEL */
	NX_SHAPE_CONVEX = 5, /*!< NX_SHAPE_CONVEX */
	NX_SHAPE_MESH = 6, /*!< NX_SHAPE_MESH */
	NX_SHAPE_HEIGHTFIELD = 7, /*!< NX_SHAPE_HEIGHTFIELD */
	NX_SHAPE_RAW_MESH = 8, /*!< NX_SHAPE_RAW_MESH */
	NX_SHAPE_COMPOUND = 9, /*!< NX_SHAPE_COMPOUND */
}
static partial class Nif { //--NxShapeType--//
public static void NifStream(out NxShapeType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (NxShapeType)temp; }
public static void NifStream(NxShapeType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(NxShapeType val) { switch (val) {
	case NxShapeType.NX_SHAPE_PLANE: return "NX_SHAPE_PLANE";
	case NxShapeType.NX_SHAPE_SPHERE: return "NX_SHAPE_SPHERE";
	case NxShapeType.NX_SHAPE_BOX: return "NX_SHAPE_BOX";
	case NxShapeType.NX_SHAPE_CAPSULE: return "NX_SHAPE_CAPSULE";
	case NxShapeType.NX_SHAPE_WHEEL: return "NX_SHAPE_WHEEL";
	case NxShapeType.NX_SHAPE_CONVEX: return "NX_SHAPE_CONVEX";
	case NxShapeType.NX_SHAPE_MESH: return "NX_SHAPE_MESH";
	case NxShapeType.NX_SHAPE_HEIGHTFIELD: return "NX_SHAPE_HEIGHTFIELD";
	case NxShapeType.NX_SHAPE_RAW_MESH: return "NX_SHAPE_RAW_MESH";
	case NxShapeType.NX_SHAPE_COMPOUND: return "NX_SHAPE_COMPOUND";
	default: return $"Invalid Value! - {val}";
}}}

public enum NxCombineMode : uint {
	NX_CM_AVERAGE = 0, /*!< NX_CM_AVERAGE */
	NX_CM_MIN = 1, /*!< NX_CM_MIN */
	NX_CM_MULTIPLY = 2, /*!< NX_CM_MULTIPLY */
	NX_CM_MAX = 3, /*!< NX_CM_MAX */
}
static partial class Nif { //--NxCombineMode--//
public static void NifStream(out NxCombineMode val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (NxCombineMode)temp; }
public static void NifStream(NxCombineMode val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(NxCombineMode val) { switch (val) {
	case NxCombineMode.NX_CM_AVERAGE: return "NX_CM_AVERAGE";
	case NxCombineMode.NX_CM_MIN: return "NX_CM_MIN";
	case NxCombineMode.NX_CM_MULTIPLY: return "NX_CM_MULTIPLY";
	case NxCombineMode.NX_CM_MAX: return "NX_CM_MAX";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * The type of animation interpolation (blending) that will be used on the
 * associated key frames.
 */
public enum BSShaderType : uint {
	SHADER_TALL_GRASS = 0, /*!< Tall Grass Shader */
	SHADER_DEFAULT = 1, /*!< Standard Lighting Shader */
	SHADER_SKY = 10, /*!< Sky Shader */
	SHADER_SKIN = 14, /*!< Skin Shader */
	SHADER_WATER = 17, /*!< Water Shader */
	SHADER_LIGHTING30 = 29, /*!< Lighting 3.0 Shader */
	SHADER_TILE = 32, /*!< Tiled Shader */
	SHADER_NOLIGHTING = 33, /*!< No Lighting Shader */
}
static partial class Nif { //--BSShaderType--//
public static void NifStream(out BSShaderType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (BSShaderType)temp; }
public static void NifStream(BSShaderType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(BSShaderType val) { switch (val) {
	case BSShaderType.SHADER_TALL_GRASS: return "SHADER_TALL_GRASS";
	case BSShaderType.SHADER_DEFAULT: return "SHADER_DEFAULT";
	case BSShaderType.SHADER_SKY: return "SHADER_SKY";
	case BSShaderType.SHADER_SKIN: return "SHADER_SKIN";
	case BSShaderType.SHADER_WATER: return "SHADER_WATER";
	case BSShaderType.SHADER_LIGHTING30: return "SHADER_LIGHTING30";
	case BSShaderType.SHADER_TILE: return "SHADER_TILE";
	case BSShaderType.SHADER_NOLIGHTING: return "SHADER_NOLIGHTING";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Sets what sky function this object fulfills in BSSkyShaderProperty or
 * SkyShaderProperty.
 */
public enum SkyObjectType : uint {
	BSSM_SKY_TEXTURE = 0, /*!< BSSM_Sky_Texture */
	BSSM_SKY_SUNGLARE = 1, /*!< BSSM_Sky_Sunglare */
	BSSM_SKY = 2, /*!< BSSM_Sky */
	BSSM_SKY_CLOUDS = 3, /*!< BSSM_Sky_Clouds */
	BSSM_SKY_STARS = 5, /*!< BSSM_Sky_Stars */
	BSSM_SKY_MOON_STARS_MASK = 7, /*!< BSSM_Sky_Moon_Stars_Mask */
}
static partial class Nif { //--SkyObjectType--//
public static void NifStream(out SkyObjectType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (SkyObjectType)temp; }
public static void NifStream(SkyObjectType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(SkyObjectType val) { switch (val) {
	case SkyObjectType.BSSM_SKY_TEXTURE: return "BSSM_SKY_TEXTURE";
	case SkyObjectType.BSSM_SKY_SUNGLARE: return "BSSM_SKY_SUNGLARE";
	case SkyObjectType.BSSM_SKY: return "BSSM_SKY";
	case SkyObjectType.BSSM_SKY_CLOUDS: return "BSSM_SKY_CLOUDS";
	case SkyObjectType.BSSM_SKY_STARS: return "BSSM_SKY_STARS";
	case SkyObjectType.BSSM_SKY_MOON_STARS_MASK: return "BSSM_SKY_MOON_STARS_MASK";
	default: return $"Invalid Value! - {val}";
}}}

/*! Anim note types. */
public enum AnimNoteType : uint {
	ANT_INVALID = 0, /*!< ANT_INVALID */
	ANT_GRABIK = 1, /*!< ANT_GRABIK */
	ANT_LOOKIK = 2, /*!< ANT_LOOKIK */
}
static partial class Nif { //--AnimNoteType--//
public static void NifStream(out AnimNoteType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (AnimNoteType)temp; }
public static void NifStream(AnimNoteType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(AnimNoteType val) { switch (val) {
	case AnimNoteType.ANT_INVALID: return "ANT_INVALID";
	case AnimNoteType.ANT_GRABIK: return "ANT_GRABIK";
	case AnimNoteType.ANT_LOOKIK: return "ANT_LOOKIK";
	default: return $"Invalid Value! - {val}";
}}}

/*! Culling modes for multi bound nodes. */
public enum BSCPCullingType : uint {
	BSCP_CULL_NORMAL = 0, /*!< Normal */
	BSCP_CULL_ALLPASS = 1, /*!< All Pass */
	BSCP_CULL_ALLFAIL = 2, /*!< All Fail */
	BSCP_CULL_IGNOREMULTIBOUNDS = 3, /*!< Ignore Multi Bounds */
	BSCP_CULL_FORCEMULTIBOUNDSNOUPDATE = 4, /*!< Force Multi Bounds No Update */
}
static partial class Nif { //--BSCPCullingType--//
public static void NifStream(out BSCPCullingType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (BSCPCullingType)temp; }
public static void NifStream(BSCPCullingType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(BSCPCullingType val) { switch (val) {
	case BSCPCullingType.BSCP_CULL_NORMAL: return "BSCP_CULL_NORMAL";
	case BSCPCullingType.BSCP_CULL_ALLPASS: return "BSCP_CULL_ALLPASS";
	case BSCPCullingType.BSCP_CULL_ALLFAIL: return "BSCP_CULL_ALLFAIL";
	case BSCPCullingType.BSCP_CULL_IGNOREMULTIBOUNDS: return "BSCP_CULL_IGNOREMULTIBOUNDS";
	case BSCPCullingType.BSCP_CULL_FORCEMULTIBOUNDSNOUPDATE: return "BSCP_CULL_FORCEMULTIBOUNDSNOUPDATE";
	default: return $"Invalid Value! - {val}";
}}}

/*! Sets how objects are to be cloned. */
public enum CloningBehavior : uint {
	CLONING_SHARE = 0, /*!< Share this object pointer with the newly cloned scene. */
	CLONING_COPY = 1, /*!< Create an exact duplicate of this object for use with the newly cloned scene. */
	CLONING_BLANK_COPY = 2, /*!< Create a copy of this object for use with the newly cloned stream, leaving some of the data to be written later. */
}
static partial class Nif { //--CloningBehavior--//
public static void NifStream(out CloningBehavior val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (CloningBehavior)temp; }
public static void NifStream(CloningBehavior val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(CloningBehavior val) { switch (val) {
	case CloningBehavior.CLONING_SHARE: return "CLONING_SHARE";
	case CloningBehavior.CLONING_COPY: return "CLONING_COPY";
	case CloningBehavior.CLONING_BLANK_COPY: return "CLONING_BLANK_COPY";
	default: return $"Invalid Value! - {val}";
}}}

/*! The data format of components. */
public enum ComponentFormat : uint {
	F_UNKNOWN = 0x00000000, /*!< Unknown, or don't care, format. */
	F_INT8_1 = 0x00010101, /*!< F_INT8_1 */
	F_INT8_2 = 0x00020102, /*!< F_INT8_2 */
	F_INT8_3 = 0x00030103, /*!< F_INT8_3 */
	F_INT8_4 = 0x00040104, /*!< F_INT8_4 */
	F_UINT8_1 = 0x00010105, /*!< F_UINT8_1 */
	F_UINT8_2 = 0x00020106, /*!< F_UINT8_2 */
	F_UINT8_3 = 0x00030107, /*!< F_UINT8_3 */
	F_UINT8_4 = 0x00040108, /*!< F_UINT8_4 */
	F_NORMINT8_1 = 0x00010109, /*!< F_NORMINT8_1 */
	F_NORMINT8_2 = 0x0002010A, /*!< F_NORMINT8_2 */
	F_NORMINT8_3 = 0x0003010B, /*!< F_NORMINT8_3 */
	F_NORMINT8_4 = 0x0004010C, /*!< F_NORMINT8_4 */
	F_NORMUINT8_1 = 0x0001010D, /*!< F_NORMUINT8_1 */
	F_NORMUINT8_2 = 0x0002010E, /*!< F_NORMUINT8_2 */
	F_NORMUINT8_3 = 0x0003010F, /*!< F_NORMUINT8_3 */
	F_NORMUINT8_4 = 0x00040110, /*!< F_NORMUINT8_4 */
	F_INT16_1 = 0x00010211, /*!< F_INT16_1 */
	F_INT16_2 = 0x00020212, /*!< F_INT16_2 */
	F_INT16_3 = 0x00030213, /*!< F_INT16_3 */
	F_INT16_4 = 0x00040214, /*!< F_INT16_4 */
	F_UINT16_1 = 0x00010215, /*!< F_UINT16_1 */
	F_UINT16_2 = 0x00020216, /*!< F_UINT16_2 */
	F_UINT16_3 = 0x00030217, /*!< F_UINT16_3 */
	F_UINT16_4 = 0x00040218, /*!< F_UINT16_4 */
	F_NORMINT16_1 = 0x00010219, /*!< F_NORMINT16_1 */
	F_NORMINT16_2 = 0x0002021A, /*!< F_NORMINT16_2 */
	F_NORMINT16_3 = 0x0003021B, /*!< F_NORMINT16_3 */
	F_NORMINT16_4 = 0x0004021C, /*!< F_NORMINT16_4 */
	F_NORMUINT16_1 = 0x0001021D, /*!< F_NORMUINT16_1 */
	F_NORMUINT16_2 = 0x0002021E, /*!< F_NORMUINT16_2 */
	F_NORMUINT16_3 = 0x0003021F, /*!< F_NORMUINT16_3 */
	F_NORMUINT16_4 = 0x00040220, /*!< F_NORMUINT16_4 */
	F_INT32_1 = 0x00010421, /*!< F_INT32_1 */
	F_INT32_2 = 0x00020422, /*!< F_INT32_2 */
	F_INT32_3 = 0x00030423, /*!< F_INT32_3 */
	F_INT32_4 = 0x00040424, /*!< F_INT32_4 */
	F_UINT32_1 = 0x00010425, /*!< F_UINT32_1 */
	F_UINT32_2 = 0x00020426, /*!< F_UINT32_2 */
	F_UINT32_3 = 0x00030427, /*!< F_UINT32_3 */
	F_UINT32_4 = 0x00040428, /*!< F_UINT32_4 */
	F_NORMINT32_1 = 0x00010429, /*!< F_NORMINT32_1 */
	F_NORMINT32_2 = 0x0002042A, /*!< F_NORMINT32_2 */
	F_NORMINT32_3 = 0x0003042B, /*!< F_NORMINT32_3 */
	F_NORMINT32_4 = 0x0004042C, /*!< F_NORMINT32_4 */
	F_NORMUINT32_1 = 0x0001042D, /*!< F_NORMUINT32_1 */
	F_NORMUINT32_2 = 0x0002042E, /*!< F_NORMUINT32_2 */
	F_NORMUINT32_3 = 0x0003042F, /*!< F_NORMUINT32_3 */
	F_NORMUINT32_4 = 0x00040430, /*!< F_NORMUINT32_4 */
	F_FLOAT16_1 = 0x00010231, /*!< F_FLOAT16_1 */
	F_FLOAT16_2 = 0x00020232, /*!< F_FLOAT16_2 */
	F_FLOAT16_3 = 0x00030233, /*!< F_FLOAT16_3 */
	F_FLOAT16_4 = 0x00040234, /*!< F_FLOAT16_4 */
	F_FLOAT32_1 = 0x00010435, /*!< F_FLOAT32_1 */
	F_FLOAT32_2 = 0x00020436, /*!< F_FLOAT32_2 */
	F_FLOAT32_3 = 0x00030437, /*!< F_FLOAT32_3 */
	F_FLOAT32_4 = 0x00040438, /*!< F_FLOAT32_4 */
	F_UINT_10_10_10_L1 = 0x00010439, /*!< F_UINT_10_10_10_L1 */
	F_NORMINT_10_10_10_L1 = 0x0001043A, /*!< F_NORMINT_10_10_10_L1 */
	F_NORMINT_11_11_10 = 0x0001043B, /*!< F_NORMINT_11_11_10 */
	F_NORMUINT8_4_BGRA = 0x0004013C, /*!< F_NORMUINT8_4_BGRA */
	F_NORMINT_10_10_10_2 = 0x0001043D, /*!< F_NORMINT_10_10_10_2 */
	F_UINT_10_10_10_2 = 0x0001043E, /*!< F_UINT_10_10_10_2 */
}
static partial class Nif { //--ComponentFormat--//
public static void NifStream(out ComponentFormat val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (ComponentFormat)temp; }
public static void NifStream(ComponentFormat val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(ComponentFormat val) { switch (val) {
	case ComponentFormat.F_UNKNOWN: return "F_UNKNOWN";
	case ComponentFormat.F_INT8_1: return "F_INT8_1";
	case ComponentFormat.F_INT8_2: return "F_INT8_2";
	case ComponentFormat.F_INT8_3: return "F_INT8_3";
	case ComponentFormat.F_INT8_4: return "F_INT8_4";
	case ComponentFormat.F_UINT8_1: return "F_UINT8_1";
	case ComponentFormat.F_UINT8_2: return "F_UINT8_2";
	case ComponentFormat.F_UINT8_3: return "F_UINT8_3";
	case ComponentFormat.F_UINT8_4: return "F_UINT8_4";
	case ComponentFormat.F_NORMINT8_1: return "F_NORMINT8_1";
	case ComponentFormat.F_NORMINT8_2: return "F_NORMINT8_2";
	case ComponentFormat.F_NORMINT8_3: return "F_NORMINT8_3";
	case ComponentFormat.F_NORMINT8_4: return "F_NORMINT8_4";
	case ComponentFormat.F_NORMUINT8_1: return "F_NORMUINT8_1";
	case ComponentFormat.F_NORMUINT8_2: return "F_NORMUINT8_2";
	case ComponentFormat.F_NORMUINT8_3: return "F_NORMUINT8_3";
	case ComponentFormat.F_NORMUINT8_4: return "F_NORMUINT8_4";
	case ComponentFormat.F_INT16_1: return "F_INT16_1";
	case ComponentFormat.F_INT16_2: return "F_INT16_2";
	case ComponentFormat.F_INT16_3: return "F_INT16_3";
	case ComponentFormat.F_INT16_4: return "F_INT16_4";
	case ComponentFormat.F_UINT16_1: return "F_UINT16_1";
	case ComponentFormat.F_UINT16_2: return "F_UINT16_2";
	case ComponentFormat.F_UINT16_3: return "F_UINT16_3";
	case ComponentFormat.F_UINT16_4: return "F_UINT16_4";
	case ComponentFormat.F_NORMINT16_1: return "F_NORMINT16_1";
	case ComponentFormat.F_NORMINT16_2: return "F_NORMINT16_2";
	case ComponentFormat.F_NORMINT16_3: return "F_NORMINT16_3";
	case ComponentFormat.F_NORMINT16_4: return "F_NORMINT16_4";
	case ComponentFormat.F_NORMUINT16_1: return "F_NORMUINT16_1";
	case ComponentFormat.F_NORMUINT16_2: return "F_NORMUINT16_2";
	case ComponentFormat.F_NORMUINT16_3: return "F_NORMUINT16_3";
	case ComponentFormat.F_NORMUINT16_4: return "F_NORMUINT16_4";
	case ComponentFormat.F_INT32_1: return "F_INT32_1";
	case ComponentFormat.F_INT32_2: return "F_INT32_2";
	case ComponentFormat.F_INT32_3: return "F_INT32_3";
	case ComponentFormat.F_INT32_4: return "F_INT32_4";
	case ComponentFormat.F_UINT32_1: return "F_UINT32_1";
	case ComponentFormat.F_UINT32_2: return "F_UINT32_2";
	case ComponentFormat.F_UINT32_3: return "F_UINT32_3";
	case ComponentFormat.F_UINT32_4: return "F_UINT32_4";
	case ComponentFormat.F_NORMINT32_1: return "F_NORMINT32_1";
	case ComponentFormat.F_NORMINT32_2: return "F_NORMINT32_2";
	case ComponentFormat.F_NORMINT32_3: return "F_NORMINT32_3";
	case ComponentFormat.F_NORMINT32_4: return "F_NORMINT32_4";
	case ComponentFormat.F_NORMUINT32_1: return "F_NORMUINT32_1";
	case ComponentFormat.F_NORMUINT32_2: return "F_NORMUINT32_2";
	case ComponentFormat.F_NORMUINT32_3: return "F_NORMUINT32_3";
	case ComponentFormat.F_NORMUINT32_4: return "F_NORMUINT32_4";
	case ComponentFormat.F_FLOAT16_1: return "F_FLOAT16_1";
	case ComponentFormat.F_FLOAT16_2: return "F_FLOAT16_2";
	case ComponentFormat.F_FLOAT16_3: return "F_FLOAT16_3";
	case ComponentFormat.F_FLOAT16_4: return "F_FLOAT16_4";
	case ComponentFormat.F_FLOAT32_1: return "F_FLOAT32_1";
	case ComponentFormat.F_FLOAT32_2: return "F_FLOAT32_2";
	case ComponentFormat.F_FLOAT32_3: return "F_FLOAT32_3";
	case ComponentFormat.F_FLOAT32_4: return "F_FLOAT32_4";
	case ComponentFormat.F_UINT_10_10_10_L1: return "F_UINT_10_10_10_L1";
	case ComponentFormat.F_NORMINT_10_10_10_L1: return "F_NORMINT_10_10_10_L1";
	case ComponentFormat.F_NORMINT_11_11_10: return "F_NORMINT_11_11_10";
	case ComponentFormat.F_NORMUINT8_4_BGRA: return "F_NORMUINT8_4_BGRA";
	case ComponentFormat.F_NORMINT_10_10_10_2: return "F_NORMINT_10_10_10_2";
	case ComponentFormat.F_UINT_10_10_10_2: return "F_UINT_10_10_10_2";
	default: return $"Invalid Value! - {val}";
}}}

/*! Determines how a data stream is used? */
public enum DataStreamUsage : uint {
	USAGE_VERTEX_INDEX = 0, /*!< USAGE_VERTEX_INDEX */
	USAGE_VERTEX = 1, /*!< USAGE_VERTEX */
	USAGE_SHADER_CONSTANT = 2, /*!< USAGE_SHADER_CONSTANT */
	USAGE_USER = 3, /*!< USAGE_USER */
}
static partial class Nif { //--DataStreamUsage--//
public static void NifStream(out DataStreamUsage val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (DataStreamUsage)temp; }
public static void NifStream(DataStreamUsage val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(DataStreamUsage val) { switch (val) {
	case DataStreamUsage.USAGE_VERTEX_INDEX: return "USAGE_VERTEX_INDEX";
	case DataStreamUsage.USAGE_VERTEX: return "USAGE_VERTEX";
	case DataStreamUsage.USAGE_SHADER_CONSTANT: return "USAGE_SHADER_CONSTANT";
	case DataStreamUsage.USAGE_USER: return "USAGE_USER";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the type of primitives stored in a mesh object. */
public enum MeshPrimitiveType : uint {
	MESH_PRIMITIVE_TRIANGLES = 0, /*!< Triangle primitive type. */
	MESH_PRIMITIVE_TRISTRIPS = 1, /*!< Triangle strip primitive type. */
	MESH_PRIMITIVE_LINES = 2, /*!< Lines primitive type. */
	MESH_PRIMITIVE_LINESTRIPS = 3, /*!< Line strip primitive type. */
	MESH_PRIMITIVE_QUADS = 4, /*!< Quadrilateral primitive type. */
	MESH_PRIMITIVE_POINTS = 5, /*!< Point primitive type. */
}
static partial class Nif { //--MeshPrimitiveType--//
public static void NifStream(out MeshPrimitiveType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (MeshPrimitiveType)temp; }
public static void NifStream(MeshPrimitiveType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(MeshPrimitiveType val) { switch (val) {
	case MeshPrimitiveType.MESH_PRIMITIVE_TRIANGLES: return "MESH_PRIMITIVE_TRIANGLES";
	case MeshPrimitiveType.MESH_PRIMITIVE_TRISTRIPS: return "MESH_PRIMITIVE_TRISTRIPS";
	case MeshPrimitiveType.MESH_PRIMITIVE_LINES: return "MESH_PRIMITIVE_LINES";
	case MeshPrimitiveType.MESH_PRIMITIVE_LINESTRIPS: return "MESH_PRIMITIVE_LINESTRIPS";
	case MeshPrimitiveType.MESH_PRIMITIVE_QUADS: return "MESH_PRIMITIVE_QUADS";
	case MeshPrimitiveType.MESH_PRIMITIVE_POINTS: return "MESH_PRIMITIVE_POINTS";
	default: return $"Invalid Value! - {val}";
}}}

/*! A sync point corresponds to a particular stage in per-frame processing. */
public enum SyncPoint : uint {
	SYNC_ANY = 0x8000, /*!< Synchronize for any sync points that the modifier supports. */
	SYNC_UPDATE = 0x8010, /*!< Synchronize when an object is updated. */
	SYNC_POST_UPDATE = 0x8020, /*!< Synchronize when an entire scene graph has been updated. */
	SYNC_VISIBLE = 0x8030, /*!< Synchronize when an object is determined to be potentially visible. */
	SYNC_RENDER = 0x8040, /*!< Synchronize when an object is rendered. */
	SYNC_PHYSICS_SIMULATE = 0x8050, /*!< Synchronize when a physics simulation step is about to begin. */
	SYNC_PHYSICS_COMPLETED = 0x8060, /*!< Synchronize when a physics simulation step has produced results. */
	SYNC_REFLECTIONS = 0x8070, /*!< Synchronize after all data necessary to calculate reflections is ready. */
}
static partial class Nif { //--SyncPoint--//
public static void NifStream(out SyncPoint val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (SyncPoint)temp; }
public static void NifStream(SyncPoint val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(SyncPoint val) { switch (val) {
	case SyncPoint.SYNC_ANY: return "SYNC_ANY";
	case SyncPoint.SYNC_UPDATE: return "SYNC_UPDATE";
	case SyncPoint.SYNC_POST_UPDATE: return "SYNC_POST_UPDATE";
	case SyncPoint.SYNC_VISIBLE: return "SYNC_VISIBLE";
	case SyncPoint.SYNC_RENDER: return "SYNC_RENDER";
	case SyncPoint.SYNC_PHYSICS_SIMULATE: return "SYNC_PHYSICS_SIMULATE";
	case SyncPoint.SYNC_PHYSICS_COMPLETED: return "SYNC_PHYSICS_COMPLETED";
	case SyncPoint.SYNC_REFLECTIONS: return "SYNC_REFLECTIONS";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Describes the various methods that may be used to specify the orientation of the
 * particles.
 */
public enum AlignMethod : uint {
	ALIGN_INVALID = 0, /*!< ALIGN_INVALID */
	ALIGN_PER_PARTICLE = 1, /*!< ALIGN_PER_PARTICLE */
	ALIGN_LOCAL_FIXED = 2, /*!< ALIGN_LOCAL_FIXED */
	ALIGN_LOCAL_POSITION = 5, /*!< ALIGN_LOCAL_POSITION */
	ALIGN_LOCAL_VELOCITY = 9, /*!< ALIGN_LOCAL_VELOCITY */
	ALIGN_CAMERA = 16, /*!< ALIGN_CAMERA */
}
static partial class Nif { //--AlignMethod--//
public static void NifStream(out AlignMethod val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (AlignMethod)temp; }
public static void NifStream(AlignMethod val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(AlignMethod val) { switch (val) {
	case AlignMethod.ALIGN_INVALID: return "ALIGN_INVALID";
	case AlignMethod.ALIGN_PER_PARTICLE: return "ALIGN_PER_PARTICLE";
	case AlignMethod.ALIGN_LOCAL_FIXED: return "ALIGN_LOCAL_FIXED";
	case AlignMethod.ALIGN_LOCAL_POSITION: return "ALIGN_LOCAL_POSITION";
	case AlignMethod.ALIGN_LOCAL_VELOCITY: return "ALIGN_LOCAL_VELOCITY";
	case AlignMethod.ALIGN_CAMERA: return "ALIGN_CAMERA";
	default: return $"Invalid Value! - {val}";
}}}

public enum PSLoopBehavior : uint {
	PS_LOOP_CLAMP_BIRTH = 0, /*!< Key times map such that the first key occurs at the birth of the particle, and times later than the last key get the last key value. */
	PS_LOOP_CLAMP_DEATH = 1, /*!< Key times map such that the last key occurs at the death of the particle, and times before the initial key time get the value of the initial key. */
	PS_LOOP_AGESCALE = 2, /*!< Scale the animation to fit the particle lifetime, so that the first key is age zero, and the last key comes at the particle death. */
	PS_LOOP_LOOP = 3, /*!< The time is converted to one within the time range represented by the keys, as if the key sequence loops forever in the past and future. */
	PS_LOOP_REFLECT = 4, /*!< The time is reflection looped, as if the keys played forward then backward the forward then backward etc for all time. */
}
static partial class Nif { //--PSLoopBehavior--//
public static void NifStream(out PSLoopBehavior val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PSLoopBehavior)temp; }
public static void NifStream(PSLoopBehavior val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PSLoopBehavior val) { switch (val) {
	case PSLoopBehavior.PS_LOOP_CLAMP_BIRTH: return "PS_LOOP_CLAMP_BIRTH";
	case PSLoopBehavior.PS_LOOP_CLAMP_DEATH: return "PS_LOOP_CLAMP_DEATH";
	case PSLoopBehavior.PS_LOOP_AGESCALE: return "PS_LOOP_AGESCALE";
	case PSLoopBehavior.PS_LOOP_LOOP: return "PS_LOOP_LOOP";
	case PSLoopBehavior.PS_LOOP_REFLECT: return "PS_LOOP_REFLECT";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * This is used by the Floodgate kernel to determine which NiPSForceHelpers
 * functions to call.
 */
public enum PSForceType : uint {
	FORCE_BOMB = 0, /*!< FORCE_BOMB */
	FORCE_DRAG = 1, /*!< FORCE_DRAG */
	FORCE_AIR_FIELD = 2, /*!< FORCE_AIR_FIELD */
	FORCE_DRAG_FIELD = 3, /*!< FORCE_DRAG_FIELD */
	FORCE_GRAVITY_FIELD = 4, /*!< FORCE_GRAVITY_FIELD */
	FORCE_RADIAL_FIELD = 5, /*!< FORCE_RADIAL_FIELD */
	FORCE_TURBULENCE_FIELD = 6, /*!< FORCE_TURBULENCE_FIELD */
	FORCE_VORTEX_FIELD = 7, /*!< FORCE_VORTEX_FIELD */
	FORCE_GRAVITY = 8, /*!< FORCE_GRAVITY */
}
static partial class Nif { //--PSForceType--//
public static void NifStream(out PSForceType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (PSForceType)temp; }
public static void NifStream(PSForceType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(PSForceType val) { switch (val) {
	case PSForceType.FORCE_BOMB: return "FORCE_BOMB";
	case PSForceType.FORCE_DRAG: return "FORCE_DRAG";
	case PSForceType.FORCE_AIR_FIELD: return "FORCE_AIR_FIELD";
	case PSForceType.FORCE_DRAG_FIELD: return "FORCE_DRAG_FIELD";
	case PSForceType.FORCE_GRAVITY_FIELD: return "FORCE_GRAVITY_FIELD";
	case PSForceType.FORCE_RADIAL_FIELD: return "FORCE_RADIAL_FIELD";
	case PSForceType.FORCE_TURBULENCE_FIELD: return "FORCE_TURBULENCE_FIELD";
	case PSForceType.FORCE_VORTEX_FIELD: return "FORCE_VORTEX_FIELD";
	case PSForceType.FORCE_GRAVITY: return "FORCE_GRAVITY";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * This is used by the Floodgate kernel to determine which NiPSColliderHelpers
 * functions to call.
 */
public enum ColliderType : uint {
	COLLIDER_PLANAR = 0, /*!< COLLIDER_PLANAR */
	COLLIDER_SPHERICAL = 1, /*!< COLLIDER_SPHERICAL */
}
static partial class Nif { //--ColliderType--//
public static void NifStream(out ColliderType val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (ColliderType)temp; }
public static void NifStream(ColliderType val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(ColliderType val) { switch (val) {
	case ColliderType.COLLIDER_PLANAR: return "COLLIDER_PLANAR";
	case ColliderType.COLLIDER_SPHERICAL: return "COLLIDER_SPHERICAL";
	default: return $"Invalid Value! - {val}";
}}}

/*! Describes the options for the accum root on NiControllerSequence. */
public enum AccumFlags : uint {
	ACCUM_X_TRANS = 1, /*!< X Translation will be accumulated. */
	ACCUM_Y_TRANS = 2, /*!< Y Translation will be accumulated. */
	ACCUM_Z_TRANS = 4, /*!< Z Translation will be accumulated. */
	ACCUM_X_ROT = 8, /*!< X Rotation will be accumulated. */
	ACCUM_Y_ROT = 16, /*!< Y Rotation will be accumulated. */
	ACCUM_Z_ROT = 32, /*!< Z Rotation will be accumulated. */
	ACCUM_X_FRONT = 64, /*!< +X is front facing. (Default) */
	ACCUM_Y_FRONT = 128, /*!< +Y is front facing. */
	ACCUM_Z_FRONT = 256, /*!< +Z is front facing. */
	ACCUM_NEG_FRONT = 512, /*!< -X is front facing. */
}
static partial class Nif { //--AccumFlags--//
public static void NifStream(out AccumFlags val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (AccumFlags)temp; }
public static void NifStream(AccumFlags val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(AccumFlags val) { switch (val) {
	case AccumFlags.ACCUM_X_TRANS: return "ACCUM_X_TRANS";
	case AccumFlags.ACCUM_Y_TRANS: return "ACCUM_Y_TRANS";
	case AccumFlags.ACCUM_Z_TRANS: return "ACCUM_Z_TRANS";
	case AccumFlags.ACCUM_X_ROT: return "ACCUM_X_ROT";
	case AccumFlags.ACCUM_Y_ROT: return "ACCUM_Y_ROT";
	case AccumFlags.ACCUM_Z_ROT: return "ACCUM_Z_ROT";
	case AccumFlags.ACCUM_X_FRONT: return "ACCUM_X_FRONT";
	case AccumFlags.ACCUM_Y_FRONT: return "ACCUM_Y_FRONT";
	case AccumFlags.ACCUM_Z_FRONT: return "ACCUM_Z_FRONT";
	case AccumFlags.ACCUM_NEG_FRONT: return "ACCUM_NEG_FRONT";
	default: return $"Invalid Value! - {val}";
}}}

public enum VertexFlags : uint {
	VF_VERTEX = 16, /*!< VF_Vertex */
	VF_UVS = 32, /*!< VF_UVs */
	VF_UVS_2 = 64, /*!< VF_UVs_2 */
	VF_NORMALS = 128, /*!< VF_Normals */
	VF_TANGENTS = 256, /*!< VF_Tangents */
	VF_VERTEX_COLORS = 512, /*!< VF_Vertex_Colors */
	VF_SKINNED = 1024, /*!< VF_Skinned */
	VF_LAND_DATA = 2048, /*!< VF_Land_Data */
	VF_EYE_DATA = 4096, /*!< VF_Eye_Data */
	VF_INSTANCE = 8192, /*!< VF_Instance */
	VF_FULL_PRECISION = 16384, /*!< VF_Full_Precision */
}
static partial class Nif { //--VertexFlags--//
public static void NifStream(out VertexFlags val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (VertexFlags)temp; }
public static void NifStream(VertexFlags val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(VertexFlags val) { switch (val) {
	case VertexFlags.VF_VERTEX: return "VF_Vertex";
	case VertexFlags.VF_UVS: return "VF_UVs";
	case VertexFlags.VF_UVS_2: return "VF_UVs_2";
	case VertexFlags.VF_NORMALS: return "VF_Normals";
	case VertexFlags.VF_TANGENTS: return "VF_Tangents";
	case VertexFlags.VF_VERTEX_COLORS: return "VF_Vertex_Colors";
	case VertexFlags.VF_SKINNED: return "VF_Skinned";
	case VertexFlags.VF_LAND_DATA: return "VF_Land_Data";
	case VertexFlags.VF_EYE_DATA: return "VF_Eye_Data";
	case VertexFlags.VF_INSTANCE: return "VF_Instance";
	case VertexFlags.VF_FULL_PRECISION: return "VF_Full_Precision";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * Bethesda Animation. Furniture entry points. It specifies the direction(s) from
 * where the actor is able to enter (and leave) the position.
 */
public enum FurnitureEntryPoints : uint {
	FRONT = 1, /*!< front entry point */
	BEHIND = 2, /*!< behind entry point */
	RIGHT = 4, /*!< right entry point */
	LEFT = 8, /*!< left entry point */
	UP = 16, /*!< up entry point - unknown function. Used on some beds in Skyrim, probably for blocking of sleeping position. */
}
static partial class Nif { //--FurnitureEntryPoints--//
public static void NifStream(out FurnitureEntryPoints val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (FurnitureEntryPoints)temp; }
public static void NifStream(FurnitureEntryPoints val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(FurnitureEntryPoints val) { switch (val) {
	case FurnitureEntryPoints.FRONT: return "Front";
	case FurnitureEntryPoints.BEHIND: return "Behind";
	case FurnitureEntryPoints.RIGHT: return "Right";
	case FurnitureEntryPoints.LEFT: return "Left";
	case FurnitureEntryPoints.UP: return "Up";
	default: return $"Invalid Value! - {val}";
}}}

/*! Editor flags for the Body Partitions. */
public enum BSPartFlag : uint {
	PF_EDITOR_VISIBLE = 1, /*!< Visible in Editor */
	PF_START_NET_BONESET = 256, /*!< Start a new shared boneset.  It is expected this BoneSet and the following sets in the Skin Partition will have the same bones. */
}
static partial class Nif { //--BSPartFlag--//
public static void NifStream(out BSPartFlag val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (BSPartFlag)temp; }
public static void NifStream(BSPartFlag val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(BSPartFlag val) { switch (val) {
	case BSPartFlag.PF_EDITOR_VISIBLE: return "PF_EDITOR_VISIBLE";
	case BSPartFlag.PF_START_NET_BONESET: return "PF_START_NET_BONESET";
	default: return $"Invalid Value! - {val}";
}}}

public enum PathFlags : uint {
	NIPI_CVDATANEEDSUPDATE = 1, /*!< NIPI_CVDataNeedsUpdate */
	NIPI_CURVETYPEOPEN = 2, /*!< NIPI_CurveTypeOpen */
	NIPI_ALLOWFLIP = 4, /*!< NIPI_AllowFlip */
	NIPI_BANK = 8, /*!< NIPI_Bank */
	NIPI_CONSTANTVELOCITY = 16, /*!< NIPI_ConstantVelocity */
	NIPI_FOLLOW = 32, /*!< NIPI_Follow */
	NIPI_FLIP = 64, /*!< NIPI_Flip */
}
static partial class Nif { //--PathFlags--//
public static void NifStream(out PathFlags val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (PathFlags)temp; }
public static void NifStream(PathFlags val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(PathFlags val) { switch (val) {
	case PathFlags.NIPI_CVDATANEEDSUPDATE: return "NIPI_CVDataNeedsUpdate";
	case PathFlags.NIPI_CURVETYPEOPEN: return "NIPI_CurveTypeOpen";
	case PathFlags.NIPI_ALLOWFLIP: return "NIPI_AllowFlip";
	case PathFlags.NIPI_BANK: return "NIPI_Bank";
	case PathFlags.NIPI_CONSTANTVELOCITY: return "NIPI_ConstantVelocity";
	case PathFlags.NIPI_FOLLOW: return "NIPI_Follow";
	case PathFlags.NIPI_FLIP: return "NIPI_Flip";
	default: return $"Invalid Value! - {val}";
}}}

/*!
 * bhkNiCollisionObject flags. The flags 0x2, 0x100, and 0x200 are not seen in any
 * NIF nor get/set by the engine.
 */
public enum bhkCOFlags : uint {
	BHKCO_ACTIVE = 1, /*!< BHKCO_ACTIVE */
	BHKCO_NOTIFY = 4, /*!< BHKCO_NOTIFY */
	BHKCO_SET_LOCAL = 8, /*!< BHKCO_SET_LOCAL */
	BHKCO_DBG_DISPLAY = 16, /*!< BHKCO_DBG_DISPLAY */
	BHKCO_USE_VEL = 32, /*!< BHKCO_USE_VEL */
	BHKCO_RESET = 64, /*!< BHKCO_RESET */
	BHKCO_SYNC_ON_UPDATE = 128, /*!< BHKCO_SYNC_ON_UPDATE */
	BHKCO_ANIM_TARGETED = 1024, /*!< BHKCO_ANIM_TARGETED */
	BHKCO_DISMEMBERED_LIMB = 2048, /*!< BHKCO_DISMEMBERED_LIMB */
}
static partial class Nif { //--bhkCOFlags--//
public static void NifStream(out bhkCOFlags val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (bhkCOFlags)temp; }
public static void NifStream(bhkCOFlags val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(bhkCOFlags val) { switch (val) {
	case bhkCOFlags.BHKCO_ACTIVE: return "BHKCO_ACTIVE";
	case bhkCOFlags.BHKCO_NOTIFY: return "BHKCO_NOTIFY";
	case bhkCOFlags.BHKCO_SET_LOCAL: return "BHKCO_SET_LOCAL";
	case bhkCOFlags.BHKCO_DBG_DISPLAY: return "BHKCO_DBG_DISPLAY";
	case bhkCOFlags.BHKCO_USE_VEL: return "BHKCO_USE_VEL";
	case bhkCOFlags.BHKCO_RESET: return "BHKCO_RESET";
	case bhkCOFlags.BHKCO_SYNC_ON_UPDATE: return "BHKCO_SYNC_ON_UPDATE";
	case bhkCOFlags.BHKCO_ANIM_TARGETED: return "BHKCO_ANIM_TARGETED";
	case bhkCOFlags.BHKCO_DISMEMBERED_LIMB: return "BHKCO_DISMEMBERED_LIMB";
	default: return $"Invalid Value! - {val}";
}}}

public enum VectorFlags : uint {
	VF_UV_1 = 1, /*!< VF_UV_1 */
	VF_UV_2 = 2, /*!< VF_UV_2 */
	VF_UV_4 = 4, /*!< VF_UV_4 */
	VF_UV_8 = 8, /*!< VF_UV_8 */
	VF_UV_16 = 16, /*!< VF_UV_16 */
	VF_UV_32 = 32, /*!< VF_UV_32 */
	VF_UNK64 = 64, /*!< VF_Unk64 */
	VF_UNK128 = 128, /*!< VF_Unk128 */
	VF_UNK256 = 256, /*!< VF_Unk256 */
	VF_UNK512 = 512, /*!< VF_Unk512 */
	VF_UNK1024 = 1024, /*!< VF_Unk1024 */
	VF_UNK2048 = 2048, /*!< VF_Unk2048 */
	VF_HAS_TANGENTS = 4096, /*!< VF_Has_Tangents */
	VF_UNK8192 = 8192, /*!< VF_Unk8192 */
	VF_UNK16384 = 16384, /*!< VF_Unk16384 */
	VF_UNK32768 = 32768, /*!< VF_Unk32768 */
}
static partial class Nif { //--VectorFlags--//
public static void NifStream(out VectorFlags val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (VectorFlags)temp; }
public static void NifStream(VectorFlags val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(VectorFlags val) { switch (val) {
	case VectorFlags.VF_UV_1: return "VF_UV_1";
	case VectorFlags.VF_UV_2: return "VF_UV_2";
	case VectorFlags.VF_UV_4: return "VF_UV_4";
	case VectorFlags.VF_UV_8: return "VF_UV_8";
	case VectorFlags.VF_UV_16: return "VF_UV_16";
	case VectorFlags.VF_UV_32: return "VF_UV_32";
	case VectorFlags.VF_UNK64: return "VF_Unk64";
	case VectorFlags.VF_UNK128: return "VF_Unk128";
	case VectorFlags.VF_UNK256: return "VF_Unk256";
	case VectorFlags.VF_UNK512: return "VF_Unk512";
	case VectorFlags.VF_UNK1024: return "VF_Unk1024";
	case VectorFlags.VF_UNK2048: return "VF_Unk2048";
	case VectorFlags.VF_HAS_TANGENTS: return "VF_Has_Tangents";
	case VectorFlags.VF_UNK8192: return "VF_Unk8192";
	case VectorFlags.VF_UNK16384: return "VF_Unk16384";
	case VectorFlags.VF_UNK32768: return "VF_Unk32768";
	default: return $"Invalid Value! - {val}";
}}}

public enum BSVectorFlags : uint {
	BSVF_HAS_UV = 1, /*!< BSVF_Has_UV */
	BSVF_UNK2 = 2, /*!< BSVF_Unk2 */
	BSVF_UNK4 = 4, /*!< BSVF_Unk4 */
	BSVF_UNK8 = 8, /*!< BSVF_Unk8 */
	BSVF_UNK16 = 16, /*!< BSVF_Unk16 */
	BSVF_UNK32 = 32, /*!< BSVF_Unk32 */
	BSVF_UNK64 = 64, /*!< BSVF_Unk64 */
	BSVF_UNK128 = 128, /*!< BSVF_Unk128 */
	BSVF_UNK256 = 256, /*!< BSVF_Unk256 */
	BSVF_UNK512 = 512, /*!< BSVF_Unk512 */
	BSVF_UNK1024 = 1024, /*!< BSVF_Unk1024 */
	BSVF_UNK2048 = 2048, /*!< BSVF_Unk2048 */
	BSVF_HAS_TANGENTS = 4096, /*!< BSVF_Has_Tangents */
	BSVF_UNK8192 = 8192, /*!< BSVF_Unk8192 */
	BSVF_UNK16384 = 16384, /*!< BSVF_Unk16384 */
	BSVF_UNK32768 = 32768, /*!< BSVF_Unk32768 */
}
static partial class Nif { //--BSVectorFlags--//
public static void NifStream(out BSVectorFlags val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (BSVectorFlags)temp; }
public static void NifStream(BSVectorFlags val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(BSVectorFlags val) { switch (val) {
	case BSVectorFlags.BSVF_HAS_UV: return "BSVF_Has_UV";
	case BSVectorFlags.BSVF_UNK2: return "BSVF_Unk2";
	case BSVectorFlags.BSVF_UNK4: return "BSVF_Unk4";
	case BSVectorFlags.BSVF_UNK8: return "BSVF_Unk8";
	case BSVectorFlags.BSVF_UNK16: return "BSVF_Unk16";
	case BSVectorFlags.BSVF_UNK32: return "BSVF_Unk32";
	case BSVectorFlags.BSVF_UNK64: return "BSVF_Unk64";
	case BSVectorFlags.BSVF_UNK128: return "BSVF_Unk128";
	case BSVectorFlags.BSVF_UNK256: return "BSVF_Unk256";
	case BSVectorFlags.BSVF_UNK512: return "BSVF_Unk512";
	case BSVectorFlags.BSVF_UNK1024: return "BSVF_Unk1024";
	case BSVectorFlags.BSVF_UNK2048: return "BSVF_Unk2048";
	case BSVectorFlags.BSVF_HAS_TANGENTS: return "BSVF_Has_Tangents";
	case BSVectorFlags.BSVF_UNK8192: return "BSVF_Unk8192";
	case BSVectorFlags.BSVF_UNK16384: return "BSVF_Unk16384";
	case BSVectorFlags.BSVF_UNK32768: return "BSVF_Unk32768";
	default: return $"Invalid Value! - {val}";
}}}

public enum LookAtFlags : uint {
	LOOK_FLIP = 1, /*!< Flip */
	LOOK_Y_AXIS = 2, /*!< Y-Axis */
	LOOK_Z_AXIS = 4, /*!< Z-Axis */
}
static partial class Nif { //--LookAtFlags--//
public static void NifStream(out LookAtFlags val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (LookAtFlags)temp; }
public static void NifStream(LookAtFlags val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(LookAtFlags val) { switch (val) {
	case LookAtFlags.LOOK_FLIP: return "LOOK_FLIP";
	case LookAtFlags.LOOK_Y_AXIS: return "LOOK_Y_AXIS";
	case LookAtFlags.LOOK_Z_AXIS: return "LOOK_Z_AXIS";
	default: return $"Invalid Value! - {val}";
}}}

/*! Flags for NiSwitchNode. */
public enum NiSwitchFlags : uint {
	UPDATEONLYACTIVECHILD = 1, /*!< Update Only Active Child */
	UPDATECONTROLLERS = 2, /*!< Update Controllers */
}
static partial class Nif { //--NiSwitchFlags--//
public static void NifStream(out NiSwitchFlags val, IStream s, NifInfo info) { ushort temp; NifStream(out temp, s, info); val = (NiSwitchFlags)temp; }
public static void NifStream(NiSwitchFlags val, OStream s, NifInfo info) => NifStream((ushort)val, s, info);
public static string AsString(NiSwitchFlags val) { switch (val) {
	case NiSwitchFlags.UPDATEONLYACTIVECHILD: return "UpdateOnlyActiveChild";
	case NiSwitchFlags.UPDATECONTROLLERS: return "UpdateControllers";
	default: return $"Invalid Value! - {val}";
}}}

public enum NxMaterialFlag : uint {
	NX_MF_ANISOTROPIC = 2, /*!< NX_MF_ANISOTROPIC */
	NX_MF_DUMMY1 = 4, /*!< NX_MF_DUMMY1 */
	NX_MF_DUMMY2 = 8, /*!< NX_MF_DUMMY2 */
	NX_MF_DUMMY3 = 16, /*!< NX_MF_DUMMY3 */
	NX_MF_DISABLE_FRICTION = 32, /*!< NX_MF_DISABLE_FRICTION */
	NX_MF_DISABLE_STRONG_FRICTION = 64, /*!< NX_MF_DISABLE_STRONG_FRICTION */
}
static partial class Nif { //--NxMaterialFlag--//
public static void NifStream(out NxMaterialFlag val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (NxMaterialFlag)temp; }
public static void NifStream(NxMaterialFlag val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(NxMaterialFlag val) { switch (val) {
	case NxMaterialFlag.NX_MF_ANISOTROPIC: return "NX_MF_ANISOTROPIC";
	case NxMaterialFlag.NX_MF_DUMMY1: return "NX_MF_DUMMY1";
	case NxMaterialFlag.NX_MF_DUMMY2: return "NX_MF_DUMMY2";
	case NxMaterialFlag.NX_MF_DUMMY3: return "NX_MF_DUMMY3";
	case NxMaterialFlag.NX_MF_DISABLE_FRICTION: return "NX_MF_DISABLE_FRICTION";
	case NxMaterialFlag.NX_MF_DISABLE_STRONG_FRICTION: return "NX_MF_DISABLE_STRONG_FRICTION";
	default: return $"Invalid Value! - {val}";
}}}

/*! Shader Property Flags */
public enum BSShaderFlags : uint {
	F3SF1_SPECULAR = 1, /*!< Enables Specularity */
	F3SF1_SKINNED = 2, /*!< Required For Skinned Meshes */
	F3SF1_LOWDETAIL = 4, /*!< Lowddetail (seems to use standard diff/norm/spec shader) */
	F3SF1_VERTEX_ALPHA = 8, /*!< Vertex Alpha */
	F3SF1_UNKNOWN_1 = 16, /*!< Unknown */
	F3SF1_SINGLE_PASS = 32, /*!< Single Pass */
	F3SF1_EMPTY = 64, /*!< Unknown */
	F3SF1_ENVIRONMENT_MAPPING = 128, /*!< Environment mapping (uses Envmap Scale) */
	F3SF1_ALPHA_TEXTURE = 256, /*!< Alpha Texture Requires NiAlphaProperty to Enable */
	F3SF1_UNKNOWN_2 = 512, /*!< Unknown */
	F3SF1_FACEGEN = 1024, /*!< FaceGen */
	F3SF1_PARALLAX_SHADER_INDEX_15 = 2048, /*!< Parallax */
	F3SF1_UNKNOWN_3 = 4096, /*!< Unknown/Crash */
	F3SF1_NON_PROJECTIVE_SHADOWS = 8192, /*!< Non-Projective Shadows */
	F3SF1_UNKNOWN_4 = 16384, /*!< Unknown/Crash */
	F3SF1_REFRACTION = 32768, /*!< Refraction (switches on refraction power) */
	F3SF1_FIRE_REFRACTION = 65536, /*!< Fire Refraction (switches on refraction power/period) */
	F3SF1_EYE_ENVIRONMENT_MAPPING = 131072, /*!< Eye Environment Mapping (does not use envmap light fade or envmap scale) */
	F3SF1_HAIR = 262144, /*!< Hair */
	F3SF1_DYNAMIC_ALPHA = 524288, /*!< Dynamic Alpha */
	F3SF1_LOCALMAP_HIDE_SECRET = 1048576, /*!< Localmap Hide Secret */
	F3SF1_WINDOW_ENVIRONMENT_MAPPING = 2097152, /*!< Window Environment Mapping */
	F3SF1_TREE_BILLBOARD = 4194304, /*!< Tree Billboard */
	F3SF1_SHADOW_FRUSTUM = 8388608, /*!< Shadow Frustum */
	F3SF1_MULTIPLE_TEXTURES = 16777216, /*!< Multiple Textures (base diff/norm become null) */
	F3SF1_REMAPPABLE_TEXTURES = 33554432, /*!< usually seen w/texture animation */
	F3SF1_DECAL_SINGLE_PASS = 67108864, /*!< Decal */
	F3SF1_DYNAMIC_DECAL_SINGLE_PASS = 134217728, /*!< Dynamic Decal */
	F3SF1_PARALLAX_OCCULSION = 268435456, /*!< Parallax Occlusion */
	F3SF1_EXTERNAL_EMITTANCE = 536870912, /*!< External Emittance */
	F3SF1_SHADOW_MAP = 1073741824, /*!< Shadow Map */
	F3SF1_ZBUFFER_TEST = 2147483648, /*!< ZBuffer Test (1=on) */
}
static partial class Nif { //--BSShaderFlags--//
public static void NifStream(out BSShaderFlags val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (BSShaderFlags)temp; }
public static void NifStream(BSShaderFlags val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(BSShaderFlags val) { switch (val) {
	case BSShaderFlags.F3SF1_SPECULAR: return "F3SF1_Specular";
	case BSShaderFlags.F3SF1_SKINNED: return "F3SF1_Skinned";
	case BSShaderFlags.F3SF1_LOWDETAIL: return "F3SF1_LowDetail";
	case BSShaderFlags.F3SF1_VERTEX_ALPHA: return "F3SF1_Vertex_Alpha";
	case BSShaderFlags.F3SF1_UNKNOWN_1: return "F3SF1_Unknown_1";
	case BSShaderFlags.F3SF1_SINGLE_PASS: return "F3SF1_Single_Pass";
	case BSShaderFlags.F3SF1_EMPTY: return "F3SF1_Empty";
	case BSShaderFlags.F3SF1_ENVIRONMENT_MAPPING: return "F3SF1_Environment_Mapping";
	case BSShaderFlags.F3SF1_ALPHA_TEXTURE: return "F3SF1_Alpha_Texture";
	case BSShaderFlags.F3SF1_UNKNOWN_2: return "F3SF1_Unknown_2";
	case BSShaderFlags.F3SF1_FACEGEN: return "F3SF1_FaceGen";
	case BSShaderFlags.F3SF1_PARALLAX_SHADER_INDEX_15: return "F3SF1_Parallax_Shader_Index_15";
	case BSShaderFlags.F3SF1_UNKNOWN_3: return "F3SF1_Unknown_3";
	case BSShaderFlags.F3SF1_NON_PROJECTIVE_SHADOWS: return "F3SF1_Non_Projective_Shadows";
	case BSShaderFlags.F3SF1_UNKNOWN_4: return "F3SF1_Unknown_4";
	case BSShaderFlags.F3SF1_REFRACTION: return "F3SF1_Refraction";
	case BSShaderFlags.F3SF1_FIRE_REFRACTION: return "F3SF1_Fire_Refraction";
	case BSShaderFlags.F3SF1_EYE_ENVIRONMENT_MAPPING: return "F3SF1_Eye_Environment_Mapping";
	case BSShaderFlags.F3SF1_HAIR: return "F3SF1_Hair";
	case BSShaderFlags.F3SF1_DYNAMIC_ALPHA: return "F3SF1_Dynamic_Alpha";
	case BSShaderFlags.F3SF1_LOCALMAP_HIDE_SECRET: return "F3SF1_Localmap_Hide_Secret";
	case BSShaderFlags.F3SF1_WINDOW_ENVIRONMENT_MAPPING: return "F3SF1_Window_Environment_Mapping";
	case BSShaderFlags.F3SF1_TREE_BILLBOARD: return "F3SF1_Tree_Billboard";
	case BSShaderFlags.F3SF1_SHADOW_FRUSTUM: return "F3SF1_Shadow_Frustum";
	case BSShaderFlags.F3SF1_MULTIPLE_TEXTURES: return "F3SF1_Multiple_Textures";
	case BSShaderFlags.F3SF1_REMAPPABLE_TEXTURES: return "F3SF1_Remappable_Textures";
	case BSShaderFlags.F3SF1_DECAL_SINGLE_PASS: return "F3SF1_Decal_Single_Pass";
	case BSShaderFlags.F3SF1_DYNAMIC_DECAL_SINGLE_PASS: return "F3SF1_Dynamic_Decal_Single_Pass";
	case BSShaderFlags.F3SF1_PARALLAX_OCCULSION: return "F3SF1_Parallax_Occulsion";
	case BSShaderFlags.F3SF1_EXTERNAL_EMITTANCE: return "F3SF1_External_Emittance";
	case BSShaderFlags.F3SF1_SHADOW_MAP: return "F3SF1_Shadow_Map";
	case BSShaderFlags.F3SF1_ZBUFFER_TEST: return "F3SF1_ZBuffer_Test";
	default: return $"Invalid Value! - {val}";
}}}

/*! Shader Property Flags 2 */
public enum BSShaderFlags2 : uint {
	F3SF2_ZBUFFER_WRITE = 1, /*!< ZBuffer Write */
	F3SF2_LOD_LANDSCAPE = 2, /*!< LOD Landscape */
	F3SF2_LOD_BUILDING = 4, /*!< LOD Building */
	F3SF2_NO_FADE = 8, /*!< No Fade */
	F3SF2_REFRACTION_TINT = 16, /*!< Refraction Tint */
	F3SF2_VERTEX_COLORS = 32, /*!< Has Vertex Colors */
	F3SF2_UNKNOWN1 = 64, /*!< Unknown */
	F3SF2_1ST_LIGHT_IS_POINT_LIGHT = 128, /*!< 1st Light is Point Light */
	F3SF2_2ND_LIGHT = 256, /*!< 2nd Light */
	F3SF2_3RD_LIGHT = 512, /*!< 3rd Light */
	F3SF2_VERTEX_LIGHTING = 1024, /*!< Vertex Lighting */
	F3SF2_UNIFORM_SCALE = 2048, /*!< Uniform Scale */
	F3SF2_FIT_SLOPE = 4096, /*!< Fit Slope */
	F3SF2_BILLBOARD_AND_ENVMAP_LIGHT_FADE = 8192, /*!< Billboard and Envmap Light Fade */
	F3SF2_NO_LOD_LAND_BLEND = 16384, /*!< No LOD Land Blend */
	F3SF2_ENVMAP_LIGHT_FADE = 32768, /*!< Envmap Light Fade */
	F3SF2_WIREFRAME = 65536, /*!< Wireframe */
	F3SF2_VATS_SELECTION = 131072, /*!< VATS Selection */
	F3SF2_SHOW_IN_LOCAL_MAP = 262144, /*!< Show in Local Map */
	F3SF2_PREMULT_ALPHA = 524288, /*!< Premult Alpha */
	F3SF2_SKIP_NORMAL_MAPS = 1048576, /*!< Skip Normal Maps */
	F3SF2_ALPHA_DECAL = 2097152, /*!< Alpha Decal */
	F3SF2_NO_TRANSPARECNY_MULTISAMPLING = 4194304, /*!< No Transparency MultiSampling */
	F3SF2_UNKNOWN2 = 8388608, /*!< Unknown */
	F3SF2_UNKNOWN3 = 16777216, /*!< Unknown */
	F3SF2_UNKNOWN4 = 33554432, /*!< Unknown */
	F3SF2_UNKNOWN5 = 67108864, /*!< Unknown */
	F3SF2_UNKNOWN6 = 134217728, /*!< Unknown */
	F3SF2_UNKNOWN7 = 268435456, /*!< Unknown */
	F3SF2_UNKNOWN8 = 536870912, /*!< Unknown */
	F3SF2_UNKNOWN9 = 1073741824, /*!< Unknown */
	F3SF2_UNKNOWN10 = 2147483648, /*!< Unknown */
}
static partial class Nif { //--BSShaderFlags2--//
public static void NifStream(out BSShaderFlags2 val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (BSShaderFlags2)temp; }
public static void NifStream(BSShaderFlags2 val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(BSShaderFlags2 val) { switch (val) {
	case BSShaderFlags2.F3SF2_ZBUFFER_WRITE: return "F3SF2_ZBuffer_Write";
	case BSShaderFlags2.F3SF2_LOD_LANDSCAPE: return "F3SF2_LOD_Landscape";
	case BSShaderFlags2.F3SF2_LOD_BUILDING: return "F3SF2_LOD_Building";
	case BSShaderFlags2.F3SF2_NO_FADE: return "F3SF2_No_Fade";
	case BSShaderFlags2.F3SF2_REFRACTION_TINT: return "F3SF2_Refraction_Tint";
	case BSShaderFlags2.F3SF2_VERTEX_COLORS: return "F3SF2_Vertex_Colors";
	case BSShaderFlags2.F3SF2_UNKNOWN1: return "F3SF2_Unknown1";
	case BSShaderFlags2.F3SF2_1ST_LIGHT_IS_POINT_LIGHT: return "F3SF2_1st_Light_is_Point_Light";
	case BSShaderFlags2.F3SF2_2ND_LIGHT: return "F3SF2_2nd_Light";
	case BSShaderFlags2.F3SF2_3RD_LIGHT: return "F3SF2_3rd_Light";
	case BSShaderFlags2.F3SF2_VERTEX_LIGHTING: return "F3SF2_Vertex_Lighting";
	case BSShaderFlags2.F3SF2_UNIFORM_SCALE: return "F3SF2_Uniform_Scale";
	case BSShaderFlags2.F3SF2_FIT_SLOPE: return "F3SF2_Fit_Slope";
	case BSShaderFlags2.F3SF2_BILLBOARD_AND_ENVMAP_LIGHT_FADE: return "F3SF2_Billboard_and_Envmap_Light_Fade";
	case BSShaderFlags2.F3SF2_NO_LOD_LAND_BLEND: return "F3SF2_No_LOD_Land_Blend";
	case BSShaderFlags2.F3SF2_ENVMAP_LIGHT_FADE: return "F3SF2_Envmap_Light_Fade";
	case BSShaderFlags2.F3SF2_WIREFRAME: return "F3SF2_Wireframe";
	case BSShaderFlags2.F3SF2_VATS_SELECTION: return "F3SF2_VATS_Selection";
	case BSShaderFlags2.F3SF2_SHOW_IN_LOCAL_MAP: return "F3SF2_Show_in_Local_Map";
	case BSShaderFlags2.F3SF2_PREMULT_ALPHA: return "F3SF2_Premult_Alpha";
	case BSShaderFlags2.F3SF2_SKIP_NORMAL_MAPS: return "F3SF2_Skip_Normal_Maps";
	case BSShaderFlags2.F3SF2_ALPHA_DECAL: return "F3SF2_Alpha_Decal";
	case BSShaderFlags2.F3SF2_NO_TRANSPARECNY_MULTISAMPLING: return "F3SF2_No_Transparecny_Multisampling";
	case BSShaderFlags2.F3SF2_UNKNOWN2: return "F3SF2_Unknown2";
	case BSShaderFlags2.F3SF2_UNKNOWN3: return "F3SF2_Unknown3";
	case BSShaderFlags2.F3SF2_UNKNOWN4: return "F3SF2_Unknown4";
	case BSShaderFlags2.F3SF2_UNKNOWN5: return "F3SF2_Unknown5";
	case BSShaderFlags2.F3SF2_UNKNOWN6: return "F3SF2_Unknown6";
	case BSShaderFlags2.F3SF2_UNKNOWN7: return "F3SF2_Unknown7";
	case BSShaderFlags2.F3SF2_UNKNOWN8: return "F3SF2_Unknown8";
	case BSShaderFlags2.F3SF2_UNKNOWN9: return "F3SF2_Unknown9";
	case BSShaderFlags2.F3SF2_UNKNOWN10: return "F3SF2_Unknown10";
	default: return $"Invalid Value! - {val}";
}}}

/*! Skyrim Shader Property Flags 1 */
public enum SkyrimShaderPropertyFlags1 : uint {
	SLSF1_SPECULAR = 1, /*!< Enables Specularity */
	SLSF1_SKINNED = 2, /*!< Required For Skinned Meshes. */
	SLSF1_TEMP_REFRACTION = 4, /*!< SLSF1_Temp_Refraction */
	SLSF1_VERTEX_ALPHA = 8, /*!< Enables using alpha component of vertex colors. */
	SLSF1_GREYSCALE_TO_PALETTECOLOR = 16, /*!< in EffectShaderProperty */
	SLSF1_GREYSCALE_TO_PALETTEALPHA = 32, /*!< in EffectShaderProperty */
	SLSF1_USE_FALLOFF = 64, /*!< Use Falloff value in EffectShaderProperty */
	SLSF1_ENVIRONMENT_MAPPING = 128, /*!< Environment mapping (uses Envmap Scale). */
	SLSF1_RECIEVE_SHADOWS = 256, /*!< Object can recieve shadows. */
	SLSF1_CAST_SHADOWS = 512, /*!< Can cast shadows */
	SLSF1_FACEGEN_DETAIL_MAP = 1024, /*!< Use a face detail map in the 4th texture slot. */
	SLSF1_PARALLAX = 2048, /*!< Unused? */
	SLSF1_MODEL_SPACE_NORMALS = 4096, /*!< Use Model space normals and an external Specular Map. */
	SLSF1_NON_PROJECTIVE_SHADOWS = 8192, /*!< SLSF1_Non_Projective_Shadows */
	SLSF1_LANDSCAPE = 16384, /*!< SLSF1_Landscape */
	SLSF1_REFRACTION = 32768, /*!< Use normal map for refraction effect. */
	SLSF1_FIRE_REFRACTION = 65536, /*!< SLSF1_Fire_Refraction */
	SLSF1_EYE_ENVIRONMENT_MAPPING = 131072, /*!< Eye Environment Mapping (Must use the Eye shader and the model must be skinned) */
	SLSF1_HAIR_SOFT_LIGHTING = 262144, /*!< Keeps from going too bright under lights (hair shader only) */
	SLSF1_SCREENDOOR_ALPHA_FADE = 524288, /*!< SLSF1_Screendoor_Alpha_Fade */
	SLSF1_LOCALMAP_HIDE_SECRET = 1048576, /*!< Object and anything it is positioned above will not render on local map view. */
	SLSF1_FACEGEN_RGB_TINT = 2097152, /*!< Use tintmask for Face. */
	SLSF1_OWN_EMIT = 4194304, /*!< Provides its own emittance color. (will not absorb light/ambient color?) */
	SLSF1_PROJECTED_UV = 8388608, /*!< Used for decalling? */
	SLSF1_MULTIPLE_TEXTURES = 16777216, /*!< SLSF1_Multiple_Textures */
	SLSF1_REMAPPABLE_TEXTURES = 33554432, /*!< SLSF1_Remappable_Textures */
	SLSF1_DECAL = 67108864, /*!< SLSF1_Decal */
	SLSF1_DYNAMIC_DECAL = 134217728, /*!< SLSF1_Dynamic_Decal */
	SLSF1_PARALLAX_OCCLUSION = 268435456, /*!< SLSF1_Parallax_Occlusion */
	SLSF1_EXTERNAL_EMITTANCE = 536870912, /*!< SLSF1_External_Emittance */
	SLSF1_SOFT_EFFECT = 1073741824, /*!< SLSF1_Soft_Effect */
	SLSF1_ZBUFFER_TEST = 2147483648, /*!< ZBuffer Test (1=on) */
}
static partial class Nif { //--SkyrimShaderPropertyFlags1--//
public static void NifStream(out SkyrimShaderPropertyFlags1 val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (SkyrimShaderPropertyFlags1)temp; }
public static void NifStream(SkyrimShaderPropertyFlags1 val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(SkyrimShaderPropertyFlags1 val) { switch (val) {
	case SkyrimShaderPropertyFlags1.SLSF1_SPECULAR: return "SLSF1_Specular";
	case SkyrimShaderPropertyFlags1.SLSF1_SKINNED: return "SLSF1_Skinned";
	case SkyrimShaderPropertyFlags1.SLSF1_TEMP_REFRACTION: return "SLSF1_Temp_Refraction";
	case SkyrimShaderPropertyFlags1.SLSF1_VERTEX_ALPHA: return "SLSF1_Vertex_Alpha";
	case SkyrimShaderPropertyFlags1.SLSF1_GREYSCALE_TO_PALETTECOLOR: return "SLSF1_Greyscale_To_PaletteColor";
	case SkyrimShaderPropertyFlags1.SLSF1_GREYSCALE_TO_PALETTEALPHA: return "SLSF1_Greyscale_To_PaletteAlpha";
	case SkyrimShaderPropertyFlags1.SLSF1_USE_FALLOFF: return "SLSF1_Use_Falloff";
	case SkyrimShaderPropertyFlags1.SLSF1_ENVIRONMENT_MAPPING: return "SLSF1_Environment_Mapping";
	case SkyrimShaderPropertyFlags1.SLSF1_RECIEVE_SHADOWS: return "SLSF1_Recieve_Shadows";
	case SkyrimShaderPropertyFlags1.SLSF1_CAST_SHADOWS: return "SLSF1_Cast_Shadows";
	case SkyrimShaderPropertyFlags1.SLSF1_FACEGEN_DETAIL_MAP: return "SLSF1_Facegen_Detail_Map";
	case SkyrimShaderPropertyFlags1.SLSF1_PARALLAX: return "SLSF1_Parallax";
	case SkyrimShaderPropertyFlags1.SLSF1_MODEL_SPACE_NORMALS: return "SLSF1_Model_Space_Normals";
	case SkyrimShaderPropertyFlags1.SLSF1_NON_PROJECTIVE_SHADOWS: return "SLSF1_Non_Projective_Shadows";
	case SkyrimShaderPropertyFlags1.SLSF1_LANDSCAPE: return "SLSF1_Landscape";
	case SkyrimShaderPropertyFlags1.SLSF1_REFRACTION: return "SLSF1_Refraction";
	case SkyrimShaderPropertyFlags1.SLSF1_FIRE_REFRACTION: return "SLSF1_Fire_Refraction";
	case SkyrimShaderPropertyFlags1.SLSF1_EYE_ENVIRONMENT_MAPPING: return "SLSF1_Eye_Environment_Mapping";
	case SkyrimShaderPropertyFlags1.SLSF1_HAIR_SOFT_LIGHTING: return "SLSF1_Hair_Soft_Lighting";
	case SkyrimShaderPropertyFlags1.SLSF1_SCREENDOOR_ALPHA_FADE: return "SLSF1_Screendoor_Alpha_Fade";
	case SkyrimShaderPropertyFlags1.SLSF1_LOCALMAP_HIDE_SECRET: return "SLSF1_Localmap_Hide_Secret";
	case SkyrimShaderPropertyFlags1.SLSF1_FACEGEN_RGB_TINT: return "SLSF1_FaceGen_RGB_Tint";
	case SkyrimShaderPropertyFlags1.SLSF1_OWN_EMIT: return "SLSF1_Own_Emit";
	case SkyrimShaderPropertyFlags1.SLSF1_PROJECTED_UV: return "SLSF1_Projected_UV";
	case SkyrimShaderPropertyFlags1.SLSF1_MULTIPLE_TEXTURES: return "SLSF1_Multiple_Textures";
	case SkyrimShaderPropertyFlags1.SLSF1_REMAPPABLE_TEXTURES: return "SLSF1_Remappable_Textures";
	case SkyrimShaderPropertyFlags1.SLSF1_DECAL: return "SLSF1_Decal";
	case SkyrimShaderPropertyFlags1.SLSF1_DYNAMIC_DECAL: return "SLSF1_Dynamic_Decal";
	case SkyrimShaderPropertyFlags1.SLSF1_PARALLAX_OCCLUSION: return "SLSF1_Parallax_Occlusion";
	case SkyrimShaderPropertyFlags1.SLSF1_EXTERNAL_EMITTANCE: return "SLSF1_External_Emittance";
	case SkyrimShaderPropertyFlags1.SLSF1_SOFT_EFFECT: return "SLSF1_Soft_Effect";
	case SkyrimShaderPropertyFlags1.SLSF1_ZBUFFER_TEST: return "SLSF1_ZBuffer_Test";
	default: return $"Invalid Value! - {val}";
}}}

/*! Skyrim Shader Property Flags 2 */
public enum SkyrimShaderPropertyFlags2 : uint {
	SLSF2_ZBUFFER_WRITE = 1, /*!< Enables writing to the Z-Buffer */
	SLSF2_LOD_LANDSCAPE = 2, /*!< SLSF2_LOD_Landscape */
	SLSF2_LOD_OBJECTS = 4, /*!< SLSF2_LOD_Objects */
	SLSF2_NO_FADE = 8, /*!< SLSF2_No_Fade */
	SLSF2_DOUBLE_SIDED = 16, /*!< Double-sided rendering. */
	SLSF2_VERTEX_COLORS = 32, /*!< Has Vertex Colors. */
	SLSF2_GLOW_MAP = 64, /*!< Use Glow Map in the third texture slot. */
	SLSF2_ASSUME_SHADOWMASK = 128, /*!< SLSF2_Assume_Shadowmask */
	SLSF2_PACKED_TANGENT = 256, /*!< SLSF2_Packed_Tangent */
	SLSF2_MULTI_INDEX_SNOW = 512, /*!< SLSF2_Multi_Index_Snow */
	SLSF2_VERTEX_LIGHTING = 1024, /*!< SLSF2_Vertex_Lighting */
	SLSF2_UNIFORM_SCALE = 2048, /*!< SLSF2_Uniform_Scale */
	SLSF2_FIT_SLOPE = 4096, /*!< SLSF2_Fit_Slope */
	SLSF2_BILLBOARD = 8192, /*!< SLSF2_Billboard */
	SLSF2_NO_LOD_LAND_BLEND = 16384, /*!< SLSF2_No_LOD_Land_Blend */
	SLSF2_ENVMAP_LIGHT_FADE = 32768, /*!< SLSF2_EnvMap_Light_Fade */
	SLSF2_WIREFRAME = 65536, /*!< Wireframe (Seems to only work on particles) */
	SLSF2_WEAPON_BLOOD = 131072, /*!< Used for blood decals on weapons. */
	SLSF2_HIDE_ON_LOCAL_MAP = 262144, /*!< Similar to hide secret, but only for self? */
	SLSF2_PREMULT_ALPHA = 524288, /*!< Has Premultiplied Alpha */
	SLSF2_CLOUD_LOD = 1048576, /*!< SLSF2_Cloud_LOD */
	SLSF2_ANISOTROPIC_LIGHTING = 2097152, /*!< Hair only? */
	SLSF2_NO_TRANSPARENCY_MULTISAMPLING = 4194304, /*!< SLSF2_No_Transparency_Multisampling */
	SLSF2_UNUSED01 = 8388608, /*!< Unused? */
	SLSF2_MULTI_LAYER_PARALLAX = 16777216, /*!< Use Multilayer (inner-layer) Map */
	SLSF2_SOFT_LIGHTING = 33554432, /*!< Use Soft Lighting Map */
	SLSF2_RIM_LIGHTING = 67108864, /*!< Use Rim Lighting Map */
	SLSF2_BACK_LIGHTING = 134217728, /*!< Use Back Lighting Map */
	SLSF2_UNUSED02 = 268435456, /*!< Unused? */
	SLSF2_TREE_ANIM = 536870912, /*!< Enables Vertex Animation, Flutter Animation */
	SLSF2_EFFECT_LIGHTING = 1073741824, /*!< SLSF2_Effect_Lighting */
	SLSF2_HD_LOD_OBJECTS = 2147483648, /*!< SLSF2_HD_LOD_Objects */
}
static partial class Nif { //--SkyrimShaderPropertyFlags2--//
public static void NifStream(out SkyrimShaderPropertyFlags2 val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (SkyrimShaderPropertyFlags2)temp; }
public static void NifStream(SkyrimShaderPropertyFlags2 val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(SkyrimShaderPropertyFlags2 val) { switch (val) {
	case SkyrimShaderPropertyFlags2.SLSF2_ZBUFFER_WRITE: return "SLSF2_ZBuffer_Write";
	case SkyrimShaderPropertyFlags2.SLSF2_LOD_LANDSCAPE: return "SLSF2_LOD_Landscape";
	case SkyrimShaderPropertyFlags2.SLSF2_LOD_OBJECTS: return "SLSF2_LOD_Objects";
	case SkyrimShaderPropertyFlags2.SLSF2_NO_FADE: return "SLSF2_No_Fade";
	case SkyrimShaderPropertyFlags2.SLSF2_DOUBLE_SIDED: return "SLSF2_Double_Sided";
	case SkyrimShaderPropertyFlags2.SLSF2_VERTEX_COLORS: return "SLSF2_Vertex_Colors";
	case SkyrimShaderPropertyFlags2.SLSF2_GLOW_MAP: return "SLSF2_Glow_Map";
	case SkyrimShaderPropertyFlags2.SLSF2_ASSUME_SHADOWMASK: return "SLSF2_Assume_Shadowmask";
	case SkyrimShaderPropertyFlags2.SLSF2_PACKED_TANGENT: return "SLSF2_Packed_Tangent";
	case SkyrimShaderPropertyFlags2.SLSF2_MULTI_INDEX_SNOW: return "SLSF2_Multi_Index_Snow";
	case SkyrimShaderPropertyFlags2.SLSF2_VERTEX_LIGHTING: return "SLSF2_Vertex_Lighting";
	case SkyrimShaderPropertyFlags2.SLSF2_UNIFORM_SCALE: return "SLSF2_Uniform_Scale";
	case SkyrimShaderPropertyFlags2.SLSF2_FIT_SLOPE: return "SLSF2_Fit_Slope";
	case SkyrimShaderPropertyFlags2.SLSF2_BILLBOARD: return "SLSF2_Billboard";
	case SkyrimShaderPropertyFlags2.SLSF2_NO_LOD_LAND_BLEND: return "SLSF2_No_LOD_Land_Blend";
	case SkyrimShaderPropertyFlags2.SLSF2_ENVMAP_LIGHT_FADE: return "SLSF2_EnvMap_Light_Fade";
	case SkyrimShaderPropertyFlags2.SLSF2_WIREFRAME: return "SLSF2_Wireframe";
	case SkyrimShaderPropertyFlags2.SLSF2_WEAPON_BLOOD: return "SLSF2_Weapon_Blood";
	case SkyrimShaderPropertyFlags2.SLSF2_HIDE_ON_LOCAL_MAP: return "SLSF2_Hide_On_Local_Map";
	case SkyrimShaderPropertyFlags2.SLSF2_PREMULT_ALPHA: return "SLSF2_Premult_Alpha";
	case SkyrimShaderPropertyFlags2.SLSF2_CLOUD_LOD: return "SLSF2_Cloud_LOD";
	case SkyrimShaderPropertyFlags2.SLSF2_ANISOTROPIC_LIGHTING: return "SLSF2_Anisotropic_Lighting";
	case SkyrimShaderPropertyFlags2.SLSF2_NO_TRANSPARENCY_MULTISAMPLING: return "SLSF2_No_Transparency_Multisampling";
	case SkyrimShaderPropertyFlags2.SLSF2_UNUSED01: return "SLSF2_Unused01";
	case SkyrimShaderPropertyFlags2.SLSF2_MULTI_LAYER_PARALLAX: return "SLSF2_Multi_Layer_Parallax";
	case SkyrimShaderPropertyFlags2.SLSF2_SOFT_LIGHTING: return "SLSF2_Soft_Lighting";
	case SkyrimShaderPropertyFlags2.SLSF2_RIM_LIGHTING: return "SLSF2_Rim_Lighting";
	case SkyrimShaderPropertyFlags2.SLSF2_BACK_LIGHTING: return "SLSF2_Back_Lighting";
	case SkyrimShaderPropertyFlags2.SLSF2_UNUSED02: return "SLSF2_Unused02";
	case SkyrimShaderPropertyFlags2.SLSF2_TREE_ANIM: return "SLSF2_Tree_Anim";
	case SkyrimShaderPropertyFlags2.SLSF2_EFFECT_LIGHTING: return "SLSF2_Effect_Lighting";
	case SkyrimShaderPropertyFlags2.SLSF2_HD_LOD_OBJECTS: return "SLSF2_HD_LOD_Objects";
	default: return $"Invalid Value! - {val}";
}}}

/*! Fallout 4 Shader Property Flags 1 */
public enum Fallout4ShaderPropertyFlags1 : uint {
	F4SF1_SPECULAR = 1, /*!< F4SF1_Specular */
	F4SF1_SKINNED = 2, /*!< F4SF1_Skinned */
	F4SF1_TEMP_REFRACTION = 4, /*!< F4SF1_Temp_Refraction */
	F4SF1_VERTEX_ALPHA = 8, /*!< F4SF1_Vertex_Alpha */
	F4SF1_GREYSCALETOPALETTE_COLOR = 16, /*!< F4SF1_GreyscaleToPalette_Color */
	F4SF1_GREYSCALETOPALETTE_ALPHA = 32, /*!< F4SF1_GreyscaleToPalette_Alpha */
	F4SF1_USE_FALLOFF = 64, /*!< F4SF1_Use_Falloff */
	F4SF1_ENVIRONMENT_MAPPING = 128, /*!< F4SF1_Environment_Mapping */
	F4SF1_RGB_FALLOFF = 256, /*!< F4SF1_RGB_Falloff */
	F4SF1_CAST_SHADOWS = 512, /*!< F4SF1_Cast_Shadows */
	F4SF1_FACE = 1024, /*!< F4SF1_Face */
	F4SF1_UI_MASK_RECTS = 2048, /*!< F4SF1_UI_Mask_Rects */
	F4SF1_MODEL_SPACE_NORMALS = 4096, /*!< F4SF1_Model_Space_Normals */
	F4SF1_NON_PROJECTIVE_SHADOWS = 8192, /*!< F4SF1_Non_Projective_Shadows */
	F4SF1_LANDSCAPE = 16384, /*!< F4SF1_Landscape */
	F4SF1_REFRACTION = 32768, /*!< F4SF1_Refraction */
	F4SF1_FIRE_REFRACTION = 65536, /*!< F4SF1_Fire_Refraction */
	F4SF1_EYE_ENVIRONMENT_MAPPING = 131072, /*!< F4SF1_Eye_Environment_Mapping */
	F4SF1_HAIR = 262144, /*!< F4SF1_Hair */
	F4SF1_SCREENDOOR_ALPHA_FADE = 524288, /*!< F4SF1_Screendoor_Alpha_Fade */
	F4SF1_LOCALMAP_HIDE_SECRET = 1048576, /*!< F4SF1_Localmap_Hide_Secret */
	F4SF1_SKIN_TINT = 2097152, /*!< F4SF1_Skin_Tint */
	F4SF1_OWN_EMIT = 4194304, /*!< F4SF1_Own_Emit */
	F4SF1_PROJECTED_UV = 8388608, /*!< F4SF1_Projected_UV */
	F4SF1_MULTIPLE_TEXTURES = 16777216, /*!< F4SF1_Multiple_Textures */
	F4SF1_TESSELLATE = 33554432, /*!< F4SF1_Tessellate */
	F4SF1_DECAL = 67108864, /*!< F4SF1_Decal */
	F4SF1_DYNAMIC_DECAL = 134217728, /*!< F4SF1_Dynamic_Decal */
	F4SF1_CHARACTER_LIGHTING = 268435456, /*!< F4SF1_Character_Lighting */
	F4SF1_EXTERNAL_EMITTANCE = 536870912, /*!< F4SF1_External_Emittance */
	F4SF1_SOFT_EFFECT = 1073741824, /*!< F4SF1_Soft_Effect */
	F4SF1_ZBUFFER_TEST = 2147483648, /*!< F4SF1_ZBuffer_Test */
}
static partial class Nif { //--Fallout4ShaderPropertyFlags1--//
public static void NifStream(out Fallout4ShaderPropertyFlags1 val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (Fallout4ShaderPropertyFlags1)temp; }
public static void NifStream(Fallout4ShaderPropertyFlags1 val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(Fallout4ShaderPropertyFlags1 val) { switch (val) {
	case Fallout4ShaderPropertyFlags1.F4SF1_SPECULAR: return "F4SF1_Specular";
	case Fallout4ShaderPropertyFlags1.F4SF1_SKINNED: return "F4SF1_Skinned";
	case Fallout4ShaderPropertyFlags1.F4SF1_TEMP_REFRACTION: return "F4SF1_Temp_Refraction";
	case Fallout4ShaderPropertyFlags1.F4SF1_VERTEX_ALPHA: return "F4SF1_Vertex_Alpha";
	case Fallout4ShaderPropertyFlags1.F4SF1_GREYSCALETOPALETTE_COLOR: return "F4SF1_GreyscaleToPalette_Color";
	case Fallout4ShaderPropertyFlags1.F4SF1_GREYSCALETOPALETTE_ALPHA: return "F4SF1_GreyscaleToPalette_Alpha";
	case Fallout4ShaderPropertyFlags1.F4SF1_USE_FALLOFF: return "F4SF1_Use_Falloff";
	case Fallout4ShaderPropertyFlags1.F4SF1_ENVIRONMENT_MAPPING: return "F4SF1_Environment_Mapping";
	case Fallout4ShaderPropertyFlags1.F4SF1_RGB_FALLOFF: return "F4SF1_RGB_Falloff";
	case Fallout4ShaderPropertyFlags1.F4SF1_CAST_SHADOWS: return "F4SF1_Cast_Shadows";
	case Fallout4ShaderPropertyFlags1.F4SF1_FACE: return "F4SF1_Face";
	case Fallout4ShaderPropertyFlags1.F4SF1_UI_MASK_RECTS: return "F4SF1_UI_Mask_Rects";
	case Fallout4ShaderPropertyFlags1.F4SF1_MODEL_SPACE_NORMALS: return "F4SF1_Model_Space_Normals";
	case Fallout4ShaderPropertyFlags1.F4SF1_NON_PROJECTIVE_SHADOWS: return "F4SF1_Non_Projective_Shadows";
	case Fallout4ShaderPropertyFlags1.F4SF1_LANDSCAPE: return "F4SF1_Landscape";
	case Fallout4ShaderPropertyFlags1.F4SF1_REFRACTION: return "F4SF1_Refraction";
	case Fallout4ShaderPropertyFlags1.F4SF1_FIRE_REFRACTION: return "F4SF1_Fire_Refraction";
	case Fallout4ShaderPropertyFlags1.F4SF1_EYE_ENVIRONMENT_MAPPING: return "F4SF1_Eye_Environment_Mapping";
	case Fallout4ShaderPropertyFlags1.F4SF1_HAIR: return "F4SF1_Hair";
	case Fallout4ShaderPropertyFlags1.F4SF1_SCREENDOOR_ALPHA_FADE: return "F4SF1_Screendoor_Alpha_Fade";
	case Fallout4ShaderPropertyFlags1.F4SF1_LOCALMAP_HIDE_SECRET: return "F4SF1_Localmap_Hide_Secret";
	case Fallout4ShaderPropertyFlags1.F4SF1_SKIN_TINT: return "F4SF1_Skin_Tint";
	case Fallout4ShaderPropertyFlags1.F4SF1_OWN_EMIT: return "F4SF1_Own_Emit";
	case Fallout4ShaderPropertyFlags1.F4SF1_PROJECTED_UV: return "F4SF1_Projected_UV";
	case Fallout4ShaderPropertyFlags1.F4SF1_MULTIPLE_TEXTURES: return "F4SF1_Multiple_Textures";
	case Fallout4ShaderPropertyFlags1.F4SF1_TESSELLATE: return "F4SF1_Tessellate";
	case Fallout4ShaderPropertyFlags1.F4SF1_DECAL: return "F4SF1_Decal";
	case Fallout4ShaderPropertyFlags1.F4SF1_DYNAMIC_DECAL: return "F4SF1_Dynamic_Decal";
	case Fallout4ShaderPropertyFlags1.F4SF1_CHARACTER_LIGHTING: return "F4SF1_Character_Lighting";
	case Fallout4ShaderPropertyFlags1.F4SF1_EXTERNAL_EMITTANCE: return "F4SF1_External_Emittance";
	case Fallout4ShaderPropertyFlags1.F4SF1_SOFT_EFFECT: return "F4SF1_Soft_Effect";
	case Fallout4ShaderPropertyFlags1.F4SF1_ZBUFFER_TEST: return "F4SF1_ZBuffer_Test";
	default: return $"Invalid Value! - {val}";
}}}

/*! Fallout 4 Shader Property Flags 2 */
public enum Fallout4ShaderPropertyFlags2 : uint {
	F4SF2_ZBUFFER_WRITE = 1, /*!< F4SF2_ZBuffer_Write */
	F4SF2_LOD_LANDSCAPE = 2, /*!< F4SF2_LOD_Landscape */
	F4SF2_LOD_OBJECTS = 4, /*!< F4SF2_LOD_Objects */
	F4SF2_NO_FADE = 8, /*!< F4SF2_No_Fade */
	F4SF2_DOUBLE_SIDED = 16, /*!< F4SF2_Double_Sided */
	F4SF2_VERTEX_COLORS = 32, /*!< F4SF2_Vertex_Colors */
	F4SF2_GLOW_MAP = 64, /*!< F4SF2_Glow_Map */
	F4SF2_TRANSFORM_CHANGED = 128, /*!< F4SF2_Transform_Changed */
	F4SF2_DISMEMBERMENT_MEATCUFF = 256, /*!< F4SF2_Dismemberment_Meatcuff */
	F4SF2_TINT = 512, /*!< F4SF2_Tint */
	F4SF2_GRASS_VERTEX_LIGHTING = 1024, /*!< F4SF2_Grass_Vertex_Lighting */
	F4SF2_GRASS_UNIFORM_SCALE = 2048, /*!< F4SF2_Grass_Uniform_Scale */
	F4SF2_GRASS_FIT_SLOPE = 4096, /*!< F4SF2_Grass_Fit_Slope */
	F4SF2_GRASS_BILLBOARD = 8192, /*!< F4SF2_Grass_Billboard */
	F4SF2_NO_LOD_LAND_BLEND = 16384, /*!< F4SF2_No_LOD_Land_Blend */
	F4SF2_DISMEMBERMENT = 32768, /*!< F4SF2_Dismemberment */
	F4SF2_WIREFRAME = 65536, /*!< F4SF2_Wireframe */
	F4SF2_WEAPON_BLOOD = 131072, /*!< F4SF2_Weapon_Blood */
	F4SF2_HIDE_ON_LOCAL_MAP = 262144, /*!< F4SF2_Hide_On_Local_Map */
	F4SF2_PREMULT_ALPHA = 524288, /*!< F4SF2_Premult_Alpha */
	F4SF2_VATS_TARGET = 1048576, /*!< F4SF2_VATS_Target */
	F4SF2_ANISOTROPIC_LIGHTING = 2097152, /*!< F4SF2_Anisotropic_Lighting */
	F4SF2_SKEW_SPECULAR_ALPHA = 4194304, /*!< F4SF2_Skew_Specular_Alpha */
	F4SF2_MENU_SCREEN = 8388608, /*!< F4SF2_Menu_Screen */
	F4SF2_MULTI_LAYER_PARALLAX = 16777216, /*!< F4SF2_Multi_Layer_Parallax */
	F4SF2_ALPHA_TEST = 33554432, /*!< F4SF2_Alpha_Test */
	F4SF2_GRADIENT_REMAP = 67108864, /*!< F4SF2_Gradient_Remap */
	F4SF2_VATS_TARGET_DRAW_ALL = 134217728, /*!< F4SF2_VATS_Target_Draw_All */
	F4SF2_PIPBOY_SCREEN = 268435456, /*!< F4SF2_Pipboy_Screen */
	F4SF2_TREE_ANIM = 536870912, /*!< F4SF2_Tree_Anim */
	F4SF2_EFFECT_LIGHTING = 1073741824, /*!< F4SF2_Effect_Lighting */
	F4SF2_REFRACTION_WRITES_DEPTH = 2147483648, /*!< F4SF2_Refraction_Writes_Depth */
}
static partial class Nif { //--Fallout4ShaderPropertyFlags2--//
public static void NifStream(out Fallout4ShaderPropertyFlags2 val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (Fallout4ShaderPropertyFlags2)temp; }
public static void NifStream(Fallout4ShaderPropertyFlags2 val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(Fallout4ShaderPropertyFlags2 val) { switch (val) {
	case Fallout4ShaderPropertyFlags2.F4SF2_ZBUFFER_WRITE: return "F4SF2_ZBuffer_Write";
	case Fallout4ShaderPropertyFlags2.F4SF2_LOD_LANDSCAPE: return "F4SF2_LOD_Landscape";
	case Fallout4ShaderPropertyFlags2.F4SF2_LOD_OBJECTS: return "F4SF2_LOD_Objects";
	case Fallout4ShaderPropertyFlags2.F4SF2_NO_FADE: return "F4SF2_No_Fade";
	case Fallout4ShaderPropertyFlags2.F4SF2_DOUBLE_SIDED: return "F4SF2_Double_Sided";
	case Fallout4ShaderPropertyFlags2.F4SF2_VERTEX_COLORS: return "F4SF2_Vertex_Colors";
	case Fallout4ShaderPropertyFlags2.F4SF2_GLOW_MAP: return "F4SF2_Glow_Map";
	case Fallout4ShaderPropertyFlags2.F4SF2_TRANSFORM_CHANGED: return "F4SF2_Transform_Changed";
	case Fallout4ShaderPropertyFlags2.F4SF2_DISMEMBERMENT_MEATCUFF: return "F4SF2_Dismemberment_Meatcuff";
	case Fallout4ShaderPropertyFlags2.F4SF2_TINT: return "F4SF2_Tint";
	case Fallout4ShaderPropertyFlags2.F4SF2_GRASS_VERTEX_LIGHTING: return "F4SF2_Grass_Vertex_Lighting";
	case Fallout4ShaderPropertyFlags2.F4SF2_GRASS_UNIFORM_SCALE: return "F4SF2_Grass_Uniform_Scale";
	case Fallout4ShaderPropertyFlags2.F4SF2_GRASS_FIT_SLOPE: return "F4SF2_Grass_Fit_Slope";
	case Fallout4ShaderPropertyFlags2.F4SF2_GRASS_BILLBOARD: return "F4SF2_Grass_Billboard";
	case Fallout4ShaderPropertyFlags2.F4SF2_NO_LOD_LAND_BLEND: return "F4SF2_No_LOD_Land_Blend";
	case Fallout4ShaderPropertyFlags2.F4SF2_DISMEMBERMENT: return "F4SF2_Dismemberment";
	case Fallout4ShaderPropertyFlags2.F4SF2_WIREFRAME: return "F4SF2_Wireframe";
	case Fallout4ShaderPropertyFlags2.F4SF2_WEAPON_BLOOD: return "F4SF2_Weapon_Blood";
	case Fallout4ShaderPropertyFlags2.F4SF2_HIDE_ON_LOCAL_MAP: return "F4SF2_Hide_On_Local_Map";
	case Fallout4ShaderPropertyFlags2.F4SF2_PREMULT_ALPHA: return "F4SF2_Premult_Alpha";
	case Fallout4ShaderPropertyFlags2.F4SF2_VATS_TARGET: return "F4SF2_VATS_Target";
	case Fallout4ShaderPropertyFlags2.F4SF2_ANISOTROPIC_LIGHTING: return "F4SF2_Anisotropic_Lighting";
	case Fallout4ShaderPropertyFlags2.F4SF2_SKEW_SPECULAR_ALPHA: return "F4SF2_Skew_Specular_Alpha";
	case Fallout4ShaderPropertyFlags2.F4SF2_MENU_SCREEN: return "F4SF2_Menu_Screen";
	case Fallout4ShaderPropertyFlags2.F4SF2_MULTI_LAYER_PARALLAX: return "F4SF2_Multi_Layer_Parallax";
	case Fallout4ShaderPropertyFlags2.F4SF2_ALPHA_TEST: return "F4SF2_Alpha_Test";
	case Fallout4ShaderPropertyFlags2.F4SF2_GRADIENT_REMAP: return "F4SF2_Gradient_Remap";
	case Fallout4ShaderPropertyFlags2.F4SF2_VATS_TARGET_DRAW_ALL: return "F4SF2_VATS_Target_Draw_All";
	case Fallout4ShaderPropertyFlags2.F4SF2_PIPBOY_SCREEN: return "F4SF2_Pipboy_Screen";
	case Fallout4ShaderPropertyFlags2.F4SF2_TREE_ANIM: return "F4SF2_Tree_Anim";
	case Fallout4ShaderPropertyFlags2.F4SF2_EFFECT_LIGHTING: return "F4SF2_Effect_Lighting";
	case Fallout4ShaderPropertyFlags2.F4SF2_REFRACTION_WRITES_DEPTH: return "F4SF2_Refraction_Writes_Depth";
	default: return $"Invalid Value! - {val}";
}}}

/*! Skyrim water shader property flags */
public enum SkyrimWaterShaderFlags : uint {
	SWSF1_UNKNOWN0 = 1, /*!< Unknown */
	SWSF1_BYPASS_REFRACTION_MAP = 2, /*!< Bypasses refraction map when set to 1 */
	SWSF1_WATER_TOGGLE = 4, /*!< Main water Layer on/off */
	SWSF1_UNKNOWN3 = 8, /*!< Unknown */
	SWSF1_UNKNOWN4 = 16, /*!< Unknown */
	SWSF1_UNKNOWN5 = 32, /*!< Unknown */
	SWSF1_HIGHLIGHT_LAYER_TOGGLE = 64, /*!< Reflection layer 2 on/off. (is this scene reflection?) */
	SWSF1_ENABLED = 128, /*!< Water layer on/off */
}
static partial class Nif { //--SkyrimWaterShaderFlags--//
public static void NifStream(out SkyrimWaterShaderFlags val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (SkyrimWaterShaderFlags)temp; }
public static void NifStream(SkyrimWaterShaderFlags val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(SkyrimWaterShaderFlags val) { switch (val) {
	case SkyrimWaterShaderFlags.SWSF1_UNKNOWN0: return "SWSF1_UNKNOWN0";
	case SkyrimWaterShaderFlags.SWSF1_BYPASS_REFRACTION_MAP: return "SWSF1_Bypass_Refraction_Map";
	case SkyrimWaterShaderFlags.SWSF1_WATER_TOGGLE: return "SWSF1_Water_Toggle";
	case SkyrimWaterShaderFlags.SWSF1_UNKNOWN3: return "SWSF1_UNKNOWN3";
	case SkyrimWaterShaderFlags.SWSF1_UNKNOWN4: return "SWSF1_UNKNOWN4";
	case SkyrimWaterShaderFlags.SWSF1_UNKNOWN5: return "SWSF1_UNKNOWN5";
	case SkyrimWaterShaderFlags.SWSF1_HIGHLIGHT_LAYER_TOGGLE: return "SWSF1_Highlight_Layer_Toggle";
	case SkyrimWaterShaderFlags.SWSF1_ENABLED: return "SWSF1_Enabled";
	default: return $"Invalid Value! - {val}";
}}}

/*! Flags for BSValueNode. */
public enum BSValueNodeFlags : uint {
	BILLBOARDWORLDZ = 1, /*!< BillboardWorldZ */
	USEPLAYERADJUST = 2, /*!< UsePlayerAdjust */
}
static partial class Nif { //--BSValueNodeFlags--//
public static void NifStream(out BSValueNodeFlags val, IStream s, NifInfo info) { byte temp; NifStream(out temp, s, info); val = (BSValueNodeFlags)temp; }
public static void NifStream(BSValueNodeFlags val, OStream s, NifInfo info) => NifStream((byte)val, s, info);
public static string AsString(BSValueNodeFlags val) { switch (val) {
	case BSValueNodeFlags.BILLBOARDWORLDZ: return "BillboardWorldZ";
	case BSValueNodeFlags.USEPLAYERADJUST: return "UsePlayerAdjust";
	default: return $"Invalid Value! - {val}";
}}}

/*! Determines how the data stream is accessed? */
public enum DataStreamAccess : uint {
	CPU_READ = 1, /*!< CPU Read */
	CPU_WRITE_STATIC = 2, /*!< CPU Write Static */
	CPU_WRITE_MUTABLE = 4, /*!< CPU Write Mutable */
	CPU_WRITE_VOLATILE = 8, /*!< CPU Write Volatile */
	GPU_READ = 16, /*!< GPU Read */
	GPU_WRITE = 32, /*!< GPU Write */
	CPU_WRITE_STATIC_INITITIALIZED = 64, /*!< CPU Write Static Inititialized */
}
static partial class Nif { //--DataStreamAccess--//
public static void NifStream(out DataStreamAccess val, IStream s, NifInfo info) { uint temp; NifStream(out temp, s, info); val = (DataStreamAccess)temp; }
public static void NifStream(DataStreamAccess val, OStream s, NifInfo info) => NifStream((uint)val, s, info);
public static string AsString(DataStreamAccess val) { switch (val) {
	case DataStreamAccess.CPU_READ: return "CPU Read";
	case DataStreamAccess.CPU_WRITE_STATIC: return "CPU Write Static";
	case DataStreamAccess.CPU_WRITE_MUTABLE: return "CPU Write Mutable";
	case DataStreamAccess.CPU_WRITE_VOLATILE: return "CPU Write Volatile";
	case DataStreamAccess.GPU_READ: return "GPU Read";
	case DataStreamAccess.GPU_WRITE: return "GPU Write";
	case DataStreamAccess.CPU_WRITE_STATIC_INITITIALIZED: return "CPU Write Static Inititialized";
	default: return $"Invalid Value! - {val}";
}}}

}
