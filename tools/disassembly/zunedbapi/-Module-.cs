using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using _003CCppImplementationDetails_003E;
using _003CCrtImplementationDetails_003E;
using DataStructs;
using DeviceAccess;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Messaging;
using Microsoft.Zune.Playlist;
using Microsoft.Zune.QuickMix;
using Microsoft.Zune.Service;
using Microsoft.Zune.Subscription;
using Microsoft.Zune.UserCredential;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using MicrosoftZunePlayback;
using ZuneUI;

internal class _003CModule_003E
{
	internal static _0024ArrayType_0024_0024_0024BY05_0024_0024CBG _003F_003F_C_0040_1M_0040LBHBGPGB_0040_003F_0024AAI_003F_0024AAs_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AA_003F_0024AA_0040/* Not supported: data(49 00 73 00 44 00 52 00 4D 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0M_0040_0024_0024CBG _003F_003F_C_0040_1BI_0040GNMDHAGM_0040_003F_0024AAI_003F_0024AAs_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAC_003F_0024AAa_003F_0024AAc_003F_0024AAh_003F_0024AAe_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(49 00 73 00 44 00 52 00 4D 00 43 00 61 00 63 00 68 00 65 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040DAAGAPMC_0040_003F_0024AAB_003F_0024AAa_003F_0024AAs_003F_0024AAe_003F_0024AAL_003F_0024AAA_003F_0024AAU_003F_0024AAR_003F_0024AAL_003F_0024AA_003F_0024AA_0040/* Not supported: data(42 00 61 00 73 00 65 00 4C 00 41 00 55 00 52 00 4C 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY06_0024_0024CBG _003F_003F_C_0040_1O_0040IENGDLDD_0040_003F_0024AAR_003F_0024AAi_003F_0024AAg_003F_0024AAh_003F_0024AAt_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(52 00 69 00 67 00 68 00 74 00 73 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY03_0024_0024CBG _003F_003F_C_0040_17NIMOGDPK_0040_003F_0024AAL_003F_0024AAI_003F_0024AAD_003F_0024AA_003F_0024AA_0040/* Not supported: data(4C 00 49 00 44 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0L_0040_0024_0024CBG _003F_003F_C_0040_1BG_0040DHALOKGI_0040_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAH_003F_0024AAe_003F_0024AAa_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AA_003F4_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 52 00 4D 00 48 00 65 00 61 00 64 00 65 00 72 00 2E 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0O_0040_0024_0024CBG _003F_003F_C_0040_1BM_0040DIDAGMCI_0040_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAH_003F_0024AAe_003F_0024AAa_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AA_003F4_003F_0024AAK_003F_0024AAI_003F_0024AAD_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 52 00 4D 00 48 00 65 00 61 00 64 00 65 00 72 00 2E 00 4B 00 49 00 44 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BB_0040_0024_0024CBG _003F_003F_C_0040_1CC_0040NNDAHHNF_0040_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAH_003F_0024AAe_003F_0024AAa_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AA_003F4_003F_0024AAL_003F_0024AAA_003F_0024AAI_003F_0024AAN_003F_0024AAF_003F_0024AAO_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 52 00 4D 00 48 00 65 00 61 00 64 00 65 00 72 00 2E 00 4C 00 41 00 49 00 4E 00 46 00 4F 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0O_0040_0024_0024CBG _003F_003F_C_0040_1BM_0040OLNFHHJN_0040_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAH_003F_0024AAe_003F_0024AAa_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AA_003F4_003F_0024AAC_003F_0024AAI_003F_0024AAD_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 52 00 4D 00 48 00 65 00 61 00 64 00 65 00 72 00 2E 00 43 00 49 00 44 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BK_0040_0024_0024CBG _003F_003F_C_0040_1DE_0040BPOGJHAO_0040_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAH_003F_0024AAe_003F_0024AAa_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AA_003F4_003F_0024AAS_003F_0024AAE_003F_0024AAC_003F_0024AAU_003F_0024AAR_003F_0024AAI_003F_0024AAT_003F_0024AAY_003F_0024AAV_003F_0024AAE_003F_0024AAR_003F_0024AAS_003F_0024AAI_003F_0024AAO_003F_0024AAN_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 52 00 4D 00 48 00 65 00 61 00 64 00 65 00 72 00 2E 00 53 00 45 00 43 00 55 00 52 00 49 00 54 00 59 00 56 00 45 00 52 00 53 00 49 00 4F 00 4E 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BN_0040_0024_0024CBG _003F_003F_C_0040_1DK_0040CHAOFCDN_0040_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAH_003F_0024AAe_003F_0024AAa_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AA_003F4_003F_0024AAC_003F_0024AAo_003F_0024AAn_003F_0024AAt_003F_0024AAe_003F_0024AAn_003F_0024AAt_003F_0024AAD_003F_0024AAi_003F_0024AAs_003F_0024AAt_003F_0024AAr_003F_0024AAi_003F_0024AAb_003F_0024AAu_003F_0024AAt_003F_0024AAo_003F_0024AAr_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 52 00 4D 00 48 00 65 00 61 00 64 00 65 00 72 00 2E 00 43 00 6F 00 6E 00 74 00 65 00 6E 00 74 00 44 00 69 00 73 00 74 00 72 00 69 00 62 00 75 00 74 00 6F 00 72 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0CA_0040_0024_0024CBG _003F_003F_C_0040_1EA_0040MLKHFGAG_0040_003F_0024AAD_003F_0024AAR_003F_0024AAM_003F_0024AAH_003F_0024AAe_003F_0024AAa_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AA_003F4_003F_0024AAS_003F_0024AAu_003F_0024AAb_003F_0024AAs_003F_0024AAc_003F_0024AAr_003F_0024AAi_003F_0024AAp_003F_0024AAt_003F_0024AAi_003F_0024AAo_003F_0024AAn_003F_0024AAC_003F_0024AAo_003F_0024AAn_003F_0024AAt_003F_0024AAe_003F_0024AAn_003F_0024AAt_003F_0024AAI_003F_0024AAD_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 52 00 4D 00 48 00 65 00 61 00 64 00 65 00 72 00 2E 00 53 00 75 00 62 00 73 00 63 00 72 00 69 00 70 00 74 00 69 00 6F 00 6E 00 43 00 6F 00 6E 00 74 00 65 00 6E 00 74 00 49 00 44 00 00 00) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_IsDRM/* Not supported: data(68 15 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_DRMHeader/* Not supported: data(0C 15 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_BaseLicenseAcqURL/* Not supported: data(3C 15 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_Rights/* Not supported: data(2C 15 15 65) */;

	internal static int ___0040_0040_PchSym__004000_0040UgvnkUEHBBDIUlyqOcIGuivUafmvUxorvmgUxlnklmvmghUnzmztvwUafmvwyzkrUlyquivUrDIGUkxsOlyq_0040ZuneDBApi/* Not supported: data(00 00 00 00) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_IsDRMCached/* Not supported: data(50 15 15 65) */;

	internal static _GUID ID_MS_MEDIA_SCHEMA_PLAYLIST/* Not supported: data(64 14 4F 1F 65 C9 F5 4C 95 CB A1 33 7A 2A C9 F8) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_DRMHeader_LicenseAcqURL/* Not supported: data(CC 14 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_DRMHeader_ContentID/* Not supported: data(B0 14 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_DRMHeader_ContentDistributor/* Not supported: data(40 14 15 65) */;

	internal static _GUID ID_MS_MEDIA_SCHEMA_SERIES/* Not supported: data(C5 3D 56 8A 6B E2 78 48 B3 CF D6 95 55 61 F7 8C) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_DRMHeader_SubscriptionContentID/* Not supported: data(00 14 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_DRMHeader_KeyID/* Not supported: data(F0 14 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_DRMHeader_IndividualizedVersion/* Not supported: data(7C 14 15 65) */;

	internal unsafe static ushort* _003FA0xfea64c83_002Eg_wszWMDRM_LicenseID/* Not supported: data(24 15 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2GetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(2C 9D 1F 65 A8 9C 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IGetAccountCallback_0040_00408/* Not supported: data(A8 9C 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2AppMetadata_0040_00408/* Not supported: data(70 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2VideoMetadata_0040_00408/* Not supported: data(28 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2MusicAlbumMetadata_0040_00408/* Not supported: data(E0 9B 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2MusicTrackMetadata_0040_00408/* Not supported: data(98 9B 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2IUnknown_0040_00408/* Not supported: data(C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040GetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(7C 01 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 0C 9D 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IGetAccountCallback_0040_00408/* Not supported: data(28 01 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 8C 9C 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040AppMetadata_0040_00408/* Not supported: data(D4 00 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 58 9C 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040VideoMetadata_0040_00408/* Not supported: data(A8 00 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 10 9C 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040MusicAlbumMetadata_0040_00408/* Not supported: data(74 00 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 C8 9B 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040MusicTrackMetadata_0040_00408/* Not supported: data(40 00 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 80 9B 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IUnknown_0040_00408/* Not supported: data(4C 01 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E0 9C 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3GetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 1C 9D 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_55 _003F_003F_R0_003FAVGetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 47 65 74 41 63 63 6F 75 6E 74 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IGetAccountCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 9C 9C 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_26 _003F_003F_R0_003FAUIGetAccountCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 47 65 74 41 63 63 6F 75 6E 74 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3AppMetadata_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 68 9C 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_18 _003F_003F_R0_003FAUAppMetadata_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 41 70 70 4D 65 74 61 64 61 74 61 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3VideoMetadata_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 20 9C 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_20 _003F_003F_R0_003FAUVideoMetadata_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 56 69 64 65 6F 4D 65 74 61 64 61 74 61 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3MusicAlbumMetadata_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 D8 9B 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_25 _003F_003F_R0_003FAUMusicAlbumMetadata_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 4D 75 73 69 63 41 6C 62 75 6D 4D 65 74 61 64 61 74 61 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3MusicTrackMetadata_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 90 9B 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_25 _003F_003F_R0_003FAUMusicTrackMetadata_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 4D 75 73 69 63 54 72 61 63 6B 4D 65 74 61 64 61 74 61 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IUnknown_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 F0 9C 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_15 _003F_003F_R0_003FAUIUnknown_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 55 6E 6B 6E 6F 77 6E 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4GetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 7C 01 25 65 0C 9D 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4AppMetadata_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 D4 00 25 65 58 9C 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4VideoMetadata_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 A8 00 25 65 10 9C 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4MusicAlbumMetadata_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 74 00 25 65 C8 9B 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4MusicTrackMetadata_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 40 00 25 65 80 9B 1F 65) */;

	internal static __s_GUID _GUID_a2506604_f033_4182_8bbe_a2bc722c568e/* Not supported: data(04 66 50 A2 33 F0 82 41 8B BE A2 BC 72 2C 56 8E) */;

	internal static __s_GUID _GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911/* Not supported: data(DD 1E 2D BB D5 1B E1 4B 8D 38 36 D4 F0 84 99 11) */;

	internal static __s_GUID _GUID_223a83b5_e8ac_4aad_882a_14ee6634fc33/* Not supported: data(B5 83 3A 22 AC E8 AD 4A 88 2A 14 EE 66 34 FC 33) */;

	internal static __s_GUID _GUID_00000000_0000_0000_c000_000000000046/* Not supported: data(00 00 00 00 00 00 00 00 C0 00 00 00 00 00 00 46) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7GetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(7A DB 1F 65 85 DB 1F 65 4A EF 1F 65 CD EC 1F 65 0E ED 1F 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY03Q6GXXZ _003F_003F_7AppMetadata_0040_00406B_0040/* Not supported: data(F6 EB 1F 65 2C DA 1F 65 00 E9 1F 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY03Q6GXXZ _003F_003F_7VideoMetadata_0040_00406B_0040/* Not supported: data(FB E7 1F 65 BD D9 1F 65 DA E4 1F 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY03Q6GXXZ _003F_003F_7MusicAlbumMetadata_0040_00406B_0040/* Not supported: data(A5 E3 1F 65 2C D9 1F 65 D5 E1 1F 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY03Q6GXXZ _003F_003F_7MusicTrackMetadata_0040_00406B_0040/* Not supported: data(23 E1 1F 65 7A D8 1F 65 E5 DE 1F 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_dfb90302_e898_40f7_ab37_e5ba31902d09/* Not supported: data(02 03 B9 DF 98 E8 F7 40 AB 37 E5 BA 31 90 2D 09) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2CallbackOnUIThreadBimodalUnmanaged_DONOTUSE_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(E4 9D 1F 65 00 9E 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2AsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(50 9E 1F 65 98 9D 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2IUIThreadCallbackWorker_0040_00408/* Not supported: data(60 9D 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IAsyncCallback_0040_00408/* Not supported: data(98 9D 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EN_0040IUIThreadCallbackWorker_0040_00408/* Not supported: data(EC 01 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 4D 00 00 00 48 9D 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CallbackOnUIThreadBimodalUnmanaged_DONOTUSE_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(40 02 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 C8 9D 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040AsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(A8 02 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 30 9E 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IUIThreadCallbackWorker_0040_00408/* Not supported: data(EC 01 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 48 9D 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IAsyncCallback_0040_00408/* Not supported: data(14 02 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 7C 9D 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CallbackOnUIThreadBimodalUnmanaged_DONOTUSE_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 D8 9D 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_71 _003F_003F_R0_003FAVCallbackOnUIThreadBimodalUnmanaged_DONOTUSE_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 61 6C 6C 62 61 63 6B 4F 6E 55 49 54 68 72 65 61 64 42 69 6D 6F 64 61 6C 55 6E 6D 61 6E 61 67 65 64 5F 44 4F 4E 4F 54 55 53 45 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3AsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 40 9E 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_47 _003F_003F_R0_003FAVAsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 41 73 79 6E 63 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IUIThreadCallbackWorker_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 58 9D 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_30 _003F_003F_R0_003FAUIUIThreadCallbackWorker_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 55 49 54 68 72 65 61 64 43 61 6C 6C 62 61 63 6B 57 6F 72 6B 65 72 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IAsyncCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 8C 9D 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_21 _003F_003F_R0_003FAUIAsyncCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 41 73 79 6E 63 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CallbackOnUIThreadBimodalUnmanaged_DONOTUSE_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 40 02 25 65 C8 9D 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4AsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 A8 02 25 65 30 9E 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY01Q6GXXZ _003F_003F_7CallbackOnUIThreadBimodalUnmanaged_DONOTUSE_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(89 14 20 65 00 00 00 00) */;

	internal static __s_GUID _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d/* Not supported: data(66 FD FC F5 9A 9E 6A 43 8B 10 AE B4 D6 CE 2B 3D) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7AsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(7D 15 20 65 88 15 20 65 15 16 20 65 CD 15 20 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CDDeviceCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(A0 9E 1F 65 BC 9E 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CBurnPublisherCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(28 9F 1F 65 44 9F 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IBurnPublisherCallback_0040_00408/* Not supported: data(44 9F 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IZuneCDDeviceCallback_0040_00408/* Not supported: data(BC 9E 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CDDeviceCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(0C 03 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 80 9E 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CBurnPublisherCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(90 03 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 08 9F 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IBurnPublisherCallback_0040_00408/* Not supported: data(CC 03 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 60 9F 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IZuneCDDeviceCallback_0040_00408/* Not supported: data(40 03 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 D8 9E 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CDDeviceCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 90 9E 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_44 _003F_003F_R0_003FAVCDDeviceCallback_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 44 44 65 76 69 63 65 43 61 6C 6C 62 61 63 6B 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CBurnPublisherCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 18 9F 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_50 _003F_003F_R0_003FAVCBurnPublisherCallback_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 42 75 72 6E 50 75 62 6C 69 73 68 65 72 43 61 6C 6C 62 61 63 6B 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IBurnPublisherCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 70 9F 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_29 _003F_003F_R0_003FAUIBurnPublisherCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 42 75 72 6E 50 75 62 6C 69 73 68 65 72 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IZuneCDDeviceCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 E8 9E 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAUIZuneCDDeviceCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 5A 75 6E 65 43 44 44 65 76 69 63 65 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CDDeviceCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 0C 03 25 65 80 9E 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CBurnPublisherCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 90 03 25 65 08 9F 1F 65) */;

	internal static __s_GUID _GUID_63c780f9_0f40_4e4a_8c9e_91f7a48d5946/* Not supported: data(F9 80 C7 63 40 0F 4A 4E 8C 9E 91 F7 A4 8D 59 46) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7CDDeviceCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(5D 1F 20 65 68 1F 20 65 56 27 20 65 1F 22 20 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_27354130_7f64_5b0f_8f00_5d77afbe261e/* Not supported: data(30 41 35 27 64 7F 0F 5B 8F 00 5D 77 AF BE 26 1E) */;

	internal static _0024ArrayType_0024_0024_0024BY09Q6GXXZ _003F_003F_7CBurnPublisherCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(B9 0F 20 65 C4 0F 20 65 4A 12 20 65 BD 10 20 65 F4 10 20 65 2C 11 20 65 63 11 20 65 A4 11 20 65 EA 11 20 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0xa7a1a344_002EWPP_Clipboard_cpp_Traceguids/* Not supported: data(D8 2F 0C 6B F9 2E 2D E5 91 CE A5 B6 E6 A3 7D B4) */;

	internal unsafe static WPP_PROJECT_CONTROL_BLOCK* WPP_GLOBAL_Control/* Not supported: data(F8 03 25 65) */;

	internal static _GUID WPP_ThisDir_CTLGUID_SqmClientTracingGuid/* Not supported: data(08 14 82 E2 9D C5 8F 41 AD 3F AA 4E 79 2A EB 79) */;

	internal static _GUID WPP_ThisDir_CTLGUID_CtlGuidZuneWmdu/* Not supported: data(FB 8F 56 E8 9A BF BB 4F 80 A8 6A CA 47 F4 7D 2A) */;

	internal static _GUID WPP_ThisDir_CTLGUID_CtlGuidZuneWindowsMobile/* Not supported: data(54 BA DE A8 04 76 6B 44 B6 CA D7 AF E6 EF BA 13) */;

	internal static _GUID WPP_ThisDir_CTLGUID_CtlGuidZuneService/* Not supported: data(94 2C 6C 0A 05 4F 08 4B B7 D9 F4 97 B3 5D DF 5D) */;

	internal static _GUID WPP_ThisDir_CTLGUID_CtlGuidZunePlayback/* Not supported: data(C4 0C AB 71 0F 96 25 4E 8E EE 41 54 AD 9A 64 26) */;

	internal static _GUID WPP_ThisDir_CTLGUID_CtlGuidZuneLibrary/* Not supported: data(42 31 E0 AC CB DE D2 43 88 30 B0 0D D6 1E 96 75) */;

	internal static _GUID WPP_ThisDir_CTLGUID_CtlGuidZuneSync/* Not supported: data(5E 32 67 7B 05 70 75 4E 8E 83 FF 99 11 6E B5 73) */;

	internal static _GUID WPP_ThisDir_CTLGUID_CtlGuidZune/* Not supported: data(F0 7A 8F C6 1A 37 19 4E B6 9A F9 E7 6F E6 BA FE) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2NotificationMarshaller_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(E8 9F 1F 65 98 9F 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2INotifySubscriber_0040_00408/* Not supported: data(98 9F 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040NotificationMarshaller_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(48 04 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 C8 9F 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040INotifySubscriber_0040_00408/* Not supported: data(08 04 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 7C 9F 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3NotificationMarshaller_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 D8 9F 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_58 _003F_003F_R0_003FAVNotificationMarshaller_0040Configuration_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 4E 6F 74 69 66 69 63 61 74 69 6F 6E 4D 61 72 73 68 61 6C 6C 65 72 40 43 6F 6E 66 69 67 75 72 61 74 69 6F 6E 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3INotifySubscriber_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 8C 9F 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_24 _003F_003F_R0_003FAUINotifySubscriber_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 4E 6F 74 69 66 79 53 75 62 73 63 72 69 62 65 72 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4NotificationMarshaller_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 48 04 25 65 C8 9F 1F 65) */;

	internal static __s_GUID _GUID_00e9004f_0cab_40ff_98ae_fad1a5ca594d/* Not supported: data(4F 00 E9 00 AB 0C FF 40 98 AE FA D1 A5 CA 59 4D) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7NotificationMarshaller_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(13 2F 20 65 1E 2F 20 65 37 BC 20 65 0C B1 20 65 9C 2E 20 65 00 00 00 00) */;

	internal static __s_GUID _GUID_7b6ba3cc_fb9a_4356_942a_7da1dc0eeb70/* Not supported: data(CC A3 6B 7B 9A FB 56 43 94 2A 7D A1 DC 0E EB 70) */;

	internal static __s_GUID _GUID_50502dd1_e15b_43fe_b10d_769116ead2b2/* Not supported: data(D1 2D 50 50 5B E1 FE 43 B1 0D 76 91 16 EA D2 B2) */;

	internal static __s_GUID _GUID_e7ffb676_fc41_4504_9768_1819b3535d23/* Not supported: data(76 B6 FF E7 41 FC 04 45 97 68 18 19 B3 53 5D 23) */;

	internal static __s_GUID _GUID_3b0ca835_8f64_4f22_95cf_ebd758a65835/* Not supported: data(35 A8 0C 3B 64 8F 22 4F 95 CF EB D7 58 A6 58 35) */;

	internal static __s_GUID _GUID_7472ae89_073d_420b_9828_51f9d80ca2a6/* Not supported: data(89 AE 72 74 3D 07 0B 42 98 28 51 F9 D8 0C A2 A6) */;

	internal static __s_GUID _GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71/* Not supported: data(6D 14 D7 6D 19 7A BB 4F 92 35 9E 6C 38 2F CC 71) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids/* Not supported: data(80 E0 22 8E 59 0A 2C 22 9F B4 57 48 A7 12 36 EB) */;

	internal static _0024ArrayType_0024_0024_0024BY06_0024_0024CBG _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040/* Not supported: data(3C 00 4E 00 55 00 4C 00 4C 00 3E 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY04_0024_0024CBG _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040/* Not supported: data(4E 00 55 00 4C 00 4C 00 00 00) */;

	internal static __s_GUID _GUID_53baba84_a16e_4fbd_a84a_428297ecff07/* Not supported: data(84 BA BA 53 6E A1 BD 4F A8 4A 42 82 97 EC FF 07) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids/* Not supported: data(88 96 4A 8D 27 72 FB FF 54 CB 5A 75 42 39 E3 17) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_00408/* Not supported: data(30 A0 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2DownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(08 A1 1F 65 68 A0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2DownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(B8 A0 1F 65 68 A0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IDownloadTaskProgress_0040_00408/* Not supported: data(68 A0 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_00408/* Not supported: data(C0 04 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 18 A0 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040DownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(E0 05 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E8 A0 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040DownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(70 05 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 98 A0 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IDownloadTaskProgress_0040_00408/* Not supported: data(24 05 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 4C A0 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 28 A0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_89 _003F_003F_R0_003FAV_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 3F 24 44 79 6E 61 6D 69 63 41 72 72 61 79 40 55 3F 24 67 63 72 6F 6F 74 40 50 24 41 41 56 44 6F 77 6E 6C 6F 61 64 4D 61 6E 61 67 65 72 55 70 64 61 74 65 48 61 6E 64 6C 65 72 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 40 40 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3DownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 F8 A0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_47 _003F_003F_R0_003FAVDownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 44 6F 77 6E 6C 6F 61 64 4D 61 6E 61 67 65 72 50 72 6F 78 79 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3DownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 A8 A0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_44 _003F_003F_R0_003FAVDownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 44 6F 77 6E 6C 6F 61 64 54 61 73 6B 50 72 6F 78 79 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IDownloadTaskProgress_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 5C A0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAUIDownloadTaskProgress_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 44 6F 77 6E 6C 6F 61 64 54 61 73 6B 50 72 6F 67 72 65 73 73 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 C0 04 25 65 18 A0 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 E0 05 25 65 E8 A0 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 70 05 25 65 98 A0 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY01Q6GXXZ _003F_003F_7_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_00406B_0040/* Not supported: data(26 5E 21 65 00 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09Q6GXXZ _003F_003F_7DownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(F2 4F 21 65 1B 50 21 65 5C 65 21 65 9B 67 21 65 F8 67 21 65 63 68 21 65 6E 4F 21 65 B4 68 21 65 79 4F 21 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_60fcb6b3_8562_4ddf_99f8_b93c08ed5e83/* Not supported: data(B3 B6 FC 60 62 85 DF 4D 99 F8 B9 3C 08 ED 5E 83) */;

	internal static __s_GUID _GUID_399f851b_a600_4e88_90c3_03b8f2770076/* Not supported: data(1B 85 9F 39 00 A6 88 4E 90 C3 03 B8 F2 77 00 76) */;

	internal static _0024ArrayType_0024_0024_0024BY09Q6GXXZ _003F_003F_7DownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(A6 4E 21 65 B1 4E 21 65 33 60 21 65 FB 4D 21 65 B4 5A 21 65 2B 5B 21 65 06 4E 21 65 6D 5B 21 65 2B 4E 21 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_36 _003F_003F_R2DeviceMediator_0040_00408/* Not supported: data(84 A3 1F 65 40 A1 1F 65 D0 A2 1F 65 A0 A3 1F 65 08 A3 1F 65 BC A3 1F 65 10 A4 1F 65 2C A4 1F 65 48 A4 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_20 _003F_003F_R2EndpointHostManagerMediator_0040_00408/* Not supported: data(B4 A2 1F 65 10 A2 1F 65 D0 A2 1F 65 EC A2 1F 65 08 A3 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IZuneWlanCallback_0040_00408/* Not supported: data(F4 A3 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IDeviceProgress_0040DeviceAccess_0040_00408/* Not supported: data(D8 A1 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IEndpointHostManagerCallback_0040_00408/* Not supported: data(5C A2 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2ISyncProgressCallback_0040_00408/* Not supported: data(40 A1 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IEndpointStatusCallback_0040_00408/* Not supported: data(8C A1 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IEndpointNotification_0040_00408/* Not supported: data(10 A2 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1M_0040_003F0A_0040EC_0040IUnknown_0040_00408/* Not supported: data(4C 01 25 65 00 00 00 00 0C 00 00 00 FF FF FF FF 00 00 00 00 42 00 00 00 E0 9C 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1M_0040_003F0A_0040EA_0040IDeviceProgress_0040DeviceAccess_0040_00408/* Not supported: data(B4 06 25 65 01 00 00 00 0C 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 BC A1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R17_003F0A_0040EC_0040IUnknown_0040_00408/* Not supported: data(4C 01 25 65 00 00 00 00 08 00 00 00 FF FF FF FF 00 00 00 00 42 00 00 00 E0 9C 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R17_003F0A_0040EA_0040IZuneWlanCallback_0040_00408/* Not supported: data(9C 08 25 65 01 00 00 00 08 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 D8 A3 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R13_003F0A_0040EA_0040IEndpointStatusCallback_0040_00408/* Not supported: data(70 06 25 65 01 00 00 00 04 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 70 A1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040DeviceMediator_0040_00408/* Not supported: data(7C 08 25 65 08 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 4C A3 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R13_003F0A_0040EC_0040IUnknown_0040_00408/* Not supported: data(4C 01 25 65 00 00 00 00 04 00 00 00 FF FF FF FF 00 00 00 00 42 00 00 00 E0 9C 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R13_003F0A_0040EA_0040IEndpointHostManagerCallback_0040_00408/* Not supported: data(28 07 25 65 01 00 00 00 04 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 40 A2 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EC_0040IUnknown_0040_00408/* Not supported: data(4C 01 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 42 00 00 00 E0 9C 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040EndpointHostManagerMediator_0040_00408/* Not supported: data(B8 07 25 65 04 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 8C A2 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IZuneWlanCallback_0040_00408/* Not supported: data(9C 08 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 D8 A3 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IDeviceProgress_0040DeviceAccess_0040_00408/* Not supported: data(B4 06 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 BC A1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IEndpointHostManagerCallback_0040_00408/* Not supported: data(28 07 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 40 A2 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040ISyncProgressCallback_0040_00408/* Not supported: data(34 06 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 24 A1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IEndpointStatusCallback_0040_00408/* Not supported: data(70 06 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 70 A1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IEndpointNotification_0040_00408/* Not supported: data(F0 06 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 F4 A1 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3DeviceMediator_0040_00408/* Not supported: data(00 00 00 00 05 00 00 00 09 00 00 00 5C A3 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_21 _003F_003F_R0_003FAVDeviceMediator_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 44 65 76 69 63 65 4D 65 64 69 61 74 6F 72 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3EndpointHostManagerMediator_0040_00408/* Not supported: data(00 00 00 00 05 00 00 00 05 00 00 00 9C A2 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_34 _003F_003F_R0_003FAVEndpointHostManagerMediator_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 45 6E 64 70 6F 69 6E 74 48 6F 73 74 4D 61 6E 61 67 65 72 4D 65 64 69 61 74 6F 72 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IZuneWlanCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 E8 A3 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_24 _003F_003F_R0_003FAUIZuneWlanCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 5A 75 6E 65 57 6C 61 6E 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IDeviceProgress_0040DeviceAccess_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 CC A1 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_35 _003F_003F_R0_003FAUIDeviceProgress_0040DeviceAccess_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 44 65 76 69 63 65 50 72 6F 67 72 65 73 73 40 44 65 76 69 63 65 41 63 63 65 73 73 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IEndpointHostManagerCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 50 A2 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_35 _003F_003F_R0_003FAUIEndpointHostManagerCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 45 6E 64 70 6F 69 6E 74 48 6F 73 74 4D 61 6E 61 67 65 72 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3ISyncProgressCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 34 A1 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAUISyncProgressCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 53 79 6E 63 50 72 6F 67 72 65 73 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IEndpointStatusCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 80 A1 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_30 _003F_003F_R0_003FAUIEndpointStatusCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 45 6E 64 70 6F 69 6E 74 53 74 61 74 75 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IEndpointNotification_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 04 A2 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAUIEndpointNotification_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 45 6E 64 70 6F 69 6E 74 4E 6F 74 69 66 69 63 61 74 69 6F 6E 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DeviceMediator_0040_00406BIDeviceProgress_0040DeviceAccess_0040_0040_0040/* Not supported: data(00 00 00 00 0C 00 00 00 00 00 00 00 7C 08 25 65 4C A3 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DeviceMediator_0040_00406BIZuneWlanCallback_0040_0040_0040/* Not supported: data(00 00 00 00 08 00 00 00 00 00 00 00 7C 08 25 65 4C A3 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DeviceMediator_0040_00406BIEndpointStatusCallback_0040_0040_0040/* Not supported: data(00 00 00 00 04 00 00 00 00 00 00 00 7C 08 25 65 4C A3 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DeviceMediator_0040_00406BISyncProgressCallback_0040_0040_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 7C 08 25 65 4C A3 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4EndpointHostManagerMediator_0040_00406BIEndpointHostManagerCallback_0040_0040_0040/* Not supported: data(00 00 00 00 04 00 00 00 00 00 00 00 B8 07 25 65 8C A2 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4EndpointHostManagerMediator_0040_00406BIEndpointNotification_0040_0040_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 B8 07 25 65 8C A2 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4IDeviceProgress_0040DeviceAccess_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 B4 06 25 65 BC A1 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4IEndpointHostManagerCallback_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 28 07 25 65 40 A2 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4IEndpointStatusCallback_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 70 06 25 65 70 A1 1F 65) */;

	internal static __s_GUID _GUID_bf820622_ebf9_4508_be2e_022a105fc881/* Not supported: data(22 06 82 BF F9 EB 08 45 BE 2E 02 2A 10 5F C8 81) */;

	internal static __s_GUID _GUID_d62cd97d_679e_4aa9_aac4_6eadf638e3b4/* Not supported: data(7D D9 2C D6 9E 67 A9 4A AA C4 6E AD F6 38 E3 B4) */;

	internal static __s_GUID _GUID_85e3445b_c6b2_4066_b714_35fe2ddf04d6/* Not supported: data(5B 44 E3 85 B2 C6 66 40 B7 14 35 FE 2D DF 04 D6) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7IDeviceProgress_0040DeviceAccess_0040_00406B_0040/* Not supported: data(34 B6 24 65 34 B6 24 65 34 B6 24 65 34 B6 24 65 34 B6 24 65 34 B6 24 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7IEndpointStatusCallback_0040_00406B_0040/* Not supported: data(34 B6 24 65 34 B6 24 65 34 B6 24 65 34 B6 24 65 34 B6 24 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7DeviceMediator_0040_00406BIDeviceProgress_0040DeviceAccess_0040_0040_0040/* Not supported: data(A1 86 21 65 51 86 21 65 01 8A 21 65 17 71 21 65 22 71 21 65 1F 78 21 65 78 A4 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY0L_0040Q6GXXZ _003F_003F_7DeviceMediator_0040_00406BIZuneWlanCallback_0040_0040_0040/* Not supported: data(78 86 21 65 00 86 21 65 D8 89 21 65 98 76 21 65 D0 76 21 65 08 77 21 65 40 77 21 65 78 77 21 65 B0 77 21 65 E8 77 21 65 64 A4 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7DeviceMediator_0040_00406BIEndpointStatusCallback_0040_0040_0040/* Not supported: data(28 86 21 65 D8 85 21 65 B0 89 21 65 27 76 21 65 60 76 21 65 38 A3 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7DeviceMediator_0040_00406BISyncProgressCallback_0040_0040_0040/* Not supported: data(CC 71 21 65 F3 71 21 65 87 89 21 65 6C 75 21 65 B2 75 21 65 F1 75 21 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_d8b0e1de_b8fe_4aec_bfd9_e707d8d65e1e/* Not supported: data(DE E1 B0 D8 FE B8 EC 4A BF D9 E7 07 D8 D6 5E 1E) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7IEndpointHostManagerCallback_0040_00406B_0040/* Not supported: data(34 B6 24 65 34 B6 24 65 34 B6 24 65 34 B6 24 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_0a3d3343_00d9_4c61_9a86_2d778793e05f/* Not supported: data(43 33 3D 0A D9 00 61 4C 9A 86 2D 77 87 93 E0 5F) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7EndpointHostManagerMediator_0040_00406BIEndpointHostManagerCallback_0040_0040_0040/* Not supported: data(FC 79 21 65 D4 79 21 65 F4 87 21 65 FD 82 21 65 78 A2 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7EndpointHostManagerMediator_0040_00406BIEndpointNotification_0040_0040_0040/* Not supported: data(EE 70 21 65 F9 70 21 65 36 83 21 65 01 7F 21 65 85 80 21 65 7D 7D 21 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids/* Not supported: data(A7 7F 6D 94 AA 86 E4 D5 48 EB 38 AE F7 B2 1A 4D) */;

	internal static __s_GUID _GUID_04f38ab5_391b_4b5a_a2c1_d4b74aeb4be9/* Not supported: data(B5 8A F3 04 1B 39 5A 4B A2 C1 D4 B7 4A EB 4B E9) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2FeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(D4 A4 1F 65 98 9D 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040FeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(FC 08 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 B4 A4 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3FeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 C4 A4 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_55 _003F_003F_R0_003FAVFeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 46 65 61 74 75 72 65 43 68 61 6E 67 65 64 49 6E 74 65 72 6F 70 57 72 61 70 70 65 72 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4FeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 FC 08 25 65 B4 A4 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7FeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(CD 98 21 65 D8 98 21 65 65 99 21 65 1B 99 21 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca/* Not supported: data(1A B4 81 95 CF B5 BF 4E 9D 1A 97 54 77 E0 81 CA) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2FirmwareUpdateMediator_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(AC A5 1F 65 40 A5 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(24 A5 1F 65 40 A5 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2InternalErrorInfo_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(FC A5 1F 65 18 A6 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IFirmwareUpdateCallback_0040_00408/* Not supported: data(40 A5 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IFirmwareUpdateErrorInfo_0040_00408/* Not supported: data(18 A6 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040FirmwareUpdateMediator_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(14 0A 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 8C A5 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(94 09 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 04 A5 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040InternalErrorInfo_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(74 0A 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 DC A5 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IFirmwareUpdateCallback_0040_00408/* Not supported: data(D0 09 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 5C A5 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IFirmwareUpdateErrorInfo_0040_00408/* Not supported: data(AC 0A 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 34 A6 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3FirmwareUpdateMediator_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 9C A5 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_50 _003F_003F_R0_003FAVFirmwareUpdateMediator_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 46 69 72 6D 77 61 72 65 55 70 64 61 74 65 4D 65 64 69 61 74 6F 72 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 14 A5 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_51 _003F_003F_R0_003FAVCheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 68 65 63 6B 46 6F 72 55 70 64 61 74 65 73 43 61 6C 6C 62 61 63 6B 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3InternalErrorInfo_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 EC A5 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_45 _003F_003F_R0_003FAVInternalErrorInfo_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 49 6E 74 65 72 6E 61 6C 45 72 72 6F 72 49 6E 66 6F 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IFirmwareUpdateCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 6C A5 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_30 _003F_003F_R0_003FAUIFirmwareUpdateCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 46 69 72 6D 77 61 72 65 55 70 64 61 74 65 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IFirmwareUpdateErrorInfo_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 44 A6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_31 _003F_003F_R0_003FAUIFirmwareUpdateErrorInfo_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 46 69 72 6D 77 61 72 65 55 70 64 61 74 65 45 72 72 6F 72 49 6E 66 6F 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4FirmwareUpdateMediator_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 14 0A 25 65 8C A5 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 94 09 25 65 04 A5 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4InternalErrorInfo_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 74 0A 25 65 DC A5 1F 65) */;

	internal static __s_GUID _GUID_7d0dee42_94b2_49a4_99fa_bae184d835d2/* Not supported: data(42 EE 0D 7D B2 94 A4 49 99 FA BA E1 84 D8 35 D2) */;

	internal static __s_GUID _GUID_6e2f6626_94f9_4e71_8736_f2e4c5a00a3b/* Not supported: data(26 66 2F 6E F9 94 71 4E 87 36 F2 E4 C5 A0 0A 3B) */;

	internal static __s_GUID _GUID_f066fc29_e525_4ddc_abe6_5213d22c14d2/* Not supported: data(29 FC 66 F0 25 E5 DC 4D AB E6 52 13 D2 2C 14 D2) */;

	internal static __s_GUID _GUID_c3b03bab_79ec_4e44_a0ec_2e0ff3b747de/* Not supported: data(AB 3B B0 C3 EC 79 44 4E A0 EC 2E 0F F3 B7 47 DE) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7InternalErrorInfo_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(CD A4 21 65 D8 A4 21 65 32 CA 21 65 35 A4 21 65 D4 B9 21 65 22 BA 21 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_41ccb4e2_52e7_436e_b9ad_8d9962e0cd11/* Not supported: data(E2 B4 CC 41 E7 52 6E 43 B9 AD 8D 99 62 E0 CD 11) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7FirmwareUpdateMediator_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(ED A3 21 65 F8 A3 21 65 17 D4 21 65 DD B6 21 65 FD B7 21 65 F9 C8 21 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_f882fe79_4b03_48f3_b52b_67dc5b2dfb3b/* Not supported: data(79 FE 82 F8 03 4B F3 48 B5 2B 67 DC 5B 2D FB 3B) */;

	internal static __s_GUID _GUID_1052ac05_e104_4a08_a83c_89b2e3df1743/* Not supported: data(05 AC 52 10 04 E1 08 4A A8 3C 89 B2 E3 DF 17 43) */;

	internal static _0024ArrayType_0024_0024_0024BY07Q6GXXZ _003F_003F_7CheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(33 A3 21 65 5A A3 21 65 98 A3 21 65 1D A3 21 65 28 A3 21 65 95 C7 21 65 5E C6 21 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids/* Not supported: data(8E 8C 9F A5 F0 E6 65 84 7A C8 2B DC 8F 52 A9 FA) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2GasGaugeMediator_0040_00408/* Not supported: data(BC A6 1F 65 6C A6 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IGasGaugeCallback_0040_00408/* Not supported: data(6C A6 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040GasGaugeMediator_0040_00408/* Not supported: data(30 0B 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 9C A6 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IGasGaugeCallback_0040_00408/* Not supported: data(F4 0A 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 50 A6 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3GasGaugeMediator_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 AC A6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_23 _003F_003F_R0_003FAVGasGaugeMediator_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 47 61 73 47 61 75 67 65 4D 65 64 69 61 74 6F 72 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IGasGaugeCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 60 A6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_24 _003F_003F_R0_003FAUIGasGaugeCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 47 61 73 47 61 75 67 65 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4GasGaugeMediator_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 30 0B 25 65 9C A6 1F 65) */;

	internal static __s_GUID _GUID_7a68ebac_a036_41dc_90e4_8b39c5be83a3/* Not supported: data(AC EB 68 7A 36 A0 DC 41 90 E4 8B 39 C5 BE 83 A3) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7GasGaugeMediator_0040_00406B_0040/* Not supported: data(BD 02 22 65 6D 01 22 65 7A 03 22 65 FC 01 22 65 33 02 22 65 69 02 22 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040DOFICLFP_0040_003F_0024AAC_003F_0024AAo_003F_0024AAn_003F_0024AAt_003F_0024AAe_003F_0024AAn_003F_0024AAt_003F_0024AA_003F9_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(43 00 6F 00 6E 00 74 00 65 00 6E 00 74 00 2D 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBG _003F_003F_C_0040_1BA_0040FICLJLAN_0040_003F_0024AAE_003F_0024AAx_003F_0024AAp_003F_0024AAi_003F_0024AAr_003F_0024AAe_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(45 00 78 00 70 00 69 00 72 00 65 00 73 00 00 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(44 A7 1F 65 F4 A6 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IHttpWebRequestCallback_0040_00408/* Not supported: data(F4 A6 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(C0 0B 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 24 A7 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IHttpWebRequestCallback_0040_00408/* Not supported: data(78 0B 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 D8 A6 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 34 A7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_56 _003F_003F_R0_003FAVCWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 57 65 62 52 65 71 75 65 73 74 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IHttpWebRequestCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 E8 A6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_30 _003F_003F_R0_003FAUIHttpWebRequestCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 48 74 74 70 57 65 62 52 65 71 75 65 73 74 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 C0 0B 25 65 24 A7 1F 65) */;

	internal static __s_GUID _GUID_a3138a7c_be4e_4aa1_999f_f2fdd4b3f428/* Not supported: data(7C 8A 13 A3 4E BE A1 4A 99 9F F2 FD D4 B3 F4 28) */;

	internal static _0024ArrayType_0024_0024_0024BY07Q6GXXZ _003F_003F_7CWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(B6 0B 22 65 C1 0B 22 65 96 16 22 65 CC 0B 22 65 EB 0B 22 65 F6 0B 22 65 3D 11 22 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040BFIEKNFP_0040_003F_0024AAF_003F_0024AAr_003F_0024AAi_003F_0024AAe_003F_0024AAn_003F_0024AAd_003F_0024AAl_003F_0024AAy_003F_0024AAN_003F_0024AAa_003F_0024AAm_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(46 00 72 00 69 00 65 00 6E 00 64 00 6C 00 79 00 4E 00 61 00 6D 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040GBMCOKGG_0040_003F_0024AAS_003F_0024AAe_003F_0024AAr_003F_0024AAi_003F_0024AAa_003F_0024AAl_003F_0024AAN_003F_0024AAu_003F_0024AAm_003F_0024AAb_003F_0024AAe_003F_0024AAr_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 65 00 72 00 69 00 61 00 6C 00 4E 00 75 00 6D 00 62 00 65 00 72 00 00 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2NativeInteropNotifications_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(CC A7 1F 65 7C A7 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IInteropNotify_0040_00408/* Not supported: data(7C A7 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040NativeInteropNotifications_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(48 0C 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 AC A7 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IInteropNotify_0040_00408/* Not supported: data(0C 0C 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 60 A7 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3NativeInteropNotifications_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 BC A7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_54 _003F_003F_R0_003FAVNativeInteropNotifications_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 4E 61 74 69 76 65 49 6E 74 65 72 6F 70 4E 6F 74 69 66 69 63 61 74 69 6F 6E 73 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IInteropNotify_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 70 A7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_21 _003F_003F_R0_003FAUIInteropNotify_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 49 6E 74 65 72 6F 70 4E 6F 74 69 66 79 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4NativeInteropNotifications_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 48 0C 25 65 AC A7 1F 65) */;

	internal static __s_GUID _GUID_3fb2d757_8ddb_46a9_9dd2_3424e2903e46/* Not supported: data(57 D7 B2 3F DB 8D A9 46 9D D2 34 24 E2 90 3E 46) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7NativeInteropNotifications_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(5D 25 22 65 CD 24 22 65 8A 27 22 65 C1 25 22 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBG _003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040/* Not supported: data(00 00) */;

	internal static __s_GUID _GUID_0e0fc989_9392_467b_92fc_80d4c1999a97/* Not supported: data(89 C9 0F 0E 92 93 7B 46 92 FC 80 D4 C1 99 9A 97) */;

	internal static uint MicrosoftZuneLibrary_002E_003FA0xb7c00b43_002Es_uNextTraceId/*Field data (rva=0x102f6c) could not be found in any section!*/;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids/* Not supported: data(D0 AB 49 CE D1 F4 96 0C 99 6C 4D 26 01 C7 38 E5) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2WMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(54 A8 1F 65 04 A8 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IWMISGetAlbumForAlbumIdCallback_0040_00408/* Not supported: data(04 A8 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040WMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(D8 0C 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 34 A8 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IWMISGetAlbumForAlbumIdCallback_0040_00408/* Not supported: data(94 0C 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E8 A7 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3WMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 44 A8 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_65 _003F_003F_R0_003FAVWMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 57 4D 49 53 47 65 74 41 6C 62 75 6D 46 6F 72 41 6C 62 75 6D 49 64 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IWMISGetAlbumForAlbumIdCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 F8 A7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_38 _003F_003F_R0_003FAUIWMISGetAlbumForAlbumIdCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 57 4D 49 53 47 65 74 41 6C 62 75 6D 46 6F 72 41 6C 62 75 6D 49 64 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4WMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 D8 0C 25 65 34 A8 1F 65) */;

	internal static __s_GUID _GUID_7f89b907_d770_41d1_9fd7_5fd3777649b9/* Not supported: data(07 B9 89 7F 70 D7 D1 41 9F D7 5F D3 77 76 49 B9) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7WMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(B2 79 22 65 BD 79 22 65 92 8A 22 65 EA 81 22 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_00408/* Not supported: data(A4 A8 1F 65 C0 A8 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IZuneMBRBandwidthTestEventSink_0040_00408/* Not supported: data(C0 A8 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_00408/* Not supported: data(4C 0D 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 84 A8 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IZuneMBRBandwidthTestEventSink_0040_00408/* Not supported: data(8C 0D 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 DC A8 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 94 A8 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_55 _003F_003F_R0_003FAVCMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 4D 42 52 42 61 6E 64 77 69 64 74 68 54 65 73 74 45 76 65 6E 74 53 69 6E 6B 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 50 6C 61 79 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IZuneMBRBandwidthTestEventSink_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 EC A8 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_37 _003F_003F_R0_003FAUIZuneMBRBandwidthTestEventSink_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 5A 75 6E 65 4D 42 52 42 61 6E 64 77 69 64 74 68 54 65 73 74 45 76 65 6E 74 53 69 6E 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 4C 0D 25 65 84 A8 1F 65) */;

	internal static __s_GUID _GUID_e4f957fa_2742_4393_9518_c2705fb5c517/* Not supported: data(FA 57 F9 E4 42 27 93 43 95 18 C2 70 5F B5 C5 17) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7CMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_00406B_0040/* Not supported: data(2E 8E 22 65 56 8E 22 65 F2 91 22 65 09 95 22 65 6D 95 22 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2MessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(04 AA 1F 65 98 9F 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2AddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(B4 A9 1F 65 48 A9 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2MessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(2C A9 1F 65 48 A9 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IMessagingCallback_0040_00408/* Not supported: data(48 A9 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040MessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(D4 0E 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E4 A9 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040AddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(70 0E 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 94 A9 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040MessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(E8 0D 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 0C A9 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IMessagingCallback_0040_00408/* Not supported: data(28 0E 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 64 A9 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3MessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 F4 A9 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_51 _003F_003F_R0_003FAVMessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 4D 65 73 73 61 67 69 6E 67 53 75 62 73 63 72 69 62 65 72 40 4D 65 73 73 61 67 69 6E 67 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3AddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 A4 A9 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_57 _003F_003F_R0_003FAVAddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 41 64 64 43 6F 6D 6D 65 6E 74 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 4D 65 73 73 61 67 69 6E 67 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3MessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 1C A9 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_56 _003F_003F_R0_003FAVMessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 4D 65 73 73 61 67 69 6E 67 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 4D 65 73 73 61 67 69 6E 67 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IMessagingCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 74 A9 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_25 _003F_003F_R0_003FAUIMessagingCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 4D 65 73 73 61 67 69 6E 67 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4MessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 D4 0E 25 65 E4 A9 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4AddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 70 0E 25 65 94 A9 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4MessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 E8 0D 25 65 0C A9 1F 65) */;

	internal static __s_GUID _GUID_fd0ba7bb_76c8_4451_8842_7138fa2edd72/* Not supported: data(BB A7 0B FD C8 76 51 44 88 42 71 38 FA 2E DD 72) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7MessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(3E 9A 22 65 D0 99 22 65 9E B0 22 65 63 9A 22 65 FA A3 22 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_bf368f0d_4743_439c_9142_e487c9534104/* Not supported: data(0D 8F 36 BF 43 47 9C 43 91 42 E4 87 C9 53 41 04) */;

	internal static __s_GUID _GUID_1221e242_5ca1_4024_8789_9778b59ca5de/* Not supported: data(42 E2 21 12 A1 5C 24 40 87 89 97 78 B5 9C A5 DE) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7AddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(63 9C 22 65 6E 9C 22 65 56 A6 22 65 80 A7 22 65 00 00 00 00) */;

	internal static __s_GUID _GUID_39977545_c867_4bd4_b5a1_e4ee473a1e8b/* Not supported: data(45 75 97 39 67 C8 D4 4B B5 A1 E4 EE 47 3A 1E 8B) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7MessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(15 9B 22 65 20 9B 22 65 7A A5 22 65 AB 9B 22 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2NativeMetadataNotifications_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(8C AA 1F 65 3C AA 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IMetadataChangeNotify_0040_00408/* Not supported: data(3C AA 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040NativeMetadataNotifications_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(DC 0F 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 6C AA 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IMetadataChangeNotify_0040_00408/* Not supported: data(2C 0F 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 20 AA 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3NativeMetadataNotifications_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 7C AA 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_55 _003F_003F_R0_003FAVNativeMetadataNotifications_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 4E 61 74 69 76 65 4D 65 74 61 64 61 74 61 4E 6F 74 69 66 69 63 61 74 69 6F 6E 73 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IMetadataChangeNotify_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 30 AA 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAUIMetadataChangeNotify_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 4D 65 74 61 64 61 74 61 43 68 61 6E 67 65 4E 6F 74 69 66 79 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4NativeMetadataNotifications_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 DC 0F 25 65 6C AA 1F 65) */;

	internal static __s_GUID _GUID_d67cdf64_5ea9_44ea_bf5c_29a422f4c23f/* Not supported: data(64 DF 7C D6 A9 5E EA 44 BF 5C 29 A4 22 F4 C2 3F) */;

	internal static _0024ArrayType_0024_0024_0024BY0BD_0040Q6GXXZ _003F_003F_7NativeMetadataNotifications_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(59 BA 22 65 64 BA 22 65 9E C0 22 65 10 BB 22 65 57 BB 22 65 9F BB 22 65 E7 BB 22 65 2F BC 22 65 AF BC 22 65 EF BC 22 65 6F BC 22 65 83 BD 22 65 C3 BD 22 65 43 BD 22 65 03 BE 22 65 43 BE 22 65 8B BE 22 65 D3 BE 22 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2NSSMediator_0040_00408/* Not supported: data(DC AA 1F 65 F8 AA 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2INSSNotify_0040_00408/* Not supported: data(F8 AA 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040NSSMediator_0040_00408/* Not supported: data(40 10 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 BC AA 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040INSSNotify_0040_00408/* Not supported: data(5C 10 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 14 AB 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3NSSMediator_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 CC AA 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_18 _003F_003F_R0_003FAVNSSMediator_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 4E 53 53 4D 65 64 69 61 74 6F 72 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3INSSNotify_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 24 AB 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_17 _003F_003F_R0_003FAUINSSNotify_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 4E 53 53 4E 6F 74 69 66 79 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4NSSMediator_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 40 10 25 65 BC AA 1F 65) */;

	internal static __s_GUID _GUID_78046458_f53a_43b3_a59a_b608960e3a6b/* Not supported: data(58 64 04 78 3A F5 B3 43 A5 9A B6 08 96 0E 3A 6B) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7NSSMediator_0040_00406B_0040/* Not supported: data(A1 C1 22 65 04 C1 22 65 8E C2 22 65 4A C1 22 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_655b468c_1224_467d_b720_3bac7f99b6ba/* Not supported: data(8C 46 5B 65 24 12 7D 46 B7 20 3B AC 7F 99 B6 BA) */;

	internal static __s_GUID _GUID_b396c324_6ab3_4e8e_a5cd_aafb3e01bedc/* Not supported: data(24 C3 96 B3 B3 6A 8E 4E A5 CD AA FB 3E 01 BE DC) */;

	internal static __s_GUID _GUID_16a9f8be_e76c_4391_ad74_8df74b7a3c21/* Not supported: data(BE F8 A9 16 6C E7 91 43 AD 74 8D F7 4B 7A 3C 21) */;

	internal static __s_GUID _GUID_a2889317_d0c7_41d8_abc7_1eb4cb8d46d6/* Not supported: data(17 93 88 A2 C7 D0 D8 41 AB C7 1E B4 CB 8D 46 D6) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_28 _003F_003F_R2CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00408/* Not supported: data(74 AB 1F 65 90 AB 1F 65 D0 A2 1F 65 C8 AB 1F 65 08 A3 1F 65 1C AC 1F 65 10 A4 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IMCTransportEvents_0040_00408/* Not supported: data(00 AC 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IMCPlayerSetUriEvents_0040_00408/* Not supported: data(54 AC 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IMCPlayerEvents_0040_00408/* Not supported: data(90 AB 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R17_003F0A_0040EA_0040IMCPlayerSetUriEvents_0040_00408/* Not supported: data(5C 11 25 65 01 00 00 00 08 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 38 AC 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R13_003F0A_0040EA_0040IMCTransportEvents_0040_00408/* Not supported: data(38 11 25 65 01 00 00 00 04 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E4 AB 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00408/* Not supported: data(DC 10 25 65 06 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 44 AB 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IMCTransportEvents_0040_00408/* Not supported: data(38 11 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E4 AB 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IMCPlayerSetUriEvents_0040_00408/* Not supported: data(5C 11 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 38 AC 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IMCPlayerEvents_0040_00408/* Not supported: data(18 11 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 AC AB 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00408/* Not supported: data(00 00 00 00 05 00 00 00 07 00 00 00 54 AB 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_52 _003F_003F_R0_003FAVCPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 50 6C 61 79 65 72 49 6E 74 65 72 6F 70 45 76 65 6E 74 53 69 6E 6B 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 50 6C 61 79 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IMCTransportEvents_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 F4 AB 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_25 _003F_003F_R0_003FAUIMCTransportEvents_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 4D 43 54 72 61 6E 73 70 6F 72 74 45 76 65 6E 74 73 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IMCPlayerSetUriEvents_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 48 AC 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAUIMCPlayerSetUriEvents_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 4D 43 50 6C 61 79 65 72 53 65 74 55 72 69 45 76 65 6E 74 73 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IMCPlayerEvents_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 BC AB 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_22 _003F_003F_R0_003FAUIMCPlayerEvents_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 4D 43 50 6C 61 79 65 72 45 76 65 6E 74 73 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerSetUriEvents_0040_0040_0040/* Not supported: data(00 00 00 00 08 00 00 00 00 00 00 00 DC 10 25 65 44 AB 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCTransportEvents_0040_0040_0040/* Not supported: data(00 00 00 00 04 00 00 00 00 00 00 00 DC 10 25 65 44 AB 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerEvents_0040_0040_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 DC 10 25 65 44 AB 1F 65) */;

	internal static __s_GUID _GUID_aff1732d_13f3_45e5_a52f_a854729e8730/* Not supported: data(2D 73 F1 AF F3 13 E5 45 A5 2F A8 54 72 9E 87 30) */;

	internal static __s_GUID _GUID_f6ba930c_78c3_488c_924d_2d3fc1e8fb70/* Not supported: data(0C 93 BA F6 C3 78 8C 48 92 4D 2D 3F C1 E8 FB 70) */;

	internal static __s_GUID _GUID_102e281e_28ad_4688_aaff_f560f8053d90/* Not supported: data(1E 28 2E 10 AD 28 88 46 AA FF F5 60 F8 05 3D 90) */;

	internal static __s_GUID _GUID_58864c93_45f9_4c6d_aa3f_80f6caa08281/* Not supported: data(93 4C 86 58 F9 45 6D 4C AA 3F 80 F6 CA A0 82 81) */;

	internal static __s_GUID _GUID_2f33a725_95cb_4080_adef_93a067a707ba/* Not supported: data(25 A7 33 2F CB 95 80 40 AD EF 93 A0 67 A7 07 BA) */;

	internal static __s_GUID _GUID_b1d19423_6060_493c_90cd_c6e39f0fa760/* Not supported: data(23 94 D1 B1 60 60 3C 49 90 CD C6 E3 9F 0F A7 60) */;

	internal static __s_GUID _GUID_576ef3d5_d89e_482e_ad2c_889c8e504983/* Not supported: data(D5 F3 6E 57 9E D8 2E 48 AD 2C 88 9C 8E 50 49 83) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerSetUriEvents_0040_0040_0040/* Not supported: data(04 E2 22 65 B4 E1 22 65 94 EA 22 65 2E E3 22 65 70 AC 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCTransportEvents_0040_0040_0040/* Not supported: data(DC E1 22 65 8C E1 22 65 6C EA 22 65 CE E2 22 65 00 E3 22 65 7F D8 22 65 30 AB 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY07Q6GXXZ _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerEvents_0040_0040_0040/* Not supported: data(07 D8 22 65 2F D8 22 65 13 EA 22 65 6E E2 22 65 42 EA 22 65 4F D8 22 65 A0 E2 22 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_c2d9122b_f648_4b95_92fc_11f2e7f326d7/* Not supported: data(2B 12 D9 C2 48 F6 95 4B 92 FC 11 F2 E7 F3 26 D7) */;

	public unsafe static int** __unep_0040_003FSavePlaylistAsStaticThreadProc_0040PlaylistAsyncOperation_0040Playlist_0040Zune_0040Microsoft_0040_0040_0024_0024FCGKPAX_0040Z/* Not supported: data(B5 F7 22 65) */;

	internal static _0024ArrayType_0024_0024_0024BY06_0024_0024CBG _003F_003F_C_0040_1O_0040JLJABCOF_0040_003F_0024AAU_003F_0024AAs_003F_0024AAe_003F_0024AAr_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(55 00 73 00 65 00 72 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040PDEHEDHD_0040_003F_0024AAD_003F_0024AAe_003F_0024AAv_003F_0024AAi_003F_0024AAc_003F_0024AAe_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 65 00 76 00 69 00 63 00 65 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0L_0040_0024_0024CBG _003F_003F_C_0040_1BG_0040DDEIGLKB_0040_003F_0024AAR_003F_0024AAu_003F_0024AAl_003F_0024AAe_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(52 00 75 00 6C 00 65 00 54 00 79 00 70 00 65 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040HKLCJAIP_0040_003F_0024AAA_003F_0024AAr_003F_0024AAt_003F_0024AAi_003F_0024AAs_003F_0024AAt_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(41 00 72 00 74 00 69 00 73 00 74 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040DFJOGCAA_0040_003F_0024AAA_003F_0024AAr_003F_0024AAt_003F_0024AAi_003F_0024AAs_003F_0024AAt_003F_0024AAI_003F_0024AAd_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(41 00 72 00 74 00 69 00 73 00 74 00 49 00 64 00 73 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BF_0040_0024_0024CBG _003F_003F_C_0040_1CK_0040KDKBJIAI_0040_003F_0024AAC_003F_0024AAo_003F_0024AAn_003F_0024AAt_003F_0024AAr_003F_0024AAi_003F_0024AAb_003F_0024AAu_003F_0024AAt_003F_0024AAi_003F_0024AAn_003F_0024AAg_003F_0024AAA_003F_0024AAr_003F_0024AAt_003F_0024AAi_003F_0024AAs_003F_0024AAt_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(43 00 6F 00 6E 00 74 00 72 00 69 00 62 00 75 00 74 00 69 00 6E 00 67 00 41 00 72 00 74 00 69 00 73 00 74 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBG _003F_003F_C_0040_1BA_0040DGIOPPBE_0040_003F_0024AAA_003F_0024AAl_003F_0024AAb_003F_0024AAu_003F_0024AAm_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(41 00 6C 00 62 00 75 00 6D 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040HKCGABCE_0040_003F_0024AAA_003F_0024AAl_003F_0024AAb_003F_0024AAu_003F_0024AAm_003F_0024AAI_003F_0024AAd_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(41 00 6C 00 62 00 75 00 6D 00 49 00 64 00 73 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040JCKMNCPP_0040_003F_0024AAS_003F_0024AAe_003F_0024AAr_003F_0024AAi_003F_0024AAe_003F_0024AAs_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 65 00 72 00 69 00 65 00 73 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040PDFIJCIO_0040_003F_0024AAF_003F_0024AAo_003F_0024AAl_003F_0024AAd_003F_0024AAe_003F_0024AAr_003F_0024AAI_003F_0024AAD_003F_0024AA_003F_0024AA_0040/* Not supported: data(46 00 6F 00 6C 00 64 00 65 00 72 00 49 00 44 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0L_0040_0024_0024CBG _003F_003F_C_0040_1BG_0040HIHNPLIF_0040_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAl_003F_0024AAi_003F_0024AAs_003F_0024AAt_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(50 00 6C 00 61 00 79 00 6C 00 69 00 73 00 74 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBG _003F_003F_C_0040_1BA_0040PPHKCEPN_0040_003F_0024AAG_003F_0024AAe_003F_0024AAn_003F_0024AAr_003F_0024AAe_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(47 00 65 00 6E 00 72 00 65 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040OEJLILCJ_0040_003F_0024AAG_003F_0024AAe_003F_0024AAn_003F_0024AAr_003F_0024AAe_003F_0024AAI_003F_0024AAd_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(47 00 65 00 6E 00 72 00 65 00 49 00 64 00 73 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040IIEFMAJ_0040_003F_0024AAM_003F_0024AAe_003F_0024AAd_003F_0024AAi_003F_0024AAa_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(4D 00 65 00 64 00 69 00 61 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040JICBJLMN_0040_003F_0024AAQ_003F_0024AAu_003F_0024AAe_003F_0024AAr_003F_0024AAy_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(51 00 75 00 65 00 72 00 79 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040EJGEHGOH_0040_003F_0024AAQ_003F_0024AAu_003F_0024AAe_003F_0024AAr_003F_0024AAy_003F_0024AAV_003F_0024AAi_003F_0024AAe_003F_0024AAw_003F_0024AA_003F_0024AA_0040/* Not supported: data(51 00 75 00 65 00 72 00 79 00 56 00 69 00 65 00 77 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040JJJGIENO_0040_003F_0024AAO_003F_0024AAp_003F_0024AAe_003F_0024AAr_003F_0024AAa_003F_0024AAt_003F_0024AAi_003F_0024AAo_003F_0024AAn_003F_0024AA_003F_0024AA_0040/* Not supported: data(4F 00 70 00 65 00 72 00 61 00 74 00 69 00 6F 00 6E 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040BKHGPOG_0040_003F_0024AAI_003F_0024AAn_003F_0024AAi_003F_0024AAt_003F_0024AAT_003F_0024AAi_003F_0024AAm_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(49 00 6E 00 69 00 74 00 54 00 69 00 6D 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BA_0040_0024_0024CBG _003F_003F_C_0040_1CA_0040DFOLKBJO_0040_003F_0024AAS_003F_0024AAy_003F_0024AAn_003F_0024AAc_003F_0024AAM_003F_0024AAa_003F_0024AAp_003F_0024AAp_003F_0024AAe_003F_0024AAd_003F_0024AAE_003F_0024AAr_003F_0024AAr_003F_0024AAo_003F_0024AAr_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 79 00 6E 00 63 00 4D 00 61 00 70 00 70 00 65 00 64 00 45 00 72 00 72 00 6F 00 72 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040KMDLMMHN_0040_003F_0024AAK_003F_0024AAe_003F_0024AAy_003F_0024AAw_003F_0024AAo_003F_0024AAr_003F_0024AAd_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(4B 00 65 00 79 00 77 00 6F 00 72 00 64 00 73 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY03_0024_0024CBG _003F_003F_C_0040_17DMBOJCMA_0040_003F_0024AAT_003F_0024AAO_003F_0024AAC_003F_0024AA_003F_0024AA_0040/* Not supported: data(54 00 4F 00 43 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040DDIALECK_0040_003F_0024AAS_003F_0024AAo_003F_0024AAr_003F_0024AAt_003F_0024AAC_003F_0024AAo_003F_0024AAl_003F_0024AAu_003F_0024AAm_003F_0024AAn_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 6F 00 72 00 74 00 43 00 6F 00 6C 00 75 00 6D 00 6E 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0L_0040_0024_0024CBG _003F_003F_C_0040_1BG_0040OMHCOFOE_0040_003F_0024AAS_003F_0024AAo_003F_0024AAr_003F_0024AAt_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 6F 00 72 00 74 00 54 00 79 00 70 00 65 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BB_0040_0024_0024CBG _003F_003F_C_0040_1CC_0040PNMMPGKJ_0040_003F_0024AAS_003F_0024AAo_003F_0024AAr_003F_0024AAt_003F_0024AAA_003F_0024AAt_003F_0024AAt_003F_0024AAr_003F_0024AAi_003F_0024AAb_003F_0024AAu_003F_0024AAt_003F_0024AAe_003F_0024AAs_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 6F 00 72 00 74 00 41 00 74 00 74 00 72 00 69 00 62 00 75 00 74 00 65 00 73 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040JFGNAANP_0040_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAl_003F_0024AAi_003F_0024AAs_003F_0024AAt_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(50 00 6C 00 61 00 79 00 6C 00 69 00 73 00 74 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BB_0040_0024_0024CBG _003F_003F_C_0040_1CC_0040GECFPDAM_0040_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAl_003F_0024AAi_003F_0024AAs_003F_0024AAt_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AAM_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AA_003F_0024AA_0040/* Not supported: data(50 00 6C 00 61 00 79 00 6C 00 69 00 73 00 74 00 54 00 79 00 70 00 65 00 4D 00 61 00 73 00 6B 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040BCBGOCNJ_0040_003F_0024AAI_003F_0024AAn_003F_0024AAL_003F_0024AAi_003F_0024AAb_003F_0024AAr_003F_0024AAa_003F_0024AAr_003F_0024AAy_003F_0024AA_003F_0024AA_0040/* Not supported: data(49 00 6E 00 4C 00 69 00 62 00 72 00 61 00 72 00 79 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0L_0040_0024_0024CBG _003F_003F_C_0040_1BG_0040BDNCKGC_0040_003F_0024AAC_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AAg_003F_0024AAo_003F_0024AAr_003F_0024AAy_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(43 00 61 00 74 00 65 00 67 00 6F 00 72 00 79 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0L_0040_0024_0024CBG _003F_003F_C_0040_1BG_0040BFODNNAL_0040_003F_0024AAP_003F_0024AAe_003F_0024AAr_003F_0024AAs_003F_0024AAo_003F_0024AAn_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(50 00 65 00 72 00 73 00 6F 00 6E 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBG _003F_003F_C_0040_1BA_0040LKPKACFF_0040_003F_0024AAM_003F_0024AAe_003F_0024AAd_003F_0024AAi_003F_0024AAa_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(4D 00 65 00 64 00 69 00 61 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0M_0040_0024_0024CBG _003F_003F_C_0040_1BI_0040CNOCMOCN_0040_003F_0024AAU_003F_0024AAs_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAa_003F_0024AAr_003F_0024AAd_003F_0024AAI_003F_0024AAd_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(55 00 73 00 65 00 72 00 43 00 61 00 72 00 64 00 49 00 64 00 73 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0P_0040_0024_0024CBG _003F_003F_C_0040_1BO_0040CBIMGG_0040_003F_0024AAM_003F_0024AAa_003F_0024AAx_003F_0024AAR_003F_0024AAe_003F_0024AAs_003F_0024AAu_003F_0024AAl_003F_0024AAt_003F_0024AAC_003F_0024AAo_003F_0024AAu_003F_0024AAn_003F_0024AAt_003F_0024AA_003F_0024AA_0040/* Not supported: data(4D 00 61 00 78 00 52 00 65 00 73 00 75 00 6C 00 74 00 43 00 6F 00 75 00 6E 00 74 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040BNEGFOIA_0040_003F_0024AAW_003F_0024AAa_003F_0024AAt_003F_0024AAc_003F_0024AAh_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(57 00 61 00 74 00 63 00 68 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0M_0040_0024_0024CBG _003F_003F_C_0040_1BI_0040NABDIIHL_0040_003F_0024AAE_003F_0024AAx_003F_0024AAp_003F_0024AAi_003F_0024AAr_003F_0024AAe_003F_0024AAs_003F_0024AAO_003F_0024AAn_003F_0024AAl_003F_0024AAy_003F_0024AA_003F_0024AA_0040/* Not supported: data(45 00 78 00 70 00 69 00 72 00 65 00 73 00 4F 00 6E 00 6C 00 79 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040KMEHKLG_0040_003F_0024AAD_003F_0024AAr_003F_0024AAm_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AAM_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 72 00 6D 00 53 00 74 00 61 00 74 00 65 00 4D 00 61 00 73 00 6B 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBG _003F_003F_C_0040_1BA_0040DMMPBFMI_0040_003F_0024AAP_003F_0024AAi_003F_0024AAn_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(50 00 69 00 6E 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040FJIGCHKK_0040_003F_0024AAR_003F_0024AAe_003F_0024AAc_003F_0024AAu_003F_0024AAr_003F_0024AAs_003F_0024AAi_003F_0024AAv_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(52 00 65 00 63 00 75 00 72 00 73 00 69 00 76 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0CF_0040_0024_0024CBUPropIdMapEntry_0040MicrosoftZuneInterop_0040_0040 MicrosoftZuneInterop_002E_003FA0x30e7a1fd_002EkPropIdMap/* Not supported: data(C0 23 15 65 00 00 00 00 AC 23 15 65 01 00 00 00 94 23 15 65 02 00 00 00 80 23 15 65 03 00 00 00 6C 23 15 65 04 00 00 00 40 23 15 65 05 00 00 00 30 23 15 65 06 00 00 00 1C 23 15 65 07 00 00 00 08 23 15 65 08 00 00 00 F4 22 15 65 09 00 00 00 DC 22 15 65 0A 00 00 00 CC 22 15 65 0B 00 00 00 B8 22 15 65 0C 00 00 00 A4 22 15 65 0D 00 00 00 90 22 15 65 0E 00 00 00 7C 22 15 65 0F 00 00 00 68 22 15 65 10 00 00 00 54 22 15 65 11 00 00 00 34 22 15 65 12 00 00 00 20 22 15 65 13 00 00 00 18 22 15 65 14 00 00 00 FC 21 15 65 15 00 00 00 E4 21 15 65 16 00 00 00 C0 21 15 65 17 00 00 00 A4 21 15 65 18 00 00 00 80 21 15 65 19 00 00 00 6C 21 15 65 1A 00 00 00 54 21 15 65 1B 00 00 00 3C 21 15 65 1C 00 00 00 2C 21 15 65 1D 00 00 00 14 21 15 65 1E 00 00 00 F4 20 15 65 1F 00 00 00 E0 20 15 65 20 00 00 00 C8 20 15 65 21 00 00 00 AC 20 15 65 22 00 00 00 9C 20 15 65 23 00 00 00 88 20 15 65 24 00 00 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_20 _003F_003F_R2QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00408/* Not supported: data(58 AD 1F 65 B4 AC 1F 65 D0 A2 1F 65 74 AD 1F 65 08 A3 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IQuickMixStatusCallback_0040_00408/* Not supported: data(00 AD 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IQuickMixSessionCallback_0040_00408/* Not supported: data(B4 AC 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R13_003F0A_0040EA_0040IQuickMixStatusCallback_0040_00408/* Not supported: data(F4 11 25 65 01 00 00 00 04 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E4 AC 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00408/* Not supported: data(48 12 25 65 04 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 30 AD 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IQuickMixStatusCallback_0040_00408/* Not supported: data(F4 11 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E4 AC 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IQuickMixSessionCallback_0040_00408/* Not supported: data(B8 11 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 98 AC 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 05 00 00 00 05 00 00 00 40 AD 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_52 _003F_003F_R0_003FAVQuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 51 75 69 63 6B 4D 69 78 43 61 6C 6C 62 61 63 6B 50 72 6F 78 79 40 51 75 69 63 6B 4D 69 78 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IQuickMixStatusCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 F4 AC 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_30 _003F_003F_R0_003FAUIQuickMixStatusCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 51 75 69 63 6B 4D 69 78 53 74 61 74 75 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IQuickMixSessionCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 A8 AC 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_31 _003F_003F_R0_003FAUIQuickMixSessionCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 51 75 69 63 6B 4D 69 78 53 65 73 73 69 6F 6E 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixStatusCallback_0040_0040_0040/* Not supported: data(00 00 00 00 04 00 00 00 00 00 00 00 48 12 25 65 30 AD 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixSessionCallback_0040_0040_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 48 12 25 65 30 AD 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4IQuickMixStatusCallback_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 F4 11 25 65 E4 AC 1F 65) */;

	internal static __s_GUID _GUID_d69e22ae_7e21_4959_be6e_14462eb96f64/* Not supported: data(AE 22 9E D6 21 7E 59 49 BE 6E 14 46 2E B9 6F 64) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7IQuickMixStatusCallback_0040_00406B_0040/* Not supported: data(34 B6 24 65 34 B6 24 65 34 B6 24 65 34 B6 24 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_e76e9fcf_b179_412c_b12e_e1a454c9bfbb/* Not supported: data(CF 9F 6E E7 79 B1 2C 41 B1 2E E1 A4 54 C9 BF BB) */;

	internal static __s_GUID _GUID_588d1e9b_4619_4520_ad4d_f4880b74a506/* Not supported: data(9B 1E 8D 58 19 46 20 45 AD 4D F4 88 0B 74 A5 06) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixStatusCallback_0040_0040_0040/* Not supported: data(88 04 23 65 60 04 23 65 7C 0E 23 65 E3 05 23 65 1C AD 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixSessionCallback_0040_0040_0040/* Not supported: data(2B 04 23 65 36 04 23 65 95 07 23 65 5A 05 23 65 A2 05 23 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2RadioStationProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(D8 AD 1F 65 98 9D 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040RadioStationProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(C8 12 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 B8 AD 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3RadioStationProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 C8 AD 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_44 _003F_003F_R0_003FAVRadioStationProxy_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 52 61 64 69 6F 53 74 61 74 69 6F 6E 50 72 6F 78 79 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4RadioStationProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 C8 12 25 65 B8 AD 1F 65) */;

	internal static __s_GUID _GUID_e1c20902_172d_4c40_bc82_5164f64ab783/* Not supported: data(02 09 C2 E1 2D 17 40 4C BC 82 51 64 F6 4A B7 83) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7RadioStationProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(77 11 23 65 82 11 23 65 03 16 23 65 8C 12 23 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2RecordManagerCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(28 AE 1F 65 44 AE 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IRecordManagerCallback_0040_00408/* Not supported: data(44 AE 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040RecordManagerCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(40 13 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 08 AE 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IRecordManagerCallback_0040_00408/* Not supported: data(7C 13 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 60 AE 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3RecordManagerCallback_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 18 AE 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_49 _003F_003F_R0_003FAVRecordManagerCallback_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 52 65 63 6F 72 64 4D 61 6E 61 67 65 72 43 61 6C 6C 62 61 63 6B 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IRecordManagerCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 70 AE 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_29 _003F_003F_R0_003FAUIRecordManagerCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 52 65 63 6F 72 64 4D 61 6E 61 67 65 72 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4RecordManagerCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 40 13 25 65 08 AE 1F 65) */;

	internal static __s_GUID _GUID_dbb19183_e14e_49cc_a75a_0dbf88f7cc57/* Not supported: data(83 91 B1 DB 4E E1 CC 49 A7 5A 0D BF 88 F7 CC 57) */;

	internal static _0024ArrayType_0024_0024_0024BY08Q6GXXZ _003F_003F_7RecordManagerCallback_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(C1 1B 23 65 CC 1B 23 65 C6 21 23 65 CE 1E 23 65 06 1F 23 65 3E 1F 23 65 77 1F 23 65 AF 1F 23 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2DeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(20 AF 1F 65 98 AE 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2RefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(70 AF 1F 65 D0 AE 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IDeregisterTunerCallback_0040_00408/* Not supported: data(98 AE 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IRefreshTunerListCallback_0040_00408/* Not supported: data(D0 AE 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040DeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(10 14 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 00 AF 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040RefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(70 14 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 50 AF 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IDeregisterTunerCallback_0040_00408/* Not supported: data(A8 13 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 7C AE 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IRefreshTunerListCallback_0040_00408/* Not supported: data(D0 13 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 B4 AE 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3DeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 10 AF 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_54 _003F_003F_R0_003FAVDeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 44 65 72 65 67 69 73 74 65 72 43 61 6C 6C 62 61 63 6B 40 43 6F 6E 66 69 67 75 72 61 74 69 6F 6E 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3RefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 60 AF 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_51 _003F_003F_R0_003FAVRefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 52 65 66 72 65 73 68 43 61 6C 6C 62 61 63 6B 40 43 6F 6E 66 69 67 75 72 61 74 69 6F 6E 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IDeregisterTunerCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 8C AE 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_31 _003F_003F_R0_003FAUIDeregisterTunerCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 44 65 72 65 67 69 73 74 65 72 54 75 6E 65 72 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IRefreshTunerListCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 C4 AE 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_32 _003F_003F_R0_003FAUIRefreshTunerListCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 52 65 66 72 65 73 68 54 75 6E 65 72 4C 69 73 74 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 10 14 25 65 00 AF 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4RefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 70 14 25 65 50 AF 1F 65) */;

	internal static __s_GUID _GUID_0714c405_844f_457c_b141_7664765cb87a/* Not supported: data(05 C4 14 07 4F 84 7C 45 B1 41 76 64 76 5C B8 7A) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7DeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(32 27 23 65 3D 27 23 65 35 2B 23 65 75 2B 23 65 BC 2B 23 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_9e9b9023_b31c_47ed_b609_58361bdae7d3/* Not supported: data(23 90 9B 9E 1C B3 ED 47 B6 09 58 36 1B DA E7 D3) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7RefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(2A 2A 23 65 35 2A 23 65 E9 2A 23 65 9D 2A 23 65 58 2A 23 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0xc57fc1f0_002EWPP_RegisteredDevicesApi_cpp_Traceguids/* Not supported: data(47 96 1D A5 86 E0 77 74 05 F1 70 5B DD 58 2E 15) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0xb8c78a91_002EWPP_SafeBitmap_cpp_Traceguids/* Not supported: data(A9 F2 CC 85 DF A4 A2 42 8C 8B 8E 26 B1 58 ED 15) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2DownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(58 B3 1F 65 F8 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2PurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(08 B3 1F 65 C0 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2AddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(B8 B2 1F 65 88 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2GetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(68 B2 1F 65 50 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2GetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(18 B2 1F 65 18 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2GetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(F8 B3 1F 65 E0 AF 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2GetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(C8 B1 1F 65 A8 AF 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2GetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(A8 B3 1F 65 78 B1 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IGetBillingOffersCallback_0040_00408/* Not supported: data(18 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IAddPaymentInstrumentCallback_0040_00408/* Not supported: data(88 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IGetPaymentInstrumentsCallback_0040_00408/* Not supported: data(50 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IGetBalancesCallback_0040_00408/* Not supported: data(78 B1 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IGetOfferDetailsCallback_0040_00408/* Not supported: data(E0 AF 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IGetOffersCallback_0040_00408/* Not supported: data(A8 AF 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IDownloadCallback_0040_00408/* Not supported: data(F8 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IPurchaseOffersCallback_0040_00408/* Not supported: data(C0 B0 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2_003F_0024DynamicArray_0040PAG_0040_00408/* Not supported: data(40 B1 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040DownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(84 18 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 38 B3 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040PurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(10 18 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E8 B2 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040AddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(A0 17 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 98 B2 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040GetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(28 17 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 48 B2 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040GetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(B8 16 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 F8 B1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040GetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(70 19 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 D8 B3 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040GetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(50 16 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 A8 B1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040GetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 19 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 88 B3 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IGetBillingOffersCallback_0040_00408/* Not supported: data(18 15 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 FC AF 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IAddPaymentInstrumentCallback_0040_00408/* Not supported: data(70 15 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 6C B0 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IGetPaymentInstrumentsCallback_0040_00408/* Not supported: data(40 15 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 34 B0 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IGetBalancesCallback_0040_00408/* Not supported: data(14 16 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 5C B1 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IGetOfferDetailsCallback_0040_00408/* Not supported: data(F0 14 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 C4 AF 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IGetOffersCallback_0040_00408/* Not supported: data(CC 14 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 8C AF 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IDownloadCallback_0040_00408/* Not supported: data(C4 15 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 DC B0 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IPurchaseOffersCallback_0040_00408/* Not supported: data(9C 15 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 A4 B0 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040_003F_0024DynamicArray_0040PAG_0040_00408/* Not supported: data(EC 15 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 28 B1 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3DownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 48 B3 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_53 _003F_003F_R0_003FAVDownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 44 6F 77 6E 6C 6F 61 64 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3PurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 F8 B2 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_56 _003F_003F_R0_003FAVPurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 50 75 72 63 68 61 73 65 4F 66 66 65 72 73 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3AddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 A8 B2 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_65 _003F_003F_R0_003FAVAddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 41 64 64 50 61 79 6D 65 6E 74 49 6E 73 74 72 75 6D 65 6E 74 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3GetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 58 B2 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_66 _003F_003F_R0_003FAVGetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 47 65 74 50 61 79 6D 65 6E 74 49 6E 73 74 72 75 6D 65 6E 74 73 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3GetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 08 B2 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_61 _003F_003F_R0_003FAVGetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 47 65 74 42 69 6C 6C 69 6E 67 4F 66 66 65 72 73 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3GetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 E8 B3 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_60 _003F_003F_R0_003FAVGetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 47 65 74 4F 66 66 65 72 44 65 74 61 69 6C 73 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3GetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 B8 B1 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_54 _003F_003F_R0_003FAVGetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 47 65 74 4F 66 66 65 72 73 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3GetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 98 B3 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_56 _003F_003F_R0_003FAVGetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 47 65 74 42 61 6C 61 6E 63 65 73 43 61 6C 6C 62 61 63 6B 57 72 61 70 70 65 72 40 53 65 72 76 69 63 65 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IGetBillingOffersCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 0C B0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_32 _003F_003F_R0_003FAUIGetBillingOffersCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 47 65 74 42 69 6C 6C 69 6E 67 4F 66 66 65 72 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IAddPaymentInstrumentCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 7C B0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_36 _003F_003F_R0_003FAUIAddPaymentInstrumentCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 41 64 64 50 61 79 6D 65 6E 74 49 6E 73 74 72 75 6D 65 6E 74 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IGetPaymentInstrumentsCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 44 B0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_37 _003F_003F_R0_003FAUIGetPaymentInstrumentsCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 47 65 74 50 61 79 6D 65 6E 74 49 6E 73 74 72 75 6D 65 6E 74 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IGetBalancesCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 6C B1 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_27 _003F_003F_R0_003FAUIGetBalancesCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 47 65 74 42 61 6C 61 6E 63 65 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IGetOfferDetailsCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 D4 AF 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_31 _003F_003F_R0_003FAUIGetOfferDetailsCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 47 65 74 4F 66 66 65 72 44 65 74 61 69 6C 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IGetOffersCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 9C AF 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_25 _003F_003F_R0_003FAUIGetOffersCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 47 65 74 4F 66 66 65 72 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IDownloadCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 EC B0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_24 _003F_003F_R0_003FAUIDownloadCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 44 6F 77 6E 6C 6F 61 64 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IPurchaseOffersCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 B4 B0 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_30 _003F_003F_R0_003FAUIPurchaseOffersCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 50 75 72 63 68 61 73 65 4F 66 66 65 72 73 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3_003F_0024DynamicArray_0040PAG_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 38 B1 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_25 _003F_003F_R0_003FAV_003F_0024DynamicArray_0040PAG_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 3F 24 44 79 6E 61 6D 69 63 41 72 72 61 79 40 50 41 47 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4DownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 84 18 25 65 38 B3 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4PurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 10 18 25 65 E8 B2 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4AddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 A0 17 25 65 98 B2 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4GetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 28 17 25 65 48 B2 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4GetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 B8 16 25 65 F8 B1 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4GetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 70 19 25 65 D8 B3 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4GetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 50 16 25 65 A8 B1 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4GetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 00 19 25 65 88 B3 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4_003F_0024DynamicArray_0040PAG_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 EC 15 25 65 28 B1 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY01Q6GXXZ _003F_003F_7_003F_0024DynamicArray_0040PAG_0040_00406B_0040/* Not supported: data(AE 55 23 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_2644d109_c175_4da3_b051_4085b4254c83/* Not supported: data(09 D1 44 26 75 C1 A3 4D B0 51 40 85 B4 25 4C 83) */;

	internal static _0024ArrayType_0024_0024_0024BY09Q6GXXZ _003F_003F_7DownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(4E 63 23 65 59 63 23 65 A1 86 23 65 01 64 23 65 40 64 23 65 87 64 23 65 D5 64 23 65 85 63 23 65 B1 63 23 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_0629124a_eb99_447e_9537_f10628f23b78/* Not supported: data(4A 12 29 06 99 EB 7E 44 95 37 F1 06 28 F2 3B 78) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7PurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(BE 61 23 65 C9 61 23 65 D9 85 23 65 1E 62 23 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7AddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(3A 60 23 65 45 60 23 65 91 85 23 65 C5 60 23 65 0A 61 23 65 00 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7GetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(6D 5E 23 65 78 5E 23 65 C8 84 23 65 C2 5E 23 65 0A 5F 23 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7GetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(A5 5C 23 65 B0 5C 23 65 F0 83 23 65 FA 5C 23 65 42 5D 23 65 00 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7GetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(99 82 23 65 A4 82 23 65 D1 BB 23 65 E0 BF 23 65 16 83 23 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7GetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(E9 59 23 65 F4 59 23 65 6D 81 23 65 D9 BA 23 65 3A 5A 23 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7GetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(59 7F 23 65 64 7F 23 65 A4 80 23 65 A3 7F 23 65 EA 7F 23 65 00 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY05_0024_0024CBG _003F_003F_C_0040_1M_0040MNHBCACD_0040_003F_0024AAT_003F_0024AAi_003F_0024AAt_003F_0024AAl_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(54 00 69 00 74 00 6C 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0M_0040_0024_0024CBG _003F_003F_C_0040_1BI_0040DLMANABL_0040_003F_0024AAD_003F_0024AAe_003F_0024AAs_003F_0024AAc_003F_0024AAr_003F_0024AAi_003F_0024AAp_003F_0024AAt_003F_0024AAi_003F_0024AAo_003F_0024AAn_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 65 00 73 00 63 00 72 00 69 00 70 00 74 00 69 00 6F 00 6E 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040CGFFANJJ_0040_003F_0024AAD_003F_0024AAu_003F_0024AAr_003F_0024AAa_003F_0024AAt_003F_0024AAi_003F_0024AAo_003F_0024AAn_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 75 00 72 00 61 00 74 00 69 00 6F 00 6E 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0M_0040_0024_0024CBG _003F_003F_C_0040_1BI_0040IMGEIAFE_0040_003F_0024AAR_003F_0024AAe_003F_0024AAl_003F_0024AAe_003F_0024AAa_003F_0024AAs_003F_0024AAe_003F_0024AAD_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(52 00 65 00 6C 00 65 00 61 00 73 00 65 00 44 00 61 00 74 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY08_0024_0024CBG _003F_003F_C_0040_1BC_0040BFOBHOBE_0040_003F_0024AAE_003F_0024AAx_003F_0024AAp_003F_0024AAl_003F_0024AAi_003F_0024AAc_003F_0024AAi_003F_0024AAt_003F_0024AA_003F_0024AA_0040/* Not supported: data(45 00 78 00 70 00 6C 00 69 00 63 00 69 00 74 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY06_0024_0024CBG _003F_003F_C_0040_1O_0040IKCDCNCP_0040_003F_0024AAA_003F_0024AAu_003F_0024AAt_003F_0024AAh_003F_0024AAo_003F_0024AAr_003F_0024AA_003F_0024AA_0040/* Not supported: data(41 00 75 00 74 00 68 00 6F 00 72 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040DIFENCED_0040_003F_0024AAE_003F_0024AAn_003F_0024AAc_003F_0024AAl_003F_0024AAo_003F_0024AAs_003F_0024AAu_003F_0024AAr_003F_0024AAe_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040/* Not supported: data(45 00 6E 00 63 00 6C 00 6F 00 73 00 75 00 72 00 65 00 55 00 72 00 6C 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BB_0040_0024_0024CBG _003F_003F_C_0040_1CC_0040PFACMMFM_0040_003F_0024AAE_003F_0024AAp_003F_0024AAi_003F_0024AAs_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AAM_003F_0024AAe_003F_0024AAd_003F_0024AAi_003F_0024AAa_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(45 00 70 00 69 00 73 00 6F 00 64 00 65 00 4D 00 65 00 64 00 69 00 61 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040NCILDCLH_0040_003F_0024AAL_003F_0024AAi_003F_0024AAb_003F_0024AAr_003F_0024AAa_003F_0024AAr_003F_0024AAy_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040/* Not supported: data(4C 00 69 00 62 00 72 00 61 00 72 00 79 00 49 00 64 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0O_0040_0024_0024CBG _003F_003F_C_0040_1BM_0040BGELMDBM_0040_003F_0024AAD_003F_0024AAo_003F_0024AAw_003F_0024AAn_003F_0024AAl_003F_0024AAo_003F_0024AAa_003F_0024AAd_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 6F 00 77 00 6E 00 6C 00 6F 00 61 00 64 00 53 00 74 00 61 00 74 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040EPGEAFPP_0040_003F_0024AAD_003F_0024AAo_003F_0024AAw_003F_0024AAn_003F_0024AAl_003F_0024AAo_003F_0024AAa_003F_0024AAd_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 6F 00 77 00 6E 00 6C 00 6F 00 61 00 64 00 54 00 79 00 70 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040JJCAOHOO_0040_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAd_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAu_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(50 00 6C 00 61 00 79 00 65 00 64 00 53 00 74 00 61 00 74 00 75 00 73 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040HHOJDPEL_0040_003F_0024AAS_003F_0024AAo_003F_0024AAu_003F_0024AAr_003F_0024AAc_003F_0024AAe_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 6F 00 75 00 72 00 63 00 65 00 55 00 72 00 6C 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BC_0040_0024_0024CBG _003F_003F_C_0040_1CE_0040ELHJHONN_0040_003F_0024AAD_003F_0024AAo_003F_0024AAw_003F_0024AAn_003F_0024AAl_003F_0024AAo_003F_0024AAa_003F_0024AAd_003F_0024AAE_003F_0024AAr_003F_0024AAr_003F_0024AAo_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(44 00 6F 00 77 00 6E 00 6C 00 6F 00 61 00 64 00 45 00 72 00 72 00 6F 00 72 00 43 00 6F 00 64 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBG _003F_003F_C_0040_1BA_0040CEPEGMDB_0040_003F_0024AAF_003F_0024AAe_003F_0024AAe_003F_0024AAd_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040/* Not supported: data(46 00 65 00 65 00 64 00 55 00 72 00 6C 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040ILMBBNLH_0040_003F_0024AAE_003F_0024AAr_003F_0024AAr_003F_0024AAo_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(45 00 72 00 72 00 6F 00 72 00 43 00 6F 00 64 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBG _003F_003F_C_0040_1BA_0040DPIPFNMB_0040_003F_0024AAH_003F_0024AAo_003F_0024AAm_003F_0024AAe_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040/* Not supported: data(48 00 6F 00 6D 00 65 00 55 00 72 00 6C 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY06_0024_0024CBG _003F_003F_C_0040_1O_0040OPPNLDOF_0040_003F_0024AAA_003F_0024AAr_003F_0024AAt_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040/* Not supported: data(41 00 72 00 74 00 55 00 72 00 6C 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040NPMHDJAF_0040_003F_0024AAC_003F_0024AAo_003F_0024AAp_003F_0024AAy_003F_0024AAr_003F_0024AAi_003F_0024AAg_003F_0024AAh_003F_0024AAt_003F_0024AA_003F_0024AA_0040/* Not supported: data(43 00 6F 00 70 00 79 00 72 00 69 00 67 00 68 00 74 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY09_0024_0024CBG _003F_003F_C_0040_1BE_0040GMGHCDOJ_0040_003F_0024AAO_003F_0024AAw_003F_0024AAn_003F_0024AAe_003F_0024AAr_003F_0024AAN_003F_0024AAa_003F_0024AAm_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(4F 00 77 00 6E 00 65 00 72 00 4E 00 61 00 6D 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0M_0040_0024_0024CBG _003F_003F_C_0040_1BI_0040KPFACBGA_0040_003F_0024AAS_003F_0024AAe_003F_0024AAr_003F_0024AAi_003F_0024AAe_003F_0024AAs_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AA_003F_0024AA_0040/* Not supported: data(53 00 65 00 72 00 69 00 65 00 73 00 53 00 74 00 61 00 74 00 65 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BB_0040_0024_0024CBG _003F_003F_C_0040_1CC_0040GKFOINAA_0040_003F_0024AAN_003F_0024AAu_003F_0024AAm_003F_0024AAb_003F_0024AAe_003F_0024AAr_003F_0024AAO_003F_0024AAf_003F_0024AAE_003F_0024AAp_003F_0024AAi_003F_0024AAs_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AAs_003F_0024AA_003F_0024AA_0040/* Not supported: data(4E 00 75 00 6D 00 62 00 65 00 72 00 4F 00 66 00 45 00 70 00 69 00 73 00 6F 00 64 00 65 00 73 00 00 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2VirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(80 B4 1F 65 30 B4 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2ISubscriptionViewerCallback_0040_00408/* Not supported: data(30 B4 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040VirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(30 1A 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 60 B4 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040ISubscriptionViewerCallback_0040_00408/* Not supported: data(E8 19 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 14 B4 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3VirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 70 B4 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_70 _003F_003F_R0_003FAVVirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 56 69 72 74 75 61 6C 53 75 62 73 63 72 69 70 74 69 6F 6E 45 70 69 73 6F 64 65 4C 69 73 74 50 72 6F 78 79 40 53 75 62 73 63 72 69 70 74 69 6F 6E 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3ISubscriptionViewerCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 24 B4 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_34 _003F_003F_R0_003FAUISubscriptionViewerCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 53 75 62 73 63 72 69 70 74 69 6F 6E 56 69 65 77 65 72 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4VirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 30 1A 25 65 60 B4 1F 65) */;

	internal static __s_GUID _GUID_cd97f5a6_9e6e_47a1_9b64_888484f91fc7/* Not supported: data(A6 F5 97 CD 6E 9E A1 47 9B 64 88 84 84 F9 1F C7) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7VirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(96 CF 23 65 A1 CF 23 65 AF D7 23 65 22 D6 23 65 DD E1 23 65 71 E2 23 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_9dc7c984_41d5_4130_a5ac_46d0825cd29d/* Not supported: data(84 C9 C7 9D D5 41 30 41 A5 AC 46 D0 82 5C D2 9D) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CSubscriptionCredentialProviderProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(58 B5 1F 65 B8 B4 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(08 B5 1F 65 98 9F 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2ISubscriptionManagerCredentialProvider_0040_00408/* Not supported: data(B8 B4 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CSubscriptionCredentialProviderProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(50 1B 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 38 B5 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(F8 1A 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 E8 B4 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040ISubscriptionManagerCredentialProvider_0040_00408/* Not supported: data(9C 1A 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 9C B4 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CSubscriptionCredentialProviderProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 48 B5 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_71 _003F_003F_R0_003FAVCSubscriptionCredentialProviderProxy_0040Subscription_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 53 75 62 73 63 72 69 70 74 69 6F 6E 43 72 65 64 65 6E 74 69 61 6C 50 72 6F 76 69 64 65 72 50 72 6F 78 79 40 53 75 62 73 63 72 69 70 74 69 6F 6E 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 F8 B4 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_58 _003F_003F_R0_003FAVCSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 53 75 62 73 63 72 69 70 74 69 6F 6E 45 76 65 6E 74 50 72 6F 78 79 40 53 75 62 73 63 72 69 70 74 69 6F 6E 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3ISubscriptionManagerCredentialProvider_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 AC B4 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_45 _003F_003F_R0_003FAUISubscriptionManagerCredentialProvider_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 53 75 62 73 63 72 69 70 74 69 6F 6E 4D 61 6E 61 67 65 72 43 72 65 64 65 6E 74 69 61 6C 50 72 6F 76 69 64 65 72 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CSubscriptionCredentialProviderProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 50 1B 25 65 38 B5 1F 65) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 F8 1A 25 65 E8 B4 1F 65) */;

	internal static __s_GUID _GUID_c1764920_30cf_4e56_81db_202d03556cb9/* Not supported: data(20 49 76 C1 CF 30 56 4E 81 DB 20 2D 03 55 6C B9) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7CSubscriptionCredentialProviderProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(FE E5 23 65 09 E6 23 65 AB F7 23 65 E5 EA 23 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7CSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(0E E4 23 65 19 E4 23 65 D7 F1 23 65 93 E3 23 65 71 F7 23 65 00 00 00 00) */;

	internal static __s_GUID _GUID_f4f0a85f_d136_46d4_ab5d_950d93006ae2/* Not supported: data(5F A8 F0 F4 36 D1 D4 46 AB 5D 95 0D 93 00 6A E2) */;

	internal static __s_GUID _GUID_b12dc962_cc1b_46c5_a92a_68f1f2b9bff3/* Not supported: data(62 C9 2D B1 1B CC C5 46 A9 2A 68 F1 F2 B9 BF F3) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids/* Not supported: data(A9 6A 59 A7 0D EA 2A 29 18 A2 C2 1B 4A 53 12 66) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2SyncRulesViewMediator_0040_00408/* Not supported: data(E0 B5 1F 65 90 B5 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2ISyncRulesViewCallback_0040_00408/* Not supported: data(90 B5 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040SyncRulesViewMediator_0040_00408/* Not supported: data(00 1C 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 C0 B5 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040ISyncRulesViewCallback_0040_00408/* Not supported: data(B8 1B 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 74 B5 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3SyncRulesViewMediator_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 D0 B5 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAVSyncRulesViewMediator_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 53 79 6E 63 52 75 6C 65 73 56 69 65 77 4D 65 64 69 61 74 6F 72 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3ISyncRulesViewCallback_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 84 B5 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_29 _003F_003F_R0_003FAUISyncRulesViewCallback_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 53 79 6E 63 52 75 6C 65 73 56 69 65 77 43 61 6C 6C 62 61 63 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4SyncRulesViewMediator_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 00 1C 25 65 C0 B5 1F 65) */;

	internal static __s_GUID _GUID_8c6c709a_8ed8_4dc2_a530_b206f30497c9/* Not supported: data(9A 70 6C 8C D8 8E C2 4D A5 30 B2 06 F3 04 97 C9) */;

	internal static _0024ArrayType_0024_0024_0024BY05Q6GXXZ _003F_003F_7SyncRulesViewMediator_0040_00406B_0040/* Not supported: data(65 0D 24 65 70 0D 24 65 F6 0F 24 65 32 0E 24 65 6A 0E 24 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY0BK_0040_0024_0024CBG _003F_003F_C_0040_1DE_0040LJLIMGOK_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AAM_003F_0024AAs_003F_0024AAg_003F_0024AA_003F_0024AA_0040/* Not supported: data(5A 00 75 00 6E 00 65 00 54 00 61 00 73 00 6B 00 62 00 61 00 72 00 50 00 6C 00 61 00 79 00 65 00 72 00 53 00 74 00 61 00 74 00 65 00 4D 00 73 00 67 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0BM_0040_0024_0024CBG _003F_003F_C_0040_1DI_0040BOCHIFKJ_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAm_003F_0024AAm_003F_0024AAa_003F_0024AAn_003F_0024AAd_003F_0024AAM_003F_0024AAs_003F_0024AAg_003F_0024AA_003F_0024AA_0040/* Not supported: data(5A 00 75 00 6E 00 65 00 54 00 61 00 73 00 6B 00 62 00 61 00 72 00 50 00 6C 00 61 00 79 00 65 00 72 00 43 00 6F 00 6D 00 6D 00 61 00 6E 00 64 00 4D 00 73 00 67 00 00 00) */;

	internal static _0024ArrayType_0024_0024_0024BY0CD_0040_0024_0024CBG _003F_003F_C_0040_1EG_0040LILHHEEP_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAm_003F_0024AAm_003F_0024AAa_003F_0024AAn_003F_0024AAd_003F_0024AAD_003F_0024AAi_003F_0024AAs_003F_0024AAp_003F_0024AAa_003F_0024AAt_003F_0024AAc_003F_0024AAh_0040/* Not supported: data(5A 00 75 00 6E 00 65 00 54 00 61 00 73 00 6B 00 62 00 61 00 72 00 50 00 6C 00 61 00 79 00 65 00 72 00 43 00 6F 00 6D 00 6D 00 61 00 6E 00 64 00 44 00 69 00 73 00 70 00 61 00 74 00 63 00 68 00 65 00 72 00 00 00) */;

	internal static __s_GUID _GUID_f2d3efa4_12f4_466b_a41c_d9ec613ad509/* Not supported: data(A4 EF D3 F2 F4 12 6B 46 A4 1C D9 EC 61 3A D5 09) */;

	internal static CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand/*Field data (rva=0x103d30) could not be found in any section!*/;

	internal unsafe static delegate*<void> Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand_0024initializer_0024/* Not supported: data(61 03 00 06) */;

	internal static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E Microsoft_002EZune_002EUtil_002Esm_gcTaskbarPlayer/*Field data (rva=0x103d2c) could not be found in any section!*/;

	internal unsafe static delegate*<void> Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Esm_gcTaskbarPlayer_0024initializer_0024/* Not supported: data(5F 03 00 06) */;

	public unsafe static int** __unep_0040_003FCommandDispatcherWindowProc_0040Util_0040Zune_0040Microsoft_0040_0040_0024_0024FYGJPAUHWND___0040_0040IIJ_0040Z/* Not supported: data(B6 1B 24 65) */;

	internal static __s_GUID _GUID_ab28333b_a55c_4312_a7a3_2dd60d4a7154/* Not supported: data(3B 33 28 AB 5C A5 12 43 A7 A3 2D D6 0D 4A 71 54) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2UpdateProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(68 B6 1F 65 18 B6 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IUpdateProgress_0040_00408/* Not supported: data(18 B6 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040UpdateProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(7C 1C 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 48 B6 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IUpdateProgress_0040_00408/* Not supported: data(38 1C 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 FC B5 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3UpdateProxy_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 58 B6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_38 _003F_003F_R0_003FAVUpdateProxy_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 55 70 64 61 74 65 50 72 6F 78 79 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IUpdateProgress_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 0C B6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_22 _003F_003F_R0_003FAUIUpdateProgress_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 55 70 64 61 74 65 50 72 6F 67 72 65 73 73 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4UpdateProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 7C 1C 25 65 48 B6 1F 65) */;

	internal static __s_GUID _GUID_9d21716a_ca61_4e24_a1ba_47b9e70e1e2c/* Not supported: data(6A 71 21 9D 61 CA 24 4E A1 BA 47 B9 E7 0E 1E 2C) */;

	internal static __s_GUID _GUID_4ae247ea_52dd_46b7_acd4_7127b1054eef/* Not supported: data(EA 47 E2 4A DD 52 B7 46 AC D4 71 27 B1 05 4E EF) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7UpdateProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(73 25 24 65 7E 25 24 65 DB 2B 24 65 FD 24 24 65 AD 26 24 65 E1 26 24 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_ddbb9148_dea1_47dd_a0c1_1fdcf002c1e2/* Not supported: data(48 91 BB DD A1 DE DD 47 A0 C1 1F DC F0 02 C1 E2) */;

	internal static __s_GUID _GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a/* Not supported: data(09 E7 36 2F 31 C4 36 48 AB 2B AB 57 AE F0 CF 1A) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2CUserCredentialProviderProxy_0040UserCredential_0040Zune_0040Microsoft_0040_00408/* Not supported: data(F0 B6 1F 65 A0 B6 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IUserCredentialManagerProvider_0040_00408/* Not supported: data(A0 B6 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040CUserCredentialProviderProxy_0040UserCredential_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 1D 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 D0 B6 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IUserCredentialManagerProvider_0040_00408/* Not supported: data(B8 1C 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 84 B6 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3CUserCredentialProviderProxy_0040UserCredential_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 E0 B6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_65 _003F_003F_R0_003FAVCUserCredentialProviderProxy_0040UserCredential_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 43 55 73 65 72 43 72 65 64 65 6E 74 69 61 6C 50 72 6F 76 69 64 65 72 50 72 6F 78 79 40 55 73 65 72 43 72 65 64 65 6E 74 69 61 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IUserCredentialManagerProvider_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 94 B6 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_37 _003F_003F_R0_003FAUIUserCredentialManagerProvider_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 55 73 65 72 43 72 65 64 65 6E 74 69 61 6C 4D 61 6E 61 67 65 72 50 72 6F 76 69 64 65 72 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4CUserCredentialProviderProxy_0040UserCredential_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 00 1D 25 65 D0 B6 1F 65) */;

	internal static __s_GUID _GUID_41c80590_c50b_4d27_b860_7c87f3f0cb54/* Not supported: data(90 05 C8 41 0B C5 27 4D B8 60 7C 87 F3 F0 CB 54) */;

	internal static __s_GUID _GUID_dafb3bf7_33d7_495c_9855_829248b88ba6/* Not supported: data(F7 3B FB DA D7 33 5C 49 98 55 82 92 48 B8 8B A6) */;

	internal static _0024ArrayType_0024_0024_0024BY04Q6GXXZ _003F_003F_7CUserCredentialProviderProxy_0040UserCredential_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(6E 33 24 65 79 33 24 65 0F 37 24 65 35 35 24 65 2C 2C 15 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2_003F_0024DynamicArray_0040H_0040_00408/* Not supported: data(38 B7 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040_003F_0024DynamicArray_0040H_0040_00408/* Not supported: data(58 1D 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 20 B7 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3_003F_0024DynamicArray_0040H_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 30 B7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_23 _003F_003F_R0_003FAV_003F_0024DynamicArray_0040H_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 3F 24 44 79 6E 61 6D 69 63 41 72 72 61 79 40 48 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4_003F_0024DynamicArray_0040H_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 58 1D 25 65 20 B7 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY01Q6GXXZ _003F_003F_7_003F_0024DynamicArray_0040H_0040_00406B_0040/* Not supported: data(7A 38 24 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_c9e0f18a_6c53_47d0_991e_dbd4fe395101/* Not supported: data(8A F1 E0 C9 53 6C D0 47 99 1E DB D4 FE 39 51 01) */;

	internal static _0024ArrayType_0024_0024_0024BY0BB_0040_0024_0024CBG _003F_003F_C_0040_1CC_0040NMGFJFON_0040_003F_0024AAM_003F_0024AAs_003F_0024AAn_003F_0024AAM_003F_0024AAs_003F_0024AAg_003F_0024AAr_003F_0024AAU_003F_0024AAI_003F_0024AAM_003F_0024AAa_003F_0024AAn_003F_0024AAa_003F_0024AAg_003F_0024AAe_003F_0024AAr_003F_0024AA_003F_0024AA_0040/* Not supported: data(4D 00 73 00 6E 00 4D 00 73 00 67 00 72 00 55 00 49 00 4D 00 61 00 6E 00 61 00 67 00 65 00 72 00 00 00) */;

	internal static bool _003FA0x7bb9ee51_002Es_bIsLonghornOrBetter/*Field data (rva=0x104981) could not be found in any section!*/;

	internal static bool _003FA0x7bb9ee51_002Es_bIsLonghornOrBetterInitialized/*Field data (rva=0x104980) could not be found in any section!*/;

	internal static __s_GUID _GUID_3e87c005_24d9_4446_a1b9_2c062230f2c5/* Not supported: data(05 C0 87 3E D9 24 46 44 A1 B9 2C 06 22 30 F2 C5) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2Win7ShellManagerMediator_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(C0 B7 1F 65 70 B7 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IWin7ShellManagerMediator_0040_00408/* Not supported: data(70 B7 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040Win7ShellManagerMediator_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(D4 1D 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 A0 B7 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IWin7ShellManagerMediator_0040_00408/* Not supported: data(84 1D 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 54 B7 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3Win7ShellManagerMediator_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 B0 B7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_51 _003F_003F_R0_003FAVWin7ShellManagerMediator_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 57 69 6E 37 53 68 65 6C 6C 4D 61 6E 61 67 65 72 4D 65 64 69 61 74 6F 72 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IWin7ShellManagerMediator_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 64 B7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_32 _003F_003F_R0_003FAUIWin7ShellManagerMediator_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 57 69 6E 37 53 68 65 6C 6C 4D 61 6E 61 67 65 72 4D 65 64 69 61 74 6F 72 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4Win7ShellManagerMediator_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 D4 1D 25 65 A0 B7 1F 65) */;

	internal static __s_GUID _GUID_3a6ee87c_36d0_4535_a664_6cc41f5029e6/* Not supported: data(7C E8 6E 3A D0 36 35 45 A6 64 6C C4 1F 50 29 E6) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7Win7ShellManagerMediator_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(C9 5F 24 65 D4 5F 24 65 A2 65 24 65 34 61 24 65 60 61 24 65 8B 61 24 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_e24c5c6a_85a5_440e_93e1_bb51e32033ac/* Not supported: data(6A 5C 4C E2 A5 85 0E 44 93 E1 BB 51 E3 20 33 AC) */;

	internal static __s_GUID _GUID_a89c52eb_97a9_417b_9872_46c040f1b76f/* Not supported: data(EB 52 9C A8 A9 97 7B 41 98 72 46 C0 40 F1 B7 6F) */;

	internal static _0024ArrayType_0024_0024_0024BY0N_0040_0024_0024CBG _003F_003F_C_0040_1BK_0040DJDIBCLF_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AA_003F5_003F_0024AAI_003F_0024AAn_003F_0024AAt_003F_0024AAe_003F_0024AAr_003F_0024AAo_003F_0024AAp_003F_0024AA_003F_0024AA_0040/* Not supported: data(5A 00 75 00 6E 00 65 00 20 00 49 00 6E 00 74 00 65 00 72 00 6F 00 70 00 00 00) */;

	internal static __s_GUID _GUID_c1cad55a_5652_40c7_842c_39cbe209379e/* Not supported: data(5A D5 CA C1 52 56 C7 40 84 2C 39 CB E2 09 37 9E) */;

	internal static _0024ArrayType_0024_0024_0024BY07TWPP_PROJECT_CONTROL_BLOCK_0040_0040 WPP_MAIN_CB/*Field data (rva=0x104e10) could not be found in any section!*/;

	internal static _0024ArrayType_0024_0024_0024BY07PBU_GUID_0040_0040 WPP_REGISTRATION_GUIDS/*Field data (rva=0x104f10) could not be found in any section!*/;

	public unsafe static int** __unep_0040_003FWppControlCallback_0040_0040_0024_0024J216YGKW4WMIDPREQUESTCODE_0040_0040PAXPAK1_0040Z/* Not supported: data(B1 6D 24 65) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2ResultSetEventRelay_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(40 B8 1F 65 F4 B7 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_4 _003F_003F_R2IResultSetEvents_0040_00408/* Not supported: data(F4 B7 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040ResultSetEventRelay_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(64 1E 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 24 B8 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IResultSetEvents_0040_00408/* Not supported: data(18 1E 25 65 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 DC B7 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3ResultSetEventRelay_0040MicrosoftZuneLibrary_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 34 B8 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_47 _003F_003F_R0_003FAVResultSetEventRelay_0040MicrosoftZuneLibrary_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 52 65 73 75 6C 74 53 65 74 45 76 65 6E 74 52 65 6C 61 79 40 4D 69 63 72 6F 73 6F 66 74 5A 75 6E 65 4C 69 62 72 61 72 79 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IResultSetEvents_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 01 00 00 00 EC B7 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_23 _003F_003F_R0_003FAUIResultSetEvents_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 52 65 73 75 6C 74 53 65 74 45 76 65 6E 74 73 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4ResultSetEventRelay_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 64 1E 25 65 24 B8 1F 65) */;

	internal static _0024ArrayType_0024_0024_0024BY08Q6GXXZ _003F_003F_7ResultSetEventRelay_0040MicrosoftZuneLibrary_0040_00406B_0040/* Not supported: data(D9 8F 24 65 71 90 24 65 01 91 24 65 91 91 24 65 27 8A 24 65 21 92 24 65 4B 8A 24 65 B1 92 24 65 2C 2C 15 65) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0x037fed32_002EWPP_ZuneDBList_cpp_Traceguids/* Not supported: data(E2 54 AC A0 51 06 27 CE FA F6 ED 6B 38 79 36 B4) */;

	internal static _0024ArrayType_0024_0024_0024BY04_0024_0024CBD _003F_003F_C_0040_04HIBGFPH_0040NULL_003F_0024AA_0040/* Not supported: data(4E 55 4C 4C 00) */;

	internal static _0024ArrayType_0024_0024_0024BY07_0024_0024CBD _003F_003F_C_0040_07PPOLEBIF_0040_003F5ERROR_003F3_003F_0024AA_0040/* Not supported: data(20 45 52 52 4F 52 3A 00) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBD _003F_003F_C_0040_00CNPNBAHC_0040_003F_0024AA_0040/* Not supported: data(00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_12 _003F_003F_R2ZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(C8 B8 1F 65 78 B8 1F 65 C4 9C 1F 65 00) */;

	internal static _0024_s__RTTIBaseClassArray_0024_extraBytes_8 _003F_003F_R2IZuneWebHostEventSink_0040_00408/* Not supported: data(78 B8 1F 65 C4 9C 1F 65 00) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040ZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 1F 25 65 02 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 A8 B8 1F 65) */;

	internal static _s__RTTIBaseClassDescriptor2 _003F_003F_R1A_0040_003F0A_0040EA_0040IZuneWebHostEventSink_0040_00408/* Not supported: data(C0 1E 25 65 01 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 40 00 00 00 5C B8 1F 65) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3ZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 03 00 00 00 B8 B8 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_47 _003F_003F_R0_003FAVZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 56 5A 75 6E 65 57 65 62 48 6F 73 74 45 76 65 6E 74 53 69 6E 6B 40 55 74 69 6C 40 5A 75 6E 65 40 4D 69 63 72 6F 73 6F 66 74 40 40 00) */;

	internal static _s__RTTIClassHierarchyDescriptor _003F_003F_R3IZuneWebHostEventSink_0040_00408/* Not supported: data(00 00 00 00 00 00 00 00 02 00 00 00 6C B8 1F 65) */;

	internal static _0024_TypeDescriptor_0024_extraBytes_28 _003F_003F_R0_003FAUIZuneWebHostEventSink_0040_0040_00408/* Not supported: data(2C 2C 15 65 00 00 00 00 2E 3F 41 55 49 5A 75 6E 65 57 65 62 48 6F 73 74 45 76 65 6E 74 53 69 6E 6B 40 40 00) */;

	internal static _s__RTTICompleteObjectLocator _003F_003F_R4ZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 00 1F 25 65 A8 B8 1F 65) */;

	internal static __s_GUID _GUID_2da5365a_229c_4dc9_a33e_47960794e4f9/* Not supported: data(5A 36 A5 2D 9C 22 C9 4D A3 3E 47 96 07 94 E4 F9) */;

	internal static _0024ArrayType_0024_0024_0024BY06Q6GXXZ _003F_003F_7ZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_00406B_0040/* Not supported: data(09 9D 24 65 14 9D 24 65 50 9D 24 65 89 9E 24 65 3A 9F 24 65 9E A0 24 65 2C 2C 15 65) */;

	internal static __s_GUID _GUID_51005f8f_675e_45e1_ae94_8edef996a02e/* Not supported: data(8F 5F 00 51 5E 67 E1 45 AE 94 8E DE F9 96 A0 2E) */;

	internal static _0024ArrayType_0024_0024_0024BY00_0024_0024CBU_GUID_0040_0040 _003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids/* Not supported: data(ED ED 9C D5 9D A7 BC E5 DD 5C A6 59 2A 17 B3 9E) */;

	internal static _0024ArrayType_0024_0024_0024BY00Q6MPBXXZ _003FA0x250abb45_002E__xc_mp_z/* Not supported: data(00 00 00 00) */;

	[FixedAddressValueType]
	internal static int _003FUninitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA;

	internal unsafe static delegate*<void> _003FA0x250abb45_002E_003FUninitialized_0024initializer_0024_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2P6MXXZA/* Not supported: data(E8 03 00 06) */;

	internal static _0024ArrayType_0024_0024_0024BY00Q6MPBXXZ _003FA0x250abb45_002E__xi_vt_a/* Not supported: data(00 00 00 00) */;

	[FixedAddressValueType]
	internal static Progress.State _003FInitializedPerAppDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A;

	internal unsafe static delegate*<void> _003FA0x250abb45_002E_003FInitializedPerAppDomain_0024initializer_0024_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2P6MXXZA/* Not supported: data(ED 03 00 06) */;

	[FixedAddressValueType]
	internal static bool _003FIsDefaultDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2_NA;

	internal unsafe static delegate*<void> _003FA0x250abb45_002E_003FIsDefaultDomain_0024initializer_0024_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2P6MXXZA/* Not supported: data(E9 03 00 06) */;

	internal static _0024ArrayType_0024_0024_0024BY00Q6MPBXXZ _003FA0x250abb45_002E__xc_ma_a/* Not supported: data(00 00 00 00) */;

	[FixedAddressValueType]
	internal static Progress.State _003FInitializedNative_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A;

	internal unsafe static delegate*<void> _003FA0x250abb45_002E_003FInitializedNative_0024initializer_0024_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2P6MXXZA/* Not supported: data(EB 03 00 06) */;

	[FixedAddressValueType]
	internal static int _003FInitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA;

	internal unsafe static delegate*<void> _003FA0x250abb45_002E_003FInitialized_0024initializer_0024_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2P6MXXZA/* Not supported: data(E7 03 00 06) */;

	internal static _0024ArrayType_0024_0024_0024BY00Q6MPBXXZ _003FA0x250abb45_002E__xc_ma_z/* Not supported: data(00 00 00 00) */;

	[FixedAddressValueType]
	internal static Progress.State _003FInitializedVtables_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A;

	internal unsafe static delegate*<void> _003FA0x250abb45_002E_003FInitializedVtables_0024initializer_0024_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2P6MXXZA/* Not supported: data(EA 03 00 06) */;

	internal static _0024ArrayType_0024_0024_0024BY00Q6MPBXXZ _003FA0x250abb45_002E__xi_vt_z/* Not supported: data(00 00 00 00) */;

	[FixedAddressValueType]
	internal static Progress.State _003FInitializedPerProcess_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A;

	internal unsafe static delegate*<void> _003FA0x250abb45_002E_003FInitializedPerProcess_0024initializer_0024_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2P6MXXZA/* Not supported: data(EC 03 00 06) */;

	internal static bool _003FInitializedPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA/*Field data (rva=0x1058bf) could not be found in any section!*/;

	internal static bool _003FEntered_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA/*Field data (rva=0x1058bc) could not be found in any section!*/;

	internal static bool _003FInitializedNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA/*Field data (rva=0x1058bd) could not be found in any section!*/;

	internal static int _003FCount_0040AllDomains_0040_003CCrtImplementationDetails_003E_0040_00402HA/*Field data (rva=0x1058b8) could not be found in any section!*/;

	internal static TriBool.State _003FhasNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A/* Not supported: data() */;

	internal static TriBool.State _003FhasPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A/* Not supported: data() */;

	internal static bool _003FInitializedNativeFromCCTOR_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA/*Field data (rva=0x1058be) could not be found in any section!*/;

	internal static _0024ArrayType_0024_0024_0024BY00Q6MPBXXZ _003FA0x250abb45_002E__xc_mp_a/* Not supported: data(00 00 00 00) */;

	public unsafe static int** __unep_0040_003FDoNothing_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024FCGJPAX_0040Z/* Not supported: data(F2 A2 24 65) */;

	public unsafe static int** __unep_0040_003F_UninitializeDefaultDomain_0040LanguageSupport_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024FCGJPAX_0040Z/* Not supported: data(92 A4 24 65) */;

	[FixedAddressValueType]
	internal static uint __exit_list_size_app_domain;

	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitbegin_app_domain;

	internal static uint _003FA0x11773762_002E__exit_list_size/*Field data (rva=0x1059ec) could not be found in any section!*/;

	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitend_app_domain;

	internal unsafe static delegate*<void>* _003FA0x11773762_002E__onexitbegin_m/*Field data (rva=0x1059e4) could not be found in any section!*/;

	internal unsafe static delegate*<void>* _003FA0x11773762_002E__onexitend_m/*Field data (rva=0x1059e8) could not be found in any section!*/;

	[FixedAddressValueType]
	internal unsafe static void* _003F_lock_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0PAXA;

	[FixedAddressValueType]
	internal static int _003F_ref_count_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0HA;

	public static _0024ArrayType_0024_0024_0024BY01Q6GXXZ _003F_003F_7type_info_0040_00406B_0040/* Not supported: data(E3 B5 24 65 20 5A 25 65) */;

	public static _GUID GUID_NULL/* Not supported: data(00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00) */;

	public static _GUID IID_IUnknown/* Not supported: data(00 00 00 00 00 00 00 00 C0 00 00 00 00 00 00 46) */;

	public static _GUID CLSID_MsftDiscMaster2/* Not supported: data(2E 41 35 27 64 7F 0F 5B 8F 00 5D 77 AF BE 26 1E) */;

	public static _0024ArrayType_0024_0024_0024BY0A_0040_0024_0024CBU_SCHEMAMAPENTRY_0040CSchemaMap_0040_0040 _003Fs_rgSchemaMapEntry_0040CSchemaMap_0040_00400QBU_SCHEMAMAPENTRY_00401_0040B/* Not supported: data(1C) */;

	public static EtwControlerState g_EtwControlerState/*Field data (rva=0x105d48) could not be found in any section!*/;

	public static _GUID CLSID_TrayDeskBand/* Not supported: data(37 24 44 E6 68 6C 52 4F 94 DD 2C FE D2 67 EF B9) */;

	public static _GUID IID_ITrayDeskBand/* Not supported: data(46 E8 67 6D 9C 5B B8 4D 9C BC DD E1 2F 42 54 F1) */;

	public static _0024ArrayType_0024_0024_0024BY0A_0040P6AXXZ __xc_z/* Not supported: data(00) */;

	public static volatile uint __native_vcclrit_reason/* Not supported: data(FF FF FF FF) */;

	public static _0024ArrayType_0024_0024_0024BY0A_0040P6AXXZ __xc_a/* Not supported: data(00) */;

	public static _0024ArrayType_0024_0024_0024BY0A_0040P6AHXZ __xi_a/* Not supported: data(00) */;

	public static volatile __enative_startup_state __native_startup_state/* Not supported: data() */;

	public static _0024ArrayType_0024_0024_0024BY0A_0040P6AHXZ __xi_z/* Not supported: data(00) */;

	public unsafe static volatile void* __native_startup_lock/*Field data (rva=0x105ddc) could not be found in any section!*/;

	public static volatile uint __native_dllmain_reason/* Not supported: data(FF FF FF FF) */;

	internal unsafe static void WBSTRString_002E_007Bdtor_007D(WBSTRString* P_0)
	{
		WString_002E_007Bdtor_007D((WString*)P_0);
	}

	internal unsafe static void CComPtrNtv_003CIAddress_003E_002E_007Bdtor_007D(CComPtrNtv_003CIAddress_003E* P_0)
	{
		CComPtrNtv_003CIAddress_003E_002ERelease(P_0);
	}

	internal unsafe static CComPtrNtv_003CINewsletterSettings_003E* CComPtrNtv_003CINewsletterSettings_003E_002E_007Bctor_007D(CComPtrNtv_003CINewsletterSettings_003E* P_0)
	{
		*(int*)P_0 = 0;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CINewsletterSettings_003E_002E_007Bdtor_007D(CComPtrNtv_003CINewsletterSettings_003E* P_0)
	{
		CComPtrNtv_003CINewsletterSettings_003E_002ERelease(P_0);
	}

	internal unsafe static INewsletterSettings** CComPtrNtv_003CINewsletterSettings_003E_002E_0026(CComPtrNtv_003CINewsletterSettings_003E* P_0)
	{
		return (INewsletterSettings**)P_0;
	}

	internal unsafe static CComPtrNtv_003CIPrivacySettings_003E* CComPtrNtv_003CIPrivacySettings_003E_002E_007Bctor_007D(CComPtrNtv_003CIPrivacySettings_003E* P_0)
	{
		*(int*)P_0 = 0;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIPrivacySettings_003E_002E_007Bdtor_007D(CComPtrNtv_003CIPrivacySettings_003E* P_0)
	{
		CComPtrNtv_003CIPrivacySettings_003E_002ERelease(P_0);
	}

	internal unsafe static IPrivacySettings** CComPtrNtv_003CIPrivacySettings_003E_002E_0026(CComPtrNtv_003CIPrivacySettings_003E* P_0)
	{
		return (IPrivacySettings**)P_0;
	}

	internal unsafe static void CComPtrNtv_003CIAddress_003E_002ERelease(CComPtrNtv_003CIAddress_003E* P_0)
	{
		int num = *(int*)P_0;
		IAddress* ptr = (IAddress*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CINewsletterSettings_003E_002ERelease(CComPtrNtv_003CINewsletterSettings_003E* P_0)
	{
		int num = *(int*)P_0;
		INewsletterSettings* ptr = (INewsletterSettings*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIPrivacySettings_003E_002ERelease(CComPtrNtv_003CIPrivacySettings_003E* P_0)
	{
		int num = *(int*)P_0;
		IPrivacySettings* ptr = (IPrivacySettings*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static ref char PtrToStringChars(string s)
	{
		ref byte reference = ref *(byte*)s;
		if (Unsafe.AsPointer(ref reference) != null)
		{
			reference = ref *(byte*)((ref *(_003F*)RuntimeHelpers.OffsetToStringData) + (ref Unsafe.As<byte, _003F>(ref reference)));
		}
		return ref Unsafe.As<byte, char>(ref reference);
	}

	internal unsafe static void MusicTrackMetadata_002EClear(MusicTrackMetadata* P_0)
	{
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 20, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 36, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 52, ref GUID_NULL, 16);
		((int*)P_0)[17] = 0;
		((int*)P_0)[18] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[19]);
		((int*)P_0)[19] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[20]);
		((int*)P_0)[20] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[21]);
		((int*)P_0)[21] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[22]);
		((int*)P_0)[22] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[23]);
		((int*)P_0)[23] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[24]);
		((int*)P_0)[24] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[25]);
		((int*)P_0)[25] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[26]);
		((int*)P_0)[26] = 0;
		((int*)P_0)[27] = 0;
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 112));
	}

	internal unsafe static int MusicTrackMetadata_002EInitialize(MusicTrackMetadata* P_0, MusicTrackMetadata* musicTrack)
	{
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, _GUID*, _GUID*, _GUID*, int, int, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, int, IMediaRights*, int>)(int)(*(uint*)(int)(*(uint*)P_0)))((nint)P_0, (_GUID*)((byte*)musicTrack + 4), (_GUID*)((byte*)musicTrack + 20), (_GUID*)((byte*)musicTrack + 36), (_GUID*)((byte*)musicTrack + 52), ((int*)musicTrack)[17], ((int*)musicTrack)[18], (ushort*)(int)((uint*)musicTrack)[19], (ushort*)(int)((uint*)musicTrack)[20], (ushort*)(int)((uint*)musicTrack)[21], (ushort*)(int)((uint*)musicTrack)[22], (ushort*)(int)((uint*)musicTrack)[23], (ushort*)(int)((uint*)musicTrack)[24], (ushort*)(int)((uint*)musicTrack)[25], (ushort*)(int)((uint*)musicTrack)[26], ((int*)musicTrack)[27], (IMediaRights*)(int)((uint*)musicTrack)[28]);
	}

	internal unsafe static int MusicTrackMetadata_002EInitialize(MusicTrackMetadata* P_0, _GUID* guidMediaId, _GUID* guidAlbumId, _GUID* guidArtistId, _GUID* guidTrackArtistId, int nTrackNumber, int nDiscNumber, ushort* pwszTitle, ushort* pwszAlbum, ushort* pwszArtist, ushort* pwszTrackArtist, ushort* pwszGenre, ushort* pwszMusicNetId, ushort* pwszAmgId, ushort* pwszReleaseDate, int fIsExplicit, IMediaRights* pMediaRights)
	{
		if (pwszTitle == null)
		{
			_ZuneShipAssert(1001u, 114u);
			return -2147467261;
		}
		((int*)P_0)[27] = fIsExplicit;
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, guidMediaId, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 20, guidAlbumId, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 36, guidArtistId, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 52, guidTrackArtistId, 16);
		((int*)P_0)[17] = nTrackNumber;
		((int*)P_0)[18] = nDiscNumber;
		SafeSysFreeString((ushort**)((byte*)P_0 + 76));
		byte* num = (byte*)P_0 + 76;
		ushort* ptr = SysAllocString(pwszTitle);
		*(int*)num = (int)ptr;
		int result;
		if (ptr == null)
		{
			result = -2147024882;
			goto IL_0215;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 80));
		ushort** ptr2 = (ushort**)((byte*)P_0 + 80);
		if (pwszAlbum != null)
		{
			ushort* ptr3 = SysAllocString(pwszAlbum);
			*(int*)ptr2 = (int)ptr3;
			if (ptr3 == null)
			{
				result = -2147024882;
				goto IL_0215;
			}
		}
		else
		{
			*(int*)ptr2 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 84));
		ushort** ptr4 = (ushort**)((byte*)P_0 + 84);
		if (pwszArtist != null)
		{
			ushort* ptr5 = SysAllocString(pwszArtist);
			*(int*)ptr4 = (int)ptr5;
			if (ptr5 == null)
			{
				result = -2147024882;
				goto IL_0215;
			}
		}
		else
		{
			*(int*)ptr4 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 88));
		ushort** ptr6 = (ushort**)((byte*)P_0 + 88);
		if (pwszTrackArtist != null)
		{
			ushort* ptr7 = SysAllocString(pwszTrackArtist);
			*(int*)ptr6 = (int)ptr7;
			if (ptr7 == null)
			{
				result = -2147024882;
				goto IL_0215;
			}
		}
		else
		{
			*(int*)ptr6 = 0;
		}
		ushort** ptr8 = (ushort**)((byte*)P_0 + 92);
		SysFreeString((ushort*)(int)(*(uint*)ptr8));
		*(int*)ptr8 = 0;
		ushort** ptr9 = (ushort**)((byte*)P_0 + 92);
		if (pwszGenre != null)
		{
			ushort* ptr10 = SysAllocString(pwszGenre);
			*(int*)ptr9 = (int)ptr10;
			if (ptr10 == null)
			{
				result = -2147024882;
				goto IL_0215;
			}
		}
		else
		{
			*(int*)ptr9 = 0;
		}
		ushort** ptr11 = (ushort**)((byte*)P_0 + 96);
		SysFreeString((ushort*)(int)(*(uint*)ptr11));
		*(int*)ptr11 = 0;
		ushort** ptr12 = (ushort**)((byte*)P_0 + 96);
		if (pwszMusicNetId != null)
		{
			ushort* ptr13 = SysAllocString(pwszMusicNetId);
			*(int*)ptr12 = (int)ptr13;
			if (ptr13 == null)
			{
				result = -2147024882;
				goto IL_0215;
			}
		}
		else
		{
			*(int*)ptr12 = 0;
		}
		ushort** ptr14 = (ushort**)((byte*)P_0 + 100);
		SysFreeString((ushort*)(int)(*(uint*)ptr14));
		*(int*)ptr14 = 0;
		ushort** ptr15 = (ushort**)((byte*)P_0 + 100);
		if (pwszAmgId != null)
		{
			ushort* ptr16 = SysAllocString(pwszAmgId);
			*(int*)ptr15 = (int)ptr16;
			if (ptr16 == null)
			{
				result = -2147024882;
				goto IL_0215;
			}
		}
		else
		{
			*(int*)ptr15 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 104));
		ushort** ptr17 = (ushort**)((byte*)P_0 + 104);
		if (pwszReleaseDate != null)
		{
			ushort* ptr18 = SysAllocString(pwszReleaseDate);
			*(int*)ptr17 = (int)ptr18;
			if (ptr18 == null)
			{
				result = -2147024882;
				goto IL_0215;
			}
		}
		else
		{
			*(int*)ptr17 = 0;
		}
		result = 0;
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 112));
		if (pMediaRights != null)
		{
			((int*)P_0)[28] = (int)pMediaRights;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pMediaRights + 4)))((nint)pMediaRights);
		}
		goto IL_0220;
		IL_0220:
		return result;
		IL_0215:
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, void>)(int)(*(uint*)(*(int*)P_0 + 8)))((nint)P_0);
		goto IL_0220;
	}

	internal unsafe static void MusicAlbumMetadata_002EClear(MusicAlbumMetadata* P_0)
	{
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 20, ref GUID_NULL, 16);
		SysFreeString((ushort*)(int)((uint*)P_0)[9]);
		((int*)P_0)[9] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[10]);
		((int*)P_0)[10] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[11]);
		((int*)P_0)[11] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[12]);
		((int*)P_0)[12] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[13]);
		((int*)P_0)[13] = 0;
		((int*)P_0)[14] = 0;
		((int*)P_0)[15] = 0;
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 64));
		SafeRelease_003Cstruct_0020IVideoCollection_003E((IVideoCollection**)((byte*)P_0 + 68));
		SafeRelease_003Cstruct_0020IMusicTrackCollection_003E((IMusicTrackCollection**)((byte*)P_0 + 72));
	}

	internal unsafe static int MusicAlbumMetadata_002EInitialize(MusicAlbumMetadata* P_0, MusicAlbumMetadata* album)
	{
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, _GUID*, ushort*, ushort*, ushort*, ushort*, ushort*, int, int, IMediaRights*, IVideoCollection*, IMusicTrackCollection*, int>)(int)(*(uint*)(int)(*(uint*)P_0)))((nint)P_0, (_GUID*)((byte*)album + 4), (_GUID*)((byte*)album + 20), (ushort*)(int)((uint*)album)[9], (ushort*)(int)((uint*)album)[10], (ushort*)(int)((uint*)album)[11], (ushort*)(int)((uint*)album)[12], (ushort*)(int)((uint*)album)[13], ((int*)album)[14], ((int*)album)[15], (IMediaRights*)(int)((uint*)album)[16], (IVideoCollection*)(int)((uint*)album)[17], (IMusicTrackCollection*)(int)((uint*)album)[18]);
	}

	internal unsafe static int MusicAlbumMetadata_002EInitialize(MusicAlbumMetadata* P_0, _GUID* guidMediaId, _GUID* guidArtistId, ushort* pwszTitle, ushort* pwszArtist, ushort* pwszGenre, ushort* pwszReleaseDate, ushort* pwszCoverArtUrl, int fIsExplicit, int fPremium, IMediaRights* pMediaRights, IVideoCollection* pVideoCollection, IMusicTrackCollection* pMusicTrackCollection)
	{
		if (pwszTitle == null)
		{
			_ZuneShipAssert(1001u, 280u);
			return -2147467261;
		}
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, guidMediaId, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 20, guidArtistId, 16);
		((int*)P_0)[14] = fIsExplicit;
		((int*)P_0)[15] = fPremium;
		SafeSysFreeString((ushort**)((byte*)P_0 + 36));
		byte* num = (byte*)P_0 + 36;
		ushort* ptr = SysAllocString(pwszTitle);
		*(int*)num = (int)ptr;
		int result;
		if (ptr == null)
		{
			result = -2147024882;
			goto IL_01a7;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 40));
		ushort** ptr2 = (ushort**)((byte*)P_0 + 40);
		if (pwszArtist != null)
		{
			ushort* ptr3 = SysAllocString(pwszArtist);
			*(int*)ptr2 = (int)ptr3;
			if (ptr3 == null)
			{
				result = -2147024882;
				goto IL_01a7;
			}
		}
		else
		{
			*(int*)ptr2 = 0;
		}
		ushort** ptr4 = (ushort**)((byte*)P_0 + 44);
		SysFreeString((ushort*)(int)(*(uint*)ptr4));
		*(int*)ptr4 = 0;
		ushort** ptr5 = (ushort**)((byte*)P_0 + 44);
		if (pwszGenre != null)
		{
			ushort* ptr6 = SysAllocString(pwszGenre);
			*(int*)ptr5 = (int)ptr6;
			if (ptr6 == null)
			{
				result = -2147024882;
				goto IL_01a7;
			}
		}
		else
		{
			*(int*)ptr5 = 0;
		}
		ushort** ptr7 = (ushort**)((byte*)P_0 + 48);
		SysFreeString((ushort*)(int)(*(uint*)ptr7));
		*(int*)ptr7 = 0;
		ushort** ptr8 = (ushort**)((byte*)P_0 + 48);
		if (pwszReleaseDate != null)
		{
			ushort* ptr9 = SysAllocString(pwszReleaseDate);
			*(int*)ptr8 = (int)ptr9;
			if (ptr9 == null)
			{
				result = -2147024882;
				goto IL_01a7;
			}
		}
		else
		{
			*(int*)ptr8 = 0;
		}
		ushort** ptr10 = (ushort**)((byte*)P_0 + 52);
		SysFreeString((ushort*)(int)(*(uint*)ptr10));
		*(int*)ptr10 = 0;
		ushort** ptr11 = (ushort**)((byte*)P_0 + 52);
		if (pwszCoverArtUrl != null)
		{
			ushort* ptr12 = SysAllocString(pwszCoverArtUrl);
			*(int*)ptr11 = (int)ptr12;
			if (ptr12 == null)
			{
				result = -2147024882;
				goto IL_01a7;
			}
		}
		else
		{
			*(int*)ptr11 = 0;
		}
		result = 0;
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 64));
		if (pMediaRights != null)
		{
			((int*)P_0)[16] = (int)pMediaRights;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pMediaRights + 4)))((nint)pMediaRights);
		}
		SafeRelease_003Cstruct_0020IVideoCollection_003E((IVideoCollection**)((byte*)P_0 + 68));
		if (pVideoCollection != null)
		{
			((int*)P_0)[17] = (int)pVideoCollection;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pVideoCollection + 4)))((nint)pVideoCollection);
		}
		SafeRelease_003Cstruct_0020IMusicTrackCollection_003E((IMusicTrackCollection**)((byte*)P_0 + 72));
		if (pMusicTrackCollection != null)
		{
			((int*)P_0)[18] = (int)pMusicTrackCollection;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pMusicTrackCollection + 4)))((nint)pMusicTrackCollection);
		}
		goto IL_01b2;
		IL_01a7:
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, void>)(int)(*(uint*)(*(int*)P_0 + 8)))((nint)P_0);
		goto IL_01b2;
		IL_01b2:
		return result;
	}

	internal unsafe static void VideoMetadata_002EClear(VideoMetadata* P_0)
	{
		((int*)P_0)[5] = -1;
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 36, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 56, ref GUID_NULL, 16);
		((int*)P_0)[22] = 0;
		((int*)P_0)[23] = 0;
		((int*)P_0)[24] = 0;
		((int*)P_0)[29] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[28]);
		((int*)P_0)[28] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[6]);
		((int*)P_0)[6] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[7]);
		((int*)P_0)[7] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[8]);
		((int*)P_0)[8] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[13]);
		((int*)P_0)[13] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[18]);
		((int*)P_0)[18] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[19]);
		((int*)P_0)[19] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[20]);
		((int*)P_0)[20] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[21]);
		((int*)P_0)[21] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[25]);
		((int*)P_0)[25] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[26]);
		((int*)P_0)[26] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[27]);
		((int*)P_0)[27] = 0;
		SafeRelease_003Cstruct_0020IContributorCollection_003E((IContributorCollection**)((byte*)P_0 + 120));
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 124));
	}

	internal unsafe static int VideoMetadata_002EInitialize(VideoMetadata* P_0, VideoMetadata* video)
	{
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, EVideoType, ushort*, ushort*, ushort*, _GUID*, ushort*, _GUID*, ushort*, ushort*, ushort*, ushort*, int, int, int, ushort*, ushort*, ushort*, ushort*, int, IContributorCollection*, IMediaRights*, int>)(int)(*(uint*)(int)(*(uint*)P_0)))((nint)P_0, (_GUID*)((byte*)video + 4), ((EVideoType*)video)[5], (ushort*)(int)((uint*)video)[6], (ushort*)(int)((uint*)video)[7], (ushort*)(int)((uint*)video)[8], (_GUID*)((byte*)video + 36), (ushort*)(int)((uint*)video)[13], (_GUID*)((byte*)video + 56), (ushort*)(int)((uint*)video)[18], (ushort*)(int)((uint*)video)[19], (ushort*)(int)((uint*)video)[20], (ushort*)(int)((uint*)video)[21], ((int*)video)[22], ((int*)video)[23], ((int*)video)[24], (ushort*)(int)((uint*)video)[25], (ushort*)(int)((uint*)video)[26], (ushort*)(int)((uint*)video)[27], (ushort*)(int)((uint*)video)[28], ((int*)video)[29], (IContributorCollection*)(int)((uint*)video)[30], (IMediaRights*)(int)((uint*)video)[31]);
	}

	internal unsafe static int VideoMetadata_002EInitialize(VideoMetadata* P_0, _GUID* guidMediaId, EVideoType eType, ushort* pwszTitle, ushort* pwszDescription, ushort* pwszArtist, _GUID* guidArtistId, ushort* pwszAlbum, _GUID* guidAlbumId, ushort* pwszGenre, ushort* pwszProductionCompany, ushort* pwszNetwork, ushort* pwszSeriesTitle, int nSeasonNumber, int nEpisodeNumber, int nDurationSecs, ushort* pwszReleaseDate, ushort* pwszCopyright, ushort* pwszPreviewImageUrl, ushort* pwszParentalRating, int fIsExplicit, IContributorCollection* pContributors, IMediaRights* pMediaRights)
	{
		if (pwszTitle == null)
		{
			_ZuneShipAssert(1001u, 487u);
			return -2147467261;
		}
		((int*)P_0)[5] = (int)eType;
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, guidMediaId, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 36, guidArtistId, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 56, guidAlbumId, 16);
		((int*)P_0)[22] = nSeasonNumber;
		((int*)P_0)[23] = nEpisodeNumber;
		((int*)P_0)[24] = nDurationSecs;
		((int*)P_0)[29] = fIsExplicit;
		SafeSysFreeString((ushort**)((byte*)P_0 + 112));
		ushort** ptr = (ushort**)((byte*)P_0 + 112);
		int result;
		if (pwszParentalRating != null)
		{
			ushort* ptr2 = SysAllocString(pwszParentalRating);
			*(int*)ptr = (int)ptr2;
			if (ptr2 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 24));
		byte* num = (byte*)P_0 + 24;
		ushort* ptr3 = SysAllocString(pwszTitle);
		*(int*)num = (int)ptr3;
		if (ptr3 == null)
		{
			result = -2147024882;
			goto IL_02f5;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 28));
		ushort** ptr4 = (ushort**)((byte*)P_0 + 28);
		if (pwszDescription != null)
		{
			ushort* ptr5 = SysAllocString(pwszDescription);
			*(int*)ptr4 = (int)ptr5;
			if (ptr5 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr4 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 32));
		ushort** ptr6 = (ushort**)((byte*)P_0 + 32);
		if (pwszArtist != null)
		{
			ushort* ptr7 = SysAllocString(pwszArtist);
			*(int*)ptr6 = (int)ptr7;
			if (ptr7 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr6 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 52));
		ushort** ptr8 = (ushort**)((byte*)P_0 + 52);
		if (pwszAlbum != null)
		{
			ushort* ptr9 = SysAllocString(pwszAlbum);
			*(int*)ptr8 = (int)ptr9;
			if (ptr9 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr8 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 72));
		ushort** ptr10 = (ushort**)((byte*)P_0 + 72);
		if (pwszGenre != null)
		{
			ushort* ptr11 = SysAllocString(pwszGenre);
			*(int*)ptr10 = (int)ptr11;
			if (ptr11 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr10 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 76));
		ushort** ptr12 = (ushort**)((byte*)P_0 + 76);
		if (pwszProductionCompany != null)
		{
			ushort* ptr13 = SysAllocString(pwszProductionCompany);
			*(int*)ptr12 = (int)ptr13;
			if (ptr13 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr12 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 80));
		ushort** ptr14 = (ushort**)((byte*)P_0 + 80);
		if (pwszNetwork != null)
		{
			ushort* ptr15 = SysAllocString(pwszNetwork);
			*(int*)ptr14 = (int)ptr15;
			if (ptr15 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr14 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 84));
		ushort** ptr16 = (ushort**)((byte*)P_0 + 84);
		if (pwszSeriesTitle != null)
		{
			ushort* ptr17 = SysAllocString(pwszSeriesTitle);
			*(int*)ptr16 = (int)ptr17;
			if (ptr17 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr16 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 100));
		ushort** ptr18 = (ushort**)((byte*)P_0 + 100);
		if (pwszReleaseDate != null)
		{
			ushort* ptr19 = SysAllocString(pwszReleaseDate);
			*(int*)ptr18 = (int)ptr19;
			if (ptr19 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr18 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 104));
		ushort** ptr20 = (ushort**)((byte*)P_0 + 104);
		if (pwszCopyright != null)
		{
			ushort* ptr21 = SysAllocString(pwszCopyright);
			*(int*)ptr20 = (int)ptr21;
			if (ptr21 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr20 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 108));
		ushort** ptr22 = (ushort**)((byte*)P_0 + 108);
		if (pwszPreviewImageUrl != null)
		{
			ushort* ptr23 = SysAllocString(pwszPreviewImageUrl);
			*(int*)ptr22 = (int)ptr23;
			if (ptr23 == null)
			{
				result = -2147024882;
				goto IL_02f5;
			}
		}
		else
		{
			*(int*)ptr22 = 0;
		}
		result = 0;
		SafeRelease_003Cstruct_0020IContributorCollection_003E((IContributorCollection**)((byte*)P_0 + 120));
		if (pContributors != null)
		{
			((int*)P_0)[30] = (int)pContributors;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pContributors + 4)))((nint)pContributors);
		}
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 124));
		if (pMediaRights != null)
		{
			((int*)P_0)[31] = (int)pMediaRights;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pMediaRights + 4)))((nint)pMediaRights);
		}
		goto IL_0300;
		IL_02f5:
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, void>)(int)(*(uint*)(*(int*)P_0 + 8)))((nint)P_0);
		goto IL_0300;
		IL_0300:
		return result;
	}

	internal unsafe static void AppMetadata_002EClear(AppMetadata* P_0)
	{
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		SysFreeString((ushort*)(int)((uint*)P_0)[5]);
		((int*)P_0)[5] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[6]);
		((int*)P_0)[6] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[7]);
		((int*)P_0)[7] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[8]);
		((int*)P_0)[8] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[9]);
		((int*)P_0)[9] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[10]);
		((int*)P_0)[10] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[11]);
		((int*)P_0)[11] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[12]);
		((int*)P_0)[12] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[13]);
		((int*)P_0)[13] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[14]);
		((int*)P_0)[14] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[15]);
		((int*)P_0)[15] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[16]);
		((int*)P_0)[16] = 0;
		SysFreeString((ushort*)(int)((uint*)P_0)[17]);
		((int*)P_0)[17] = 0;
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 76));
	}

	internal unsafe static int AppMetadata_002EInitialize(AppMetadata* P_0, AppMetadata* video)
	{
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, IMediaRights*, int>)(int)(*(uint*)(int)(*(uint*)P_0)))((nint)P_0, (_GUID*)((byte*)video + 4), (ushort*)(int)((uint*)video)[5], (ushort*)(int)((uint*)video)[6], (ushort*)(int)((uint*)video)[7], (ushort*)(int)((uint*)video)[8], (ushort*)(int)((uint*)video)[9], (ushort*)(int)((uint*)video)[10], (ushort*)(int)((uint*)video)[11], (ushort*)(int)((uint*)video)[12], (ushort*)(int)((uint*)video)[13], (ushort*)(int)((uint*)video)[14], (ushort*)(int)((uint*)video)[15], (ushort*)(int)((uint*)video)[16], (ushort*)(int)((uint*)video)[17], (IMediaRights*)(int)((uint*)video)[19]);
	}

	internal unsafe static int AppMetadata_002EInitialize(AppMetadata* P_0, _GUID* _guidAppId, ushort* _pwszTitle, ushort* _pwszAuthor, ushort* _pwszDescription, ushort* _pwszPublisher, ushort* _pwszDeveloper, ushort* _pwszGenre, ushort* _pwszReleaseDate, ushort* _pwszCopyright, ushort* _pwszVersion, ushort* _pwszPreviewImageUrl, ushort* _pwszParentalRating, ushort* _pwszRatingImageUrl, ushort* _pwszDownloadUrl, IMediaRights* _pMediaRights)
	{
		if (_pwszTitle == null)
		{
			_ZuneShipAssert(1001u, 701u);
			return -2147467261;
		}
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, _guidAppId, 16);
		SafeSysFreeString((ushort**)((byte*)P_0 + 20));
		byte* num = (byte*)P_0 + 20;
		ushort* ptr = SysAllocString(_pwszTitle);
		*(int*)num = (int)ptr;
		int result;
		if (ptr == null)
		{
			result = -2147024882;
			goto IL_02cc;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 24));
		ushort** ptr2 = (ushort**)((byte*)P_0 + 24);
		if (_pwszAuthor != null)
		{
			ushort* ptr3 = SysAllocString(_pwszAuthor);
			*(int*)ptr2 = (int)ptr3;
			if (ptr3 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr2 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 28));
		ushort** ptr4 = (ushort**)((byte*)P_0 + 28);
		if (_pwszDescription != null)
		{
			ushort* ptr5 = SysAllocString(_pwszDescription);
			*(int*)ptr4 = (int)ptr5;
			if (ptr5 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr4 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 32));
		ushort** ptr6 = (ushort**)((byte*)P_0 + 32);
		if (_pwszPublisher != null)
		{
			ushort* ptr7 = SysAllocString(_pwszPublisher);
			*(int*)ptr6 = (int)ptr7;
			if (ptr7 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr6 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 36));
		ushort** ptr8 = (ushort**)((byte*)P_0 + 36);
		if (_pwszDeveloper != null)
		{
			ushort* ptr9 = SysAllocString(_pwszDeveloper);
			*(int*)ptr8 = (int)ptr9;
			if (ptr9 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr8 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 40));
		ushort** ptr10 = (ushort**)((byte*)P_0 + 40);
		if (_pwszGenre != null)
		{
			ushort* ptr11 = SysAllocString(_pwszGenre);
			*(int*)ptr10 = (int)ptr11;
			if (ptr11 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr10 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 44));
		ushort** ptr12 = (ushort**)((byte*)P_0 + 44);
		if (_pwszReleaseDate != null)
		{
			ushort* ptr13 = SysAllocString(_pwszReleaseDate);
			*(int*)ptr12 = (int)ptr13;
			if (ptr13 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr12 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 48));
		ushort** ptr14 = (ushort**)((byte*)P_0 + 48);
		if (_pwszCopyright != null)
		{
			ushort* ptr15 = SysAllocString(_pwszCopyright);
			*(int*)ptr14 = (int)ptr15;
			if (ptr15 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr14 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 52));
		ushort** ptr16 = (ushort**)((byte*)P_0 + 52);
		if (_pwszVersion != null)
		{
			ushort* ptr17 = SysAllocString(_pwszVersion);
			*(int*)ptr16 = (int)ptr17;
			if (ptr17 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr16 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 56));
		ushort** ptr18 = (ushort**)((byte*)P_0 + 56);
		if (_pwszPreviewImageUrl != null)
		{
			ushort* ptr19 = SysAllocString(_pwszPreviewImageUrl);
			*(int*)ptr18 = (int)ptr19;
			if (ptr19 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr18 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 64));
		ushort** ptr20 = (ushort**)((byte*)P_0 + 64);
		if (_pwszRatingImageUrl != null)
		{
			ushort* ptr21 = SysAllocString(_pwszRatingImageUrl);
			*(int*)ptr20 = (int)ptr21;
			if (ptr21 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr20 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 60));
		ushort** ptr22 = (ushort**)((byte*)P_0 + 60);
		if (_pwszParentalRating != null)
		{
			ushort* ptr23 = SysAllocString(_pwszParentalRating);
			*(int*)ptr22 = (int)ptr23;
			if (ptr23 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr22 = 0;
		}
		SafeSysFreeString((ushort**)((byte*)P_0 + 68));
		ushort** ptr24 = (ushort**)((byte*)P_0 + 68);
		if (_pwszDownloadUrl != null)
		{
			ushort* ptr25 = SysAllocString(_pwszDownloadUrl);
			*(int*)ptr24 = (int)ptr25;
			if (ptr25 == null)
			{
				result = -2147024882;
				goto IL_02cc;
			}
		}
		else
		{
			*(int*)ptr24 = 0;
		}
		result = 0;
		SafeRelease_003Cstruct_0020IMediaRights_003E((IMediaRights**)((byte*)P_0 + 76));
		if (_pMediaRights != null)
		{
			((int*)P_0)[19] = (int)_pMediaRights;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)_pMediaRights + 4)))((nint)_pMediaRights);
		}
		goto IL_02d7;
		IL_02cc:
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, void>)(int)(*(uint*)(*(int*)P_0 + 8)))((nint)P_0);
		goto IL_02d7;
		IL_02d7:
		return result;
	}

	internal unsafe static GetAccountCallbackWrapper* Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002E_007Bctor_007D(GetAccountCallbackWrapper* P_0, GetAccountCompleteCallback completeCallback, AccountManagementErrorCallback errorCallback)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		GetAccountCallbackWrapper* ptr = (GetAccountCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E*)ptr);
		try
		{
			GetAccountCallbackWrapper* ptr2 = (GetAccountCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E*)ptr2);
			try
			{
				((int*)P_0)[1] = 1;
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E*)ptr, completeCallback);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E*)ptr2, errorCallback);
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002E_007Bdtor_007D(GetAccountCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetAccountCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002EQueryInterface(GetAccountCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_223a83b5_e8ac_4aad_882a_14ee6634fc33)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002EAddRef(GetAccountCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002ERelease(GetAccountCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002EOnSuccess(GetAccountCallbackWrapper* P_0, IAccountUser* pAccountUser)
	{
		AccountUser accountUser = null;
		if (pAccountUser != null)
		{
			accountUser = new AccountUser(pAccountUser);
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_002EP_0024AAVGetAccountCompleteCallback_0040Service_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E*)((byte*)P_0 + 8))(accountUser);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002EOnError(GetAccountCallbackWrapper* P_0, int hr, IServiceError* pServiceError)
	{
		ServiceError serviceError = null;
		if (pServiceError != null)
		{
			serviceError = new ServiceError(pServiceError);
		}
		HRESULT hr2 = hr;
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_002EP_0024AAVAccountManagementErrorCallback_0040Service_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E*)((byte*)P_0 + 12))(hr2, serviceError);
		return 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E* P_0, GetAccountCompleteCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static GetAccountCompleteCallback gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E_002E_002EP_0024AAVGetAccountCompleteCallback_0040Service_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetAccountCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (GetAccountCompleteCallback)((GCHandle)intPtr).Target;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E* P_0, AccountManagementErrorCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static AccountManagementErrorCallback gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E_002E_002EP_0024AAVAccountManagementErrorCallback_0040Service_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAccountManagementErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (AccountManagementErrorCallback)((GCHandle)intPtr).Target;
	}

	internal unsafe static CComPtrNtv_003CIPassportIdentity_003E* CComPtrNtv_003CIPassportIdentity_003E_002E_007Bctor_007D(CComPtrNtv_003CIPassportIdentity_003E* P_0)
	{
		*(int*)P_0 = 0;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D(CComPtrNtv_003CIPassportIdentity_003E* P_0)
	{
		CComPtrNtv_003CIPassportIdentity_003E_002ERelease(P_0);
	}

	internal unsafe static IPassportIdentity** CComPtrNtv_003CIPassportIdentity_003E_002E_0026(CComPtrNtv_003CIPassportIdentity_003E* P_0)
	{
		return (IPassportIdentity**)P_0;
	}

	internal unsafe static void CComPtrNtv_003CICreditCard_003E_002E_007Bdtor_007D(CComPtrNtv_003CICreditCard_003E* P_0)
	{
		CComPtrNtv_003CICreditCard_003E_002ERelease(P_0);
	}

	internal unsafe static CComPtrNtv_003CIServiceError_003E* CComPtrNtv_003CIServiceError_003E_002E_007Bctor_007D(CComPtrNtv_003CIServiceError_003E* P_0)
	{
		*(int*)P_0 = 0;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIServiceError_003E_002E_007Bdtor_007D(CComPtrNtv_003CIServiceError_003E* P_0)
	{
		CComPtrNtv_003CIServiceError_003E_002ERelease(P_0);
	}

	internal unsafe static IServiceError** CComPtrNtv_003CIServiceError_003E_002E_0026(CComPtrNtv_003CIServiceError_003E* P_0)
	{
		return (IServiceError**)P_0;
	}

	internal unsafe static void CComPtrNtv_003CIAvailableZuneTagInformation_003E_002E_007Bdtor_007D(CComPtrNtv_003CIAvailableZuneTagInformation_003E* P_0)
	{
		CComPtrNtv_003CIAvailableZuneTagInformation_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIAccountUser_003E_002E_007Bdtor_007D(CComPtrNtv_003CIAccountUser_003E* P_0)
	{
		CComPtrNtv_003CIAccountUser_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIService_003E_002E_007Bdtor_007D(CComPtrNtv_003CIService_003E* P_0)
	{
		CComPtrNtv_003CIService_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIUnknown_003E_002E_007Bdtor_007D(CComPtrNtv_003CIUnknown_003E* P_0)
	{
		CComPtrNtv_003CIUnknown_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIAccountManagement_003E_002E_007Bdtor_007D(CComPtrNtv_003CIAccountManagement_003E* P_0)
	{
		CComPtrNtv_003CIAccountManagement_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIPassportIdentity_003E_002ERelease(CComPtrNtv_003CIPassportIdentity_003E* P_0)
	{
		int num = *(int*)P_0;
		IPassportIdentity* ptr = (IPassportIdentity*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CICreditCard_003E_002ERelease(CComPtrNtv_003CICreditCard_003E* P_0)
	{
		int num = *(int*)P_0;
		ICreditCard* ptr = (ICreditCard*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIServiceError_003E_002ERelease(CComPtrNtv_003CIServiceError_003E* P_0)
	{
		int num = *(int*)P_0;
		IServiceError* ptr = (IServiceError*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIAvailableZuneTagInformation_003E_002ERelease(CComPtrNtv_003CIAvailableZuneTagInformation_003E* P_0)
	{
		int num = *(int*)P_0;
		IAvailableZuneTagInformation* ptr = (IAvailableZuneTagInformation*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIAccountUser_003E_002ERelease(CComPtrNtv_003CIAccountUser_003E* P_0)
	{
		int num = *(int*)P_0;
		IAccountUser* ptr = (IAccountUser*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIService_003E_002ERelease(CComPtrNtv_003CIService_003E* P_0)
	{
		int num = *(int*)P_0;
		IService* ptr = (IService*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIUnknown_003E_002ERelease(CComPtrNtv_003CIUnknown_003E* P_0)
	{
		int num = *(int*)P_0;
		IUnknown* ptr = (IUnknown*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIAccountManagement_003E_002ERelease(CComPtrNtv_003CIAccountManagement_003E* P_0)
	{
		int num = *(int*)P_0;
		IAccountManagement* ptr = (IAccountManagement*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static int SafeRelease_003Cstruct_0020IMediaRights_003E(IMediaRights** pUnk)
	{
		uint num = *(uint*)pUnk;
		int result;
		if (num != 0)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			*(int*)pUnk = 0;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	internal unsafe static int SafeRelease_003Cstruct_0020IVideoCollection_003E(IVideoCollection** pUnk)
	{
		uint num = *(uint*)pUnk;
		int result;
		if (num != 0)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			*(int*)pUnk = 0;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	internal unsafe static int SafeRelease_003Cstruct_0020IMusicTrackCollection_003E(IMusicTrackCollection** pUnk)
	{
		uint num = *(uint*)pUnk;
		int result;
		if (num != 0)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			*(int*)pUnk = 0;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	internal unsafe static int SafeRelease_003Cstruct_0020IContributorCollection_003E(IContributorCollection** pUnk)
	{
		uint num = *(uint*)pUnk;
		int result;
		if (num != 0)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			*(int*)pUnk = 0;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	internal unsafe static int CComPtrNtv_003CIUnknown_003E_002EQueryInterface_003Cstruct_0020IAccountManagement_003E(CComPtrNtv_003CIUnknown_003E* P_0, IAccountManagement** pp)
	{
		uint num = *(uint*)P_0;
		if (num == 0)
		{
			_ZuneShipAssert(1001u, 349u);
			return -2147467261;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)(int)num)))((IntPtr)(int)num, (_GUID*)Unsafe.AsPointer(ref _GUID_a2506604_f033_4182_8bbe_a2bc722c568e), (void**)pp);
	}

	internal unsafe static int IsEqualGUID(_GUID* rguid1, _GUID* rguid2)
	{
		uint num = 16u;
		_GUID* ptr = rguid2;
		byte b = *(byte*)rguid1;
		byte b2 = *(byte*)rguid2;
		if ((uint)b >= (uint)b2)
		{
			_GUID* ptr2 = (_GUID*)((byte*)rguid1 - (nuint)rguid2);
			while ((uint)b <= (uint)b2)
			{
				if (num != 1)
				{
					num--;
					ptr = (_GUID*)((byte*)ptr + 1);
					b = ((byte*)ptr2)[(nuint)ptr];
					b2 = *(byte*)ptr;
					if ((uint)b < (uint)b2)
					{
						break;
					}
					continue;
				}
				return 1;
			}
		}
		return 0;
	}

	internal unsafe static void SafeSysFreeString(ushort** bstr)
	{
		SysFreeString((ushort*)(int)(*(uint*)bstr));
		*(int*)bstr = 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECBurnPublisherCallback_002EItemProgress(CBurnPublisherCallback* P_0, int lMediaIndex, __MIDL___MIDL_itf_wmpcd_0000_0014_0002 status, int nPercent)
	{
		CBurnPublisherCallback* ptr = (CBurnPublisherCallback*)((byte*)P_0 + 4);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr).ItemProgress(lMediaIndex, (EBurnProgressStatus)status, nPercent);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECBurnPublisherCallback_002EItemError(CBurnPublisherCallback* P_0, int lMediaIndex, int hrError)
	{
		CBurnPublisherCallback* ptr = (CBurnPublisherCallback*)((byte*)P_0 + 4);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr).ItemError(lMediaIndex, hrError);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECBurnPublisherCallback_002ESessionProgress(CBurnPublisherCallback* P_0, int lSessonSecondsRemaining, int lTotalSessionSeconds)
	{
		CBurnPublisherCallback* ptr = (CBurnPublisherCallback*)((byte*)P_0 + 4);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr).SessionProgress(lSessonSecondsRemaining, lTotalSessionSeconds);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECBurnPublisherCallback_002EBurnStateChange(CBurnPublisherCallback* P_0, __MIDL___MIDL_itf_wmpcd_0000_0014_0001 burnState)
	{
		CBurnPublisherCallback* ptr = (CBurnPublisherCallback*)((byte*)P_0 + 4);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr).BurnStateChange((EBurnState)burnState);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECBurnPublisherCallback_002ESetDriveLockedForBurning(CBurnPublisherCallback* P_0, int fLocked)
	{
		CBurnPublisherCallback* ptr = (CBurnPublisherCallback*)((byte*)P_0 + 4);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr) != null)
		{
			bool driveLockedForBurning = ((fLocked != 0) ? true : false);
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr).SetDriveLockedForBurning(driveLockedForBurning);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECBurnPublisherCallback_002EQueryCancel(CBurnPublisherCallback* P_0, int* pfCancel)
	{
		CBurnPublisherCallback* ptr = (CBurnPublisherCallback*)((byte*)P_0 + 4);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr) != null)
		{
			bool flag = false;
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)ptr).QueryCancel(&flag);
			int num = (flag ? 1 : 0);
			*pfCancel = num;
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECBurnPublisherCallback_002EQueryInterface(CBurnPublisherCallback* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_dfb90302_e898_40f7_ab37_e5ba31902d09)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ECBurnPublisherCallback_002EAddRef(CBurnPublisherCallback* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 2);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ECBurnPublisherCallback_002ERelease(CBurnPublisherCallback* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 2);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002ECBurnPublisherCallback_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002ECBurnPublisherCallback_002E_007Bdtor_007D(CBurnPublisherCallback* P_0)
	{
		gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)((byte*)P_0 + 4));
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static ZuneLibraryCDDevice gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDevice_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (ZuneLibraryCDDevice)((GCHandle)intPtr).Target;
	}

	internal unsafe static ZuneLibraryCDDevice gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (ZuneLibraryCDDevice)((GCHandle)intPtr).Target;
	}

	internal unsafe static AsyncCallbackWrapper* Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(AsyncCallbackWrapper* P_0, AsyncCompleteHandler asyncCompleteHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[1] = 1;
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E*)ptr, asyncCompleteHandler);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002EQueryInterface(AsyncCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002EAddRef(AsyncCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002ERelease(AsyncCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0)
		{
			if (P_0 != null)
			{
				*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AsyncCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E*)((byte*)P_0 + 8));
				delete(P_0);
			}
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002EOnComplete(AsyncCallbackWrapper* P_0, int hr)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		object? target = ((GCHandle)intPtr).Target;
		HRESULT hr2 = hr;
		target(hr2);
		return 0;
	}

	internal unsafe static CallbackOnUIThreadBimodalUnmanaged_DONOTUSE* MicrosoftZuneLibrary_002ECallbackOnUIThreadBimodalUnmanaged_DONOTUSE_002E_007Bctor_007D(CallbackOnUIThreadBimodalUnmanaged_DONOTUSE* P_0, CallbackOnUIThreadBimodalManaged_DONOTUSE pManaged)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CallbackOnUIThreadBimodalUnmanaged_DONOTUSE_0040MicrosoftZuneLibrary_0040_00406B_0040);
		gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E*)((byte*)P_0 + 4), pManaged);
		try
		{
			SetUIThreadCBWorker((IUIThreadCallbackWorker*)P_0);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 4);
			throw;
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ECallbackOnUIThreadBimodalUnmanaged_DONOTUSE_002ECallbackOnUIThreadRequest(CallbackOnUIThreadBimodalUnmanaged_DONOTUSE* P_0, ECallbackPriorityNative priority, int id, void* pv, INativeDeferredCallback* pNativeDeferredCallback)
	{
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNativeDeferredCallback + 4)))((nint)pNativeDeferredCallback);
		gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E*)((byte*)P_0 + 4)).CallbackOnUIThreadRequest((CallbackPriorityManaged)priority, id, pv, pNativeDeferredCallback);
	}

	internal unsafe static void MicrosoftZuneLibrary_002ECallbackOnUIThreadBimodalUnmanaged_DONOTUSE_002ECallback(CallbackOnUIThreadBimodalUnmanaged_DONOTUSE* P_0, int id, void* pv, INativeDeferredCallback* pNativeDeferredCallback)
	{
		((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, void*, void>)(int)(*(uint*)(*(int*)pNativeDeferredCallback + 12)))((nint)pNativeDeferredCallback, id, pv);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNativeDeferredCallback + 8)))((nint)pNativeDeferredCallback);
	}

	internal unsafe static void MicrosoftZuneLibrary_002ECallbackOnUIThreadBimodalUnmanaged_DONOTUSE_002E_007Bdtor_007D(CallbackOnUIThreadBimodalUnmanaged_DONOTUSE* P_0)
	{
		gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E*)((byte*)P_0 + 4));
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AAsyncCompleteHandler_0020_005E_003E* P_0, AsyncCompleteHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E* P_0, CallbackOnUIThreadBimodalManaged_DONOTUSE t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static CallbackOnUIThreadBimodalManaged_DONOTUSE gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003ACallbackOnUIThreadBimodalManaged_DONOTUSE_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (CallbackOnUIThreadBimodalManaged_DONOTUSE)((GCHandle)intPtr).Target;
	}

	[DllImport("ZuneNativeLib")]
	public unsafe static extern int SetUIThreadCBWorker(IUIThreadCallbackWorker* pUIThreadCallbackWorker);

	internal unsafe static CBurnPublisherCallback* MicrosoftZuneLibrary_002ECBurnPublisherCallback_002E_007Bctor_007D(CBurnPublisherCallback* P_0, ZuneLibraryCDDevice owningDevice)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CBurnPublisherCallback_0040MicrosoftZuneLibrary_0040_00406B_0040);
		gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*)((byte*)P_0 + 4), owningDevice);
		try
		{
			((int*)P_0)[2] = 0;
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 4);
			throw;
		}
	}

	internal unsafe static CDDeviceCallback* MicrosoftZuneLibrary_002ECDDeviceCallback_002E_007Bctor_007D(CDDeviceCallback* P_0, ZuneLibraryCDDeviceList deviceList)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CDDeviceCallback_0040MicrosoftZuneLibrary_0040_00406B_0040);
		CDDeviceCallback* ptr = (CDDeviceCallback*)((byte*)P_0 + 4);
		gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[2] = 0;
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_003D((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E*)ptr, deviceList);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 4);
			throw;
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ECDDeviceCallback_002EOnMediaChange(CDDeviceCallback* P_0, ushort drive, int fMediaPresent)
	{
		CDDeviceCallback* ptr = (CDDeviceCallback*)((byte*)P_0 + 4);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDeviceList_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E*)ptr).OnMediaChanged(drive, fMediaPresent);
		}
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECDDeviceCallback_002EQueryInterface(CDDeviceCallback* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_63c780f9_0f40_4e4a_8c9e_91f7a48d5946)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ECDDeviceCallback_002EAddRef(CDDeviceCallback* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 2);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ECDDeviceCallback_002ERelease(CDDeviceCallback* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 2);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002ECDDeviceCallback_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002ECDDeviceCallback_002E_007Bdtor_007D(CDDeviceCallback* P_0)
	{
		gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E*)((byte*)P_0 + 4));
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDevice_0020_005E_003E* P_0, ZuneLibraryCDDevice t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_003D(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E* P_0, ZuneLibraryCDDeviceList t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static ZuneLibraryCDDeviceList gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDDeviceList_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (ZuneLibraryCDDeviceList)((GCHandle)intPtr).Target;
	}

	internal unsafe static ZuneLibraryCDDeviceList gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDDeviceList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (ZuneLibraryCDDeviceList)((GCHandle)intPtr).Target;
	}

	internal unsafe static int CComPropVariant_002EClear(CComPropVariant* P_0)
	{
		ushort num = *(ushort*)P_0;
		if (num != 0 && num != 1)
		{
			return PropVariantClear((tagPROPVARIANT*)P_0);
		}
		return 0;
	}

	internal unsafe static void CComPropVariant_002E_007Bdtor_007D(CComPropVariant* P_0)
	{
		CComPropVariant_002EClear(P_0);
	}

	internal unsafe static CComPropVariant* CComPropVariant_002E_007Bctor_007D(CComPropVariant* P_0)
	{
		// IL initblk instruction
		Unsafe.InitBlock(P_0, 0, 16);
		return P_0;
	}

	internal unsafe static void WPP_SF_D(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, 0));
	}

	internal unsafe static NotificationMarshaller* Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002E_007Bctor_007D(NotificationMarshaller* P_0, NativeConfigurationChangeEventHandler eventHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7NotificationMarshaller_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E*)((byte*)P_0 + 4), eventHandler);
		try
		{
			((int*)P_0)[2] = 1;
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 4);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002ENotification(NotificationMarshaller* P_0, int iCategory, void* pSourceInstance, int iType, int iSubType, IUnknown* pData)
	{
		return -2147418113;
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002ENotification(NotificationMarshaller* P_0, int iCategory, void* pSourceInstance, ushort* pwszType, ushort* pwszSubType, IUnknown* pData)
	{
		if (pwszType == null)
		{
			_ZuneShipAssert(1001u, 93u);
			return -2147467261;
		}
		if (pwszSubType == null)
		{
			_ZuneShipAssert(1001u, 94u);
			return -2147467261;
		}
		NativeConfigurationChangeEventHandler nativeConfigurationChangeEventHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E_002E_002EP_0024AAVNativeConfigurationChangeEventHandler_0040Configuration_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E*)((byte*)P_0 + 4));
		if (null != nativeConfigurationChangeEventHandler)
		{
			nativeConfigurationChangeEventHandler(pwszSubType);
		}
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002EQueryInterface(NotificationMarshaller* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 116u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00e9004f_0cab_40ff_98ae_fad1a5ca594d)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002EAddRef(NotificationMarshaller* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 2);
	}

	internal unsafe static uint Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002ERelease(NotificationMarshaller* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 2);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002E_007Bdtor_007D(NotificationMarshaller* P_0)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E*)((byte*)P_0 + 4));
	}

	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E* P_0, NativeConfigurationChangeEventHandler t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static NativeConfigurationChangeEventHandler gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E_002E_002EP_0024AAVNativeConfigurationChangeEventHandler_0040Configuration_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ANativeConfigurationChangeEventHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (NativeConfigurationChangeEventHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static int wmemcpy_s(ushort* _S1, uint _N1, ushort* _S2, uint _N)
	{
		return memcpy_s(_S1, _N1 << 1, _S2, _N << 1);
	}

	internal unsafe static void WPP_SF_(ulong Logger, ushort id, _GUID* TraceGuid)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(0));
	}

	internal unsafe static void WPP_SF_Dd(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1, int _a2)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, 0));
	}

	internal unsafe static void WPP_SF_d(ulong Logger, ushort id, _GUID* TraceGuid, int _a1)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, 0));
	}

	internal unsafe static void WPP_SF_dd(ulong Logger, ushort id, _GUID* TraceGuid, int _a1, int _a2)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, 0));
	}

	internal unsafe static void DataStructs_002EIntSet_002EInitialize(IntSet* P_0)
	{
		*(int*)P_0 = 0;
		((int*)P_0)[1] = -1;
		((int*)P_0)[2] = -1;
		((int*)P_0)[3] = 0;
		((int*)P_0)[4] = 0;
		((int*)P_0)[5] = 0;
	}

	internal unsafe static void DataStructs_002EIntSet_002EFreeData(IntSet* P_0)
	{
		uint num = ((uint*)P_0)[5];
		if (num != 0)
		{
			uint num2 = ((uint*)P_0)[4];
			if (num2 != 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<uint*, void>)(int)num)((uint*)(int)num2);
				((int*)P_0)[4] = 0;
			}
		}
	}

	internal unsafe static void DataStructs_002EIntSet_002E_007Bdtor_007D(IntSet* P_0)
	{
		DataStructs_002EIntSet_002EFreeData(P_0);
	}

	internal unsafe static void CComPtrNtv_003CISyncEngine_003E_002E_007Bdtor_007D(CComPtrNtv_003CISyncEngine_003E* P_0)
	{
		CComPtrNtv_003CISyncEngine_003E_002ERelease(P_0);
	}

	internal unsafe static CComPtrNtv_003CDeviceMediator_003E* CComPtrNtv_003CDeviceMediator_003E_002E_007Bctor_007D(CComPtrNtv_003CDeviceMediator_003E* P_0, DeviceMediator* lp)
	{
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CDeviceMediator_003E_002E_007Bdtor_007D(CComPtrNtv_003CDeviceMediator_003E* P_0)
	{
		CComPtrNtv_003CDeviceMediator_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CDeviceMediator_003E_002ERelease(CComPtrNtv_003CDeviceMediator_003E* P_0)
	{
		int num = *(int*)P_0;
		DeviceMediator* ptr = (DeviceMediator*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIDeviceAssetProvider_003E_002E_007Bdtor_007D(CComPtrNtv_003CIDeviceAssetProvider_003E* P_0)
	{
		CComPtrNtv_003CIDeviceAssetProvider_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIGasGauge_003E_002E_007Bdtor_007D(CComPtrNtv_003CIGasGauge_003E* P_0)
	{
		CComPtrNtv_003CIGasGauge_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIWlanProvider_003E_002E_007Bdtor_007D(CComPtrNtv_003CIWlanProvider_003E* P_0)
	{
		CComPtrNtv_003CIWlanProvider_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIMetadataManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CIMetadataManager_003E* P_0)
	{
		CComPtrNtv_003CIMetadataManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIDeviceContentProvider_003E_002E_007Bdtor_007D(CComPtrNtv_003CIDeviceContentProvider_003E* P_0)
	{
		CComPtrNtv_003CIDeviceContentProvider_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CISyncEngine_003E_002ERelease(CComPtrNtv_003CISyncEngine_003E* P_0)
	{
		int num = *(int*)P_0;
		ISyncEngine* ptr = (ISyncEngine*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIDeviceAssetProvider_003E_002ERelease(CComPtrNtv_003CIDeviceAssetProvider_003E* P_0)
	{
		int num = *(int*)P_0;
		IDeviceAssetProvider* ptr = (IDeviceAssetProvider*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIGasGauge_003E_002ERelease(CComPtrNtv_003CIGasGauge_003E* P_0)
	{
		int num = *(int*)P_0;
		IGasGauge* ptr = (IGasGauge*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIWlanProvider_003E_002ERelease(CComPtrNtv_003CIWlanProvider_003E* P_0)
	{
		int num = *(int*)P_0;
		IWlanProvider* ptr = (IWlanProvider*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIMetadataManager_003E_002ERelease(CComPtrNtv_003CIMetadataManager_003E* P_0)
	{
		int num = *(int*)P_0;
		IMetadataManager* ptr = (IMetadataManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIDeviceContentProvider_003E_002ERelease(CComPtrNtv_003CIDeviceContentProvider_003E* P_0)
	{
		int num = *(int*)P_0;
		IDeviceContentProvider* ptr = (IDeviceContentProvider*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static int GetEndpointHostInterfaceProperty_003Cstruct_0020IDeviceAssetProvider_003E(IEndpointHost* pHost, EEndpointHostProperty propId, IDeviceAssetProvider** ppT)
	{
		return GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IDeviceAssetProvider_003E(pHost, propId, ppT);
	}

	internal unsafe static void SafeDeleteArray_003Cunsigned_0020short_0020_002A_003E(ushort*** pT)
	{
		uint num = *(uint*)pT;
		if (num != 0)
		{
			delete_005B_005D((void*)(int)num);
			*(int*)pT = 0;
		}
	}

	internal unsafe static void SafeDeleteArray_003Cunsigned_0020long_003E(uint** pT)
	{
		uint num = *(uint*)pT;
		if (num != 0)
		{
			delete_005B_005D((void*)(int)num);
			*(int*)pT = 0;
		}
	}

	internal unsafe static int GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020EEndpointStatus_003E(IEndpointHost* pC, EEndpointHostProperty propId, EEndpointStatus* pT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 244u);
			return -2147467261;
		}
		if (pT == null)
		{
			_ZuneShipAssert(1001u, 245u);
			return -2147467261;
		}
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pC + 68)))((nint)pC, propId, &num);
		if (num2 >= 0)
		{
			*pT = (EEndpointStatus)num;
		}
		return num2;
	}

	internal unsafe static int GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IDeviceAssetProvider_003E(IEndpointHost* pC, EEndpointHostProperty propId, IDeviceAssetProvider** ppT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 193u);
			return -2147467261;
		}
		if (ppT == null)
		{
			_ZuneShipAssert(1001u, 194u);
			return -2147467261;
		}
		IUnknown* ptr = null;
		*(int*)ppT = 0;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, IUnknown**, int>)(int)(*(uint*)(*(int*)pC + 52)))((nint)pC, propId, &ptr);
		if (num >= 0 && ptr != null)
		{
			num = SafeQueryInterface_003Cstruct_0020IDeviceAssetProvider_003E(ptr, ppT);
		}
		SafeRelease_003Cstruct_0020IUnknown_003E(&ptr);
		return num;
	}

	internal unsafe static int GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020ESyncRelationship_003E(IEndpointHost* pC, EEndpointHostProperty propId, ESyncRelationship* pT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 244u);
			return -2147467261;
		}
		if (pT == null)
		{
			_ZuneShipAssert(1001u, 245u);
			return -2147467261;
		}
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pC + 68)))((nint)pC, propId, &num);
		if (num2 >= 0)
		{
			*pT = (ESyncRelationship)num;
		}
		return num2;
	}

	internal unsafe static int GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020EEndpointCompatibilityStatus_003E(IEndpointHost* pC, EEndpointHostProperty propId, EEndpointCompatibilityStatus* pT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 244u);
			return -2147467261;
		}
		if (pT == null)
		{
			_ZuneShipAssert(1001u, 245u);
			return -2147467261;
		}
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pC + 68)))((nint)pC, propId, &num);
		if (num2 >= 0)
		{
			*pT = (EEndpointCompatibilityStatus)num;
		}
		return num2;
	}

	internal unsafe static int GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020ISyncEngine_003E(IEndpointHost* pC, EEndpointHostProperty propId, ISyncEngine** ppT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 193u);
			return -2147467261;
		}
		if (ppT == null)
		{
			_ZuneShipAssert(1001u, 194u);
			return -2147467261;
		}
		IUnknown* ptr = null;
		*(int*)ppT = 0;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, IUnknown**, int>)(int)(*(uint*)(*(int*)pC + 52)))((nint)pC, propId, &ptr);
		if (num >= 0 && ptr != null)
		{
			num = SafeQueryInterface_003Cstruct_0020ISyncEngine_003E(ptr, ppT);
		}
		SafeRelease_003Cstruct_0020IUnknown_003E(&ptr);
		return num;
	}

	internal unsafe static int GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IGasGauge_003E(IEndpointHost* pC, EEndpointHostProperty propId, IGasGauge** ppT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 193u);
			return -2147467261;
		}
		if (ppT == null)
		{
			_ZuneShipAssert(1001u, 194u);
			return -2147467261;
		}
		IUnknown* ptr = null;
		*(int*)ppT = 0;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, IUnknown**, int>)(int)(*(uint*)(*(int*)pC + 52)))((nint)pC, propId, &ptr);
		if (num >= 0 && ptr != null)
		{
			num = SafeQueryInterface_003Cstruct_0020IGasGauge_003E(ptr, ppT);
		}
		SafeRelease_003Cstruct_0020IUnknown_003E(&ptr);
		return num;
	}

	internal unsafe static int GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IWlanProvider_003E(IEndpointHost* pC, EEndpointHostProperty propId, IWlanProvider** ppT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 193u);
			return -2147467261;
		}
		if (ppT == null)
		{
			_ZuneShipAssert(1001u, 194u);
			return -2147467261;
		}
		IUnknown* ptr = null;
		*(int*)ppT = 0;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, IUnknown**, int>)(int)(*(uint*)(*(int*)pC + 52)))((nint)pC, propId, &ptr);
		if (num >= 0 && ptr != null)
		{
			num = SafeQueryInterface_003Cstruct_0020IWlanProvider_003E(ptr, ppT);
		}
		SafeRelease_003Cstruct_0020IUnknown_003E(&ptr);
		return num;
	}

	internal unsafe static int SafeQueryInterface_003Cstruct_0020IDeviceAssetProvider_003E(IUnknown* pUnk, IDeviceAssetProvider** ppT)
	{
		if (ppT != null)
		{
			*(int*)ppT = 0;
			if (pUnk != null)
			{
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pUnk)))((nint)pUnk, (_GUID*)Unsafe.AsPointer(ref _GUID_3b0ca835_8f64_4f22_95cf_ebd758a65835), (void**)ppT);
			}
			return -2147467261;
		}
		return -2147467261;
	}

	internal unsafe static int SafeRelease_003Cstruct_0020IUnknown_003E(IUnknown** pUnk)
	{
		uint num = *(uint*)pUnk;
		int result;
		if (num != 0)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			*(int*)pUnk = 0;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	internal unsafe static int SafeQueryInterface_003Cstruct_0020ISyncEngine_003E(IUnknown* pUnk, ISyncEngine** ppT)
	{
		if (ppT != null)
		{
			*(int*)ppT = 0;
			if (pUnk != null)
			{
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pUnk)))((nint)pUnk, (_GUID*)Unsafe.AsPointer(ref _GUID_e7ffb676_fc41_4504_9768_1819b3535d23), (void**)ppT);
			}
			return -2147467261;
		}
		return -2147467261;
	}

	internal unsafe static int SafeQueryInterface_003Cstruct_0020IGasGauge_003E(IUnknown* pUnk, IGasGauge** ppT)
	{
		if (ppT != null)
		{
			*(int*)ppT = 0;
			if (pUnk != null)
			{
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pUnk)))((nint)pUnk, (_GUID*)Unsafe.AsPointer(ref _GUID_50502dd1_e15b_43fe_b10d_769116ead2b2), (void**)ppT);
			}
			return -2147467261;
		}
		return -2147467261;
	}

	internal unsafe static int SafeQueryInterface_003Cstruct_0020IWlanProvider_003E(IUnknown* pUnk, IWlanProvider** ppT)
	{
		if (ppT != null)
		{
			*(int*)ppT = 0;
			if (pUnk != null)
			{
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pUnk)))((nint)pUnk, (_GUID*)Unsafe.AsPointer(ref _GUID_7b6ba3cc_fb9a_4356_942a_7da1dc0eeb70), (void**)ppT);
			}
			return -2147467261;
		}
		return -2147467261;
	}

	internal unsafe static void WPP_SF_S(ulong Logger, ushort id, _GUID* TraceGuid, ushort* _a1)
	{
		//IL_0039->IL0039: Incompatible stack types: I vs Ref
		uint num2;
		if (_a1 != null)
		{
			uint num;
			if (*_a1 == 0)
			{
				num = 14u;
			}
			else
			{
				ushort* ptr = _a1;
				do
				{
					ptr++;
				}
				while (Unsafe.ReadUnaligned<short>(ptr) != 0);
				num = (uint)(((nint)((byte*)ptr - (nuint)_a1) >> 1) * 2 + 2);
			}
			num2 = num;
		}
		else
		{
			num2 = 10u;
		}
		ushort* ptr2 = (ushort*)((_a1 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040) : Unsafe.AsPointer(ref *_a1 == 0 ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY06_0024_0024CBG, _003F>(ref _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)_a1));
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(ptr2, num2, 0));
	}

	internal unsafe static void WPP_SF_l(ulong Logger, ushort id, _GUID* TraceGuid, int _a1)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, 0));
	}

	internal unsafe static void CComPtrNtv_003CIEndpointNotification_003E_002E_007Bdtor_007D(CComPtrNtv_003CIEndpointNotification_003E* P_0)
	{
		CComPtrNtv_003CIEndpointNotification_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIEndpointNotification_003E_002ERelease(CComPtrNtv_003CIEndpointNotification_003E* P_0)
	{
		int num = *(int*)P_0;
		IEndpointNotification* ptr = (IEndpointNotification*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static int IUnknown_002EQueryInterface_003Cstruct_0020IEndpointNotification_003E(IUnknown* P_0, IEndpointNotification** pp)
	{
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)P_0)))((nint)P_0, (_GUID*)Unsafe.AsPointer(ref _GUID_53baba84_a16e_4fbd_a84a_428297ecff07), (void**)pp);
	}

	internal unsafe static DownloadTaskProxy* Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002E_007Bctor_007D(DownloadTaskProxy* P_0, IDownloadTask* pDownloadTask)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 1;
		((int*)P_0)[2] = 1;
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*)((byte*)P_0 + 12));
		try
		{
			((int*)P_0)[4] = (int)pDownloadTask;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pDownloadTask + 4)))((nint)pDownloadTask);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002E_007Bdtor_007D(DownloadTaskProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DownloadTaskProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			DownloadTaskProxy* ptr = (DownloadTaskProxy*)((byte*)P_0 + 16);
			uint num = *(uint*)ptr;
			if (0 != num)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
				*(int*)ptr = 0;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*)((byte*)P_0 + 12));
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002ESetDelegate(DownloadTaskProxy* P_0, DownloadProgressHandler downloadProgressHandler)
	{
		if (null == downloadProgressHandler)
		{
			return -2147467261;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*)((byte*)P_0 + 12), downloadProgressHandler);
		int result = 0;
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTaskProgress*, int>)(int)(*(uint*)(*(int*)num + 104)))((IntPtr)num, (IDownloadTaskProgress*)P_0);
		}
		return result;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EResetDelegate(DownloadTaskProxy* P_0)
	{
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTaskProgress*, int>)(int)(*(uint*)(*(int*)num + 108)))((IntPtr)num, (IDownloadTaskProgress*)P_0);
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*)((byte*)P_0 + 12), null);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002ECancel(DownloadTaskProxy* P_0)
	{
		IDownloadManager* ptr = null;
		int num = GetSingleton((_GUID)_GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTask*, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, (IDownloadTask*)(int)((uint*)P_0)[4]);
		}
		if (null != ptr)
		{
			IDownloadManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EDownloadNext(DownloadTaskProxy* P_0)
	{
		IDownloadManager* ptr = null;
		int num = GetSingleton((_GUID)_GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTask*, int>)(int)(*(uint*)(*(int*)ptr + 60)))((nint)ptr, (IDownloadTask*)(int)((uint*)P_0)[4]);
		}
		if (null != ptr)
		{
			IDownloadManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002ESetPosition(DownloadTaskProxy* P_0, uint uPosition)
	{
		IDownloadManager* ptr = null;
		int num = GetSingleton((_GUID)_GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTask*, uint, int>)(int)(*(uint*)(*(int*)ptr + 56)))((nint)ptr, (IDownloadTask*)(int)((uint*)P_0)[4], uPosition);
		}
		if (null != ptr)
		{
			IDownloadManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	internal unsafe static string Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetProperty(DownloadTaskProxy* P_0, string propertyName)
	{
		object result = null;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(propertyName)))
		{
			ushort* ptr2 = null;
			int num = *(int*)(int)((uint*)P_0)[4] + 28;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort**, int>)(int)(*(uint*)num))((IntPtr)((int*)P_0)[4], ptr, &ptr2) >= 0)
			{
				result = new string((char*)ptr2);
			}
			if (ptr2 != null)
			{
				SysFreeString(ptr2);
			}
			return (string)result;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetPropertyInt(DownloadTaskProxy* P_0, string propertyName)
	{
		int result = 0;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(propertyName)))
		{
			int num = 0;
			int num2 = *(int*)(int)((uint*)P_0)[4] + 24;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num2))((IntPtr)((int*)P_0)[4], ptr, &num) >= 0)
			{
				result = num;
			}
			return result;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EEquals(DownloadTaskProxy* P_0, DownloadTaskProxy* taskProxy)
	{
		bool result = false;
		if (taskProxy != null)
		{
			result = ((int*)P_0)[4] == ((int*)taskProxy)[4];
		}
		return result;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002ESetProperty(DownloadTaskProxy* P_0, string propertyName, string propertyValue)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(propertyName)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref PtrToStringChars(propertyValue)))
			{
				int num = *(int*)(int)((uint*)P_0)[4] + 20;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, int>)(int)(*(uint*)num))((IntPtr)((int*)P_0)[4], ptr, ptr2);
			}
		}
	}

	internal unsafe static string Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetTaskId(DownloadTaskProxy* P_0)
	{
		string result = null;
		if (((int*)P_0)[4] != 0)
		{
			ushort* ptr = null;
			int num = ((int*)P_0)[4];
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)num + 12)))((IntPtr)num, &ptr) >= 0)
			{
				result = new string((char*)ptr);
			}
			if (ptr != null)
			{
				SysFreeString(ptr);
			}
		}
		return result;
	}

	internal unsafe static EDownloadTaskState Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetState(DownloadTaskProxy* P_0)
	{
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, EDownloadTaskState>)(int)(*(uint*)(*(int*)num + 100)))((IntPtr)num, null);
		}
		return EDownloadTaskState.DLTaskNone;
	}

	internal unsafe static float Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetProgress(DownloadTaskProxy* P_0)
	{
		float result = 0f;
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, float>)(int)(*(uint*)(*(int*)num + 56)))((IntPtr)num);
		}
		return result;
	}

	internal unsafe static ulong Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetBytesDownloaded(DownloadTaskProxy* P_0)
	{
		long result = 0L;
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			result = (long)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong>)(int)(*(uint*)(*(int*)num + 60)))((IntPtr)num);
		}
		return (ulong)result;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetDownloadSecondsRemaining(DownloadTaskProxy* P_0)
	{
		int result = -1;
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)num + 52)))((IntPtr)num);
		}
		return result;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetDownloadFileSecondsRemaining(DownloadTaskProxy* P_0, int iFile)
	{
		int result = -1;
		if (((int*)P_0)[4] != 0)
		{
			int num = 0;
			int num2 = ((int*)P_0)[4];
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int*, int>)(int)(*(uint*)(*(int*)num2 + 48)))((IntPtr)num2, iFile, &num) >= 0)
			{
				result = num;
			}
		}
		return result;
	}

	internal unsafe static string Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetTempFileName(DownloadTaskProxy* P_0, int iFile)
	{
		string result = null;
		if (((int*)P_0)[4] != 0)
		{
			ushort* ptr = null;
			int num = ((int*)P_0)[4];
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort**, int>)(int)(*(uint*)(*(int*)num + 40)))((IntPtr)num, iFile, &ptr) >= 0)
			{
				result = new string((char*)ptr);
			}
			if (ptr != null)
			{
				SysFreeString(ptr);
			}
		}
		return result;
	}

	internal unsafe static ulong Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetFinalFileSize(DownloadTaskProxy* P_0, int iFile)
	{
		ulong result = 0uL;
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ulong*, int>)(int)(*(uint*)(*(int*)num + 44)))((IntPtr)num, iFile, &result) < 0)
			{
				result = 0uL;
			}
		}
		return result;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetDownloadBytesPerSecond(DownloadTaskProxy* P_0)
	{
		int result = -1;
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)num + 64)))((IntPtr)num);
		}
		return result;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EDownloadBegan(DownloadTaskProxy* P_0, IDownloadTask* pDLTask)
	{
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EDownloadComplete(DownloadTaskProxy* P_0, IDownloadTask* pDLTask)
	{
		DownloadEventArguments args = new DownloadEventArguments(100f, EDownloadTaskState.DLTaskComplete, 0u);
		DownloadProgressHandler downloadProgressHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_002EP_0024AAVDownloadProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*)((byte*)P_0 + 12));
		if (downloadProgressHandler != null)
		{
			downloadProgressHandler(args);
		}
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTaskProgress*, int>)(int)(*(uint*)(*(int*)num + 108)))((IntPtr)num, (IDownloadTaskProgress*)P_0);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EDownloadFailed(DownloadTaskProxy* P_0, IDownloadTask* pDLTask, int hrFailure, float fltRemainingPercent)
	{
		DownloadEventArguments args = ((-2147467260 != hrFailure) ? new DownloadEventArguments(0f, EDownloadTaskState.DLTaskFailed, (uint)hrFailure) : new DownloadEventArguments(0f, EDownloadTaskState.DLTaskCancelled, 0u));
		DownloadProgressHandler downloadProgressHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_002EP_0024AAVDownloadProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*)((byte*)P_0 + 12));
		if (downloadProgressHandler != null)
		{
			downloadProgressHandler(args);
		}
		if (((int*)P_0)[4] != 0)
		{
			int num = ((int*)P_0)[4];
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTaskProgress*, int>)(int)(*(uint*)(*(int*)num + 108)))((IntPtr)num, (IDownloadTaskProgress*)P_0);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002ERedirected(DownloadTaskProxy* P_0, IDownloadTask* pDLTask, int iFile, ushort* pwszNewURL)
	{
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EProgress(DownloadTaskProxy* P_0, IDownloadTask* pDLTask, float fltPercent, float fltPercentDelta)
	{
		DownloadEventArguments args = new DownloadEventArguments(fltPercent, EDownloadTaskState.DLTaskDownloading, 0u);
		DownloadProgressHandler downloadProgressHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_002EP_0024AAVDownloadProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E*)((byte*)P_0 + 12));
		if (downloadProgressHandler != null)
		{
			downloadProgressHandler(args);
		}
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EGetCredential(DownloadTaskProxy* P_0, IDownloadTask* pDownloadTask, ushort* pwszTargetUrl, EAuthenticationScheme eAuthenticationScheme, int fIsAuthSchemeSafe, ushort* pwszRealm, int iRetryCount, ICredential** ppCredential)
	{
		*(int*)ppCredential = 0;
		return 1;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EQueryInterface(DownloadTaskProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 618u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_60fcb6b3_8562_4ddf_99f8_b93c08ed5e83)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002EAddRef(DownloadTaskProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002ERelease(DownloadTaskProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 1);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002EUtil_002EDownloadTaskProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static DownloadManagerProxy* Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002E_007Bctor_007D(DownloadManagerProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[6] = 1;
		DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bctor_007D((DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)((byte*)P_0 + 28));
		try
		{
			((int*)P_0)[11] = 0;
			try
			{
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EResetCounts(P_0);
				InitializeCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 48));
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDownloadManager_003E*, void>)(&CComPtrNtv_003CIDownloadManager_003E_002E_007Bdtor_007D), (byte*)P_0 + 44);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*, void>)(&DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bdtor_007D), (byte*)P_0 + 28);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002E_007Bdtor_007D(DownloadManagerProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DownloadManagerProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			DownloadManagerProxy* ptr;
			try
			{
				ptr = (DownloadManagerProxy*)((byte*)P_0 + 44);
				CComPtrNtv_003CIDownloadManager_003E_002ERelease((CComPtrNtv_003CIDownloadManager_003E*)ptr);
				DeleteCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 48));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDownloadManager_003E*, void>)(&CComPtrNtv_003CIDownloadManager_003E_002E_007Bdtor_007D), (byte*)P_0 + 44);
				throw;
			}
			CComPtrNtv_003CIDownloadManager_003E_002ERelease((CComPtrNtv_003CIDownloadManager_003E*)ptr);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*, void>)(&DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bdtor_007D), (byte*)P_0 + 28);
			throw;
		}
		DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bdtor_007D((DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)((byte*)P_0 + 28));
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EInitialize(DownloadManagerProxy* P_0)
	{
		DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 44);
		int num;
		if (*(int*)ptr == 0)
		{
			num = GetSingleton((_GUID)_GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)ptr);
			if (num < 0)
			{
				goto IL_002a;
			}
		}
		int num2 = *(int*)ptr;
		num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadTaskProgress*, int>)(int)(*(uint*)(*(int*)num2 + 76)))((IntPtr)num2, (IDownloadTaskProgress*)P_0);
		goto IL_002a;
		IL_002a:
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EResetCounts(DownloadManagerProxy* P_0)
	{
		((int*)P_0)[1] = 0;
		((int*)P_0)[2] = 0;
		((int*)P_0)[3] = 0;
		((int*)P_0)[4] = 0;
		((float*)P_0)[5] = 0f;
		((float*)P_0)[18] = 0f;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EGetQueuePosition(DownloadManagerProxy* P_0, IDownloadTask* pDLTask)
	{
		int num = -1;
		int num2 = ((int*)P_0)[11];
		int num3 = num2;
		int num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)num3 + 44)))((IntPtr)num3);
		int num5 = 0;
		if (0 < num4)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIDownloadTask_003E cComPtrNtv_003CIDownloadTask_003E);
			do
			{
				*(int*)(&cComPtrNtv_003CIDownloadTask_003E) = 0;
				try
				{
					num2 = ((int*)P_0)[11];
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IDownloadTask**, int>)(int)(*(uint*)(*(int*)num2 + 52)))((IntPtr)num2, num5, (IDownloadTask**)(&cComPtrNtv_003CIDownloadTask_003E)) >= 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EDownloadType>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDownloadTask_003E)) + 72)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDownloadTask_003E))) == (EDownloadType)0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, EDownloadTaskState>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDownloadTask_003E)) + 100)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDownloadTask_003E)), null) != EDownloadTaskState.DLTaskComplete)
					{
						num++;
						if ((IDownloadTask*)(*(int*)(&cComPtrNtv_003CIDownloadTask_003E)) == pDLTask)
						{
							goto IL_0093;
						}
					}
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDownloadTask_003E*, void>)(&CComPtrNtv_003CIDownloadTask_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIDownloadTask_003E);
					throw;
				}
				CComPtrNtv_003CIDownloadTask_003E_002ERelease(&cComPtrNtv_003CIDownloadTask_003E);
				num5++;
				continue;
				IL_0093:
				CComPtrNtv_003CIDownloadTask_003E_002ERelease(&cComPtrNtv_003CIDownloadTask_003E);
				break;
			}
			while (num5 < num4);
		}
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EDownloadBegan(DownloadManagerProxy* P_0, IDownloadTask* pDLTask)
	{
		if (pDLTask == null)
		{
			return;
		}
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EDownloadType>)(int)(*(uint*)(*(int*)pDLTask + 72)))((nint)pDLTask) == (EDownloadType)0)
		{
			DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 48);
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
			if (((int*)P_0)[1] <= 0)
			{
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EResetCounts(P_0);
			}
			((int*)P_0)[1]++;
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EUpdatePercentage(P_0, 0f);
			int nQueuePosition = Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EGetQueuePosition(P_0, pDLTask);
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(P_0, EDownloadManagerUpdateType.TaskAdded, pDLTask, nQueuePosition);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EDownloadComplete(DownloadManagerProxy* P_0, IDownloadTask* pDLTask)
	{
		if (pDLTask != null)
		{
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EDownloadType>)(int)(*(uint*)(*(int*)pDLTask + 72)))((nint)pDLTask) == (EDownloadType)0)
			{
				DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 48);
				EnterCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EDownloadFinished(P_0, pDLTask, 0);
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EUpdatePercentage(P_0, 0f);
				LeaveCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(P_0, EDownloadManagerUpdateType.TaskCompleted, pDLTask, -1);
			}
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EDownloadFailed(DownloadManagerProxy* P_0, IDownloadTask* pDLTask, int hrFailure, float fltRemainingPercent)
	{
		if (pDLTask == null)
		{
			return;
		}
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EDownloadType>)(int)(*(uint*)(*(int*)pDLTask + 72)))((nint)pDLTask) == (EDownloadType)0)
		{
			DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 48);
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EDownloadFinished(P_0, pDLTask, hrFailure);
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EUpdatePercentage(P_0, fltRemainingPercent);
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
			if (-2147467260 == hrFailure)
			{
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(P_0, EDownloadManagerUpdateType.TaskCancelled, pDLTask, -1);
			}
			else
			{
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(P_0, EDownloadManagerUpdateType.TaskFailed, pDLTask, -1);
			}
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EDownloadFinished(DownloadManagerProxy* P_0, IDownloadTask* pDLTask, int hrFailure)
	{
		DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 48);
		EnterCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
		if (pDLTask != null)
		{
			if (hrFailure < 0)
			{
				if (-2147467260 == hrFailure)
				{
					((int*)P_0)[3]++;
				}
				else
				{
					((int*)P_0)[4]++;
				}
			}
			else
			{
				((int*)P_0)[2]++;
			}
			((int*)P_0)[1] += -1;
		}
		LeaveCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERedirected(DownloadManagerProxy* P_0, IDownloadTask* pDLTask, int iFile, ushort* pwszNewURL)
	{
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EProgress(DownloadManagerProxy* P_0, IDownloadTask* pDLTask, float percent, float percentDelta)
	{
		if (pDLTask != null)
		{
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EDownloadType>)(int)(*(uint*)(*(int*)pDLTask + 72)))((nint)pDLTask) == (EDownloadType)0)
			{
				DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 48);
				EnterCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EUpdatePercentage(P_0, percentDelta);
				LeaveCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
				Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(P_0, EDownloadManagerUpdateType.TaskProgressChanged, pDLTask, -1);
			}
		}
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EGetCredential(DownloadManagerProxy* P_0, IDownloadTask* pDownloadTask, ushort* pwszTargetUrl, EAuthenticationScheme eAuthenticationScheme, int fIsAuthSchemeSafe, ushort* pwszRealm, int iRetryCount, ICredential** ppCredential)
	{
		*(int*)ppCredential = 0;
		return 1;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EQueryInterface(DownloadManagerProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 939u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_60fcb6b3_8562_4ddf_99f8_b93c08ed5e83)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EAddRef(DownloadManagerProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 6);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERelease(DownloadManagerProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 6);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EAddDelegate(DownloadManagerProxy* P_0, DownloadManagerUpdateHandler progressHandler)
	{
		Unsafe.SkipInit(out gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E obj);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bctor_007D(&obj, progressHandler);
		try
		{
			DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002EAppendItem((DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)((byte*)P_0 + 28), &obj);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bdtor_007D(&obj);
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERemoveDelegate(DownloadManagerProxy* P_0, DownloadManagerUpdateHandler progressHandler)
	{
		int num = 0;
		if (0 >= ((int*)P_0)[9])
		{
			return;
		}
		DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 28);
		while (!(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_002EP_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_005B_005D((DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)ptr, num)) == progressHandler))
		{
			num++;
			if (num >= ((int*)P_0)[9])
			{
				return;
			}
		}
		DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002ERemoveItem((DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)ptr, num);
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EUpdatePercentage(DownloadManagerProxy* P_0, float fltTaskPercentDelta)
	{
		DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 48);
		EnterCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
		float num = ((int*)P_0)[4] + ((int*)P_0)[3] + ((int*)P_0)[2] + ((int*)P_0)[1];
		DownloadManagerProxy* ptr2;
		if (num == 0f)
		{
			ptr2 = (DownloadManagerProxy*)((byte*)P_0 + 20);
			*(float*)ptr2 = 100f;
		}
		else
		{
			float num2 = (((float*)P_0)[18] = (float)((double)fltTaskPercentDelta + (double)((float*)P_0)[18]));
			ptr2 = (DownloadManagerProxy*)((byte*)P_0 + 20);
			*(float*)ptr2 = (float)((double)(float)(1.0 / (double)num) * (double)num2);
		}
		if ((double)(*(float*)ptr2) > 100.0)
		{
			*(float*)ptr2 = 100f;
		}
		LeaveCriticalSection((_RTL_CRITICAL_SECTION*)ptr);
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EPauseQueue(DownloadManagerProxy* P_0)
	{
		int num = ((int*)P_0)[11];
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)num + 64)))((IntPtr)num) >= 0)
		{
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(P_0, EDownloadManagerUpdateType.QueuePaused, null, -1);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EResumeQueue(DownloadManagerProxy* P_0)
	{
		int num = ((int*)P_0)[11];
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)num + 68)))((IntPtr)num) >= 0)
		{
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(P_0, EDownloadManagerUpdateType.QueueResumed, null, -1);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERemoveCancelledCount(DownloadManagerProxy* P_0)
	{
		int num = ((int*)P_0)[3];
		if (num > 0)
		{
			((int*)P_0)[3] = num - 1;
			if ((double)((float*)P_0)[18] >= 100.0)
			{
				((float*)P_0)[18] = (float)((double)((float*)P_0)[18] - 100.0);
			}
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EUpdatePercentage(P_0, 0f);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERemoveFailedCount(DownloadManagerProxy* P_0)
	{
		int num = ((int*)P_0)[4];
		if (num > 0)
		{
			((int*)P_0)[4] = num - 1;
			if ((double)((float*)P_0)[18] >= 100.0)
			{
				((float*)P_0)[18] = (float)((double)((float*)P_0)[18] - 100.0);
			}
			Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EUpdatePercentage(P_0, 0f);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ENotifyUpdate(DownloadManagerProxy* P_0, EDownloadManagerUpdateType type, IDownloadTask* task, int nQueuePosition)
	{
		DownloadTask task2 = null;
		if (task != null)
		{
			task2 = new DownloadTask(task);
		}
		DownloadManagerUpdateArguments args = new DownloadManagerUpdateArguments(type, task2, nQueuePosition);
		int num = 0;
		if (0 < ((int*)P_0)[9])
		{
			DownloadManagerProxy* ptr = (DownloadManagerProxy*)((byte*)P_0 + 28);
			do
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_002EP_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_005B_005D((DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)ptr, num))(args);
				num++;
			}
			while (num < ((int*)P_0)[9]);
		}
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E* P_0, DownloadProgressHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static DownloadProgressHandler gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E_002E_002EP_0024AAVDownloadProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (DownloadProgressHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIDownloadManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CIDownloadManager_003E* P_0)
	{
		CComPtrNtv_003CIDownloadManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIDownloadManager_003E_002ERelease(CComPtrNtv_003CIDownloadManager_003E* P_0)
	{
		int num = *(int*)P_0;
		IDownloadManager* ptr = (IDownloadManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIDownloadTask_003E_002E_007Bdtor_007D(CComPtrNtv_003CIDownloadTask_003E* P_0)
	{
		CComPtrNtv_003CIDownloadTask_003E_002ERelease(P_0);
	}

	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* P_0, DownloadManagerUpdateHandler t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static DownloadManagerUpdateHandler gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_002EP_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (DownloadManagerUpdateHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIDownloadTask_003E_002ERelease(CComPtrNtv_003CIDownloadTask_003E* P_0)
	{
		int num = *(int*)P_0;
		IDownloadTask* ptr = (IDownloadTask*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bctor_007D(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		((int*)P_0)[2] = 0;
		((int*)P_0)[3] = 0;
		return P_0;
	}

	internal unsafe static void DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bdtor_007D(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7_003F_0024DynamicArray_0040U_003F_0024gcroot_0040P_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040_0040_0040_0040_00406B_0040);
		if (((int*)P_0)[3] > 0)
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* ptr = (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)(int)((uint*)P_0)[1];
			if (ptr != null)
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E__vecDelDtor(ptr, 3u);
			}
		}
		((int*)P_0)[1] = 0;
		((int*)P_0)[2] = 0;
		((int*)P_0)[3] = 0;
	}

	internal unsafe static int DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002ERemoveItem(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0, int iIndex)
	{
		if (iIndex >= 0)
		{
			int num = ((int*)P_0)[2];
			if (iIndex < num)
			{
				int num2 = iIndex;
				if (iIndex < num - 1)
				{
					DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* ptr = (DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)((byte*)P_0 + 4);
					do
					{
						int num3 = num2 * 4 + *(int*)ptr;
						gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)num3, (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)(num3 + 4));
						num2++;
					}
					while (num2 < ((int*)P_0)[2] - 1);
				}
				((int*)P_0)[2] += -1;
				return 0;
			}
		}
		return -2147024809;
	}

	internal unsafe static int DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002EAppendItem(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0, gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* newItem)
	{
		if (((int*)P_0)[2] == ((int*)P_0)[3])
		{
			int num = DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002EGrow(P_0, DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002EGetGrowSize(P_0));
			if (num < 0)
			{
				return num;
			}
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)(((int*)P_0)[2] * 4 + ((int*)P_0)[1]), newItem);
		((int*)P_0)[2]++;
		return 0;
	}

	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_005B_005D(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0, int iIndx)
	{
		uint* ptr = null;
		if (iIndx < 0 || (iIndx >= ((int*)P_0)[2] && iIndx >= ((int*)P_0)[3]))
		{
			*ptr = 0u;
		}
		if (iIndx >= ((int*)P_0)[2])
		{
			((int*)P_0)[2] = iIndx + 1;
		}
		return (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)(iIndx * 4 + ((int*)P_0)[1]);
	}

	internal unsafe static void* DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E__vecDelDtor(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* ptr = (DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 16u, *(int*)ptr, (delegate*<void*, void>)(delegate*<DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*, void>)(&DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static void* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E__vecDelDtor(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* ptr = (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 4u, *(int*)ptr, (delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* P_0, gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* r)
	{
		DownloadManagerUpdateHandler target = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_002EP_0024AAVDownloadManagerUpdateHandler_0040Util_0040Zune_0040Microsoft_0040_0040(r);
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = target;
		return P_0;
	}

	internal unsafe static int DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002EGetGrowSize(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0)
	{
		int num = ((int*)P_0)[3];
		int num2 = num;
		if (num < 50)
		{
			return num2 + 5;
		}
		int num3 = ((int*)P_0)[3];
		if (num3 < 150)
		{
			return num2 + 15;
		}
		return (int)((double)(float)num3 * 0.20000000298023224) + num2;
	}

	internal unsafe static int DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E_002EGrow(DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* P_0, int iNewSize)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* ptr4;
		if (iNewSize != 0)
		{
			uint num = (((uint)iNewSize > 1073741823u) ? uint.MaxValue : ((uint)(iNewSize << 2)));
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* ptr = (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)new_005B_005D((num > 4294967291u) ? uint.MaxValue : (num + 4));
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* ptr3;
			try
			{
				if (ptr != null)
				{
					*(int*)ptr = iNewSize;
					gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* ptr2 = (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)((byte*)ptr + 4);
					__ehvec_ctor(ptr2, 4u, iNewSize, (delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*, gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bctor_007D), (delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bdtor_007D));
					ptr3 = ptr2;
				}
				else
				{
					ptr3 = null;
				}
			}
			catch
			{
				//try-fault
				delete_005B_005D(ptr);
				throw;
			}
			ptr4 = ptr3;
			if (ptr3 == null)
			{
				return -2147024882;
			}
			int num2 = 0;
			if (0 < ((int*)P_0)[2])
			{
				DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E* ptr5 = (DynamicArray_003Cgcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_0020_003E*)((byte*)P_0 + 4);
				do
				{
					gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)(num2 * 4 + (byte*)ptr3), (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)(*(int*)ptr5 + num2 * 4));
					num2++;
				}
				while (num2 < ((int*)P_0)[2]);
			}
		}
		else
		{
			ptr4 = null;
		}
		if (((int*)P_0)[3] > 0)
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* ptr6 = (gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E*)(int)((uint*)P_0)[1];
			if (ptr6 != null)
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E__vecDelDtor(ptr6, 3u);
			}
			((int*)P_0)[1] = 0;
		}
		((int*)P_0)[1] = (int)ptr4;
		((int*)P_0)[3] = iNewSize;
		return 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ADownloadManagerUpdateHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	internal unsafe static EndpointHostManagerMediator* EndpointHostManagerMediator_002E_007Bctor_007D(EndpointHostManagerMediator* P_0, DeviceList pDeviceList)
	{
		EndpointHostManagerMediator* ptr = (EndpointHostManagerMediator*)((byte*)P_0 + 4);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7IEndpointHostManagerCallback_0040_00406B_0040);
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7EndpointHostManagerMediator_0040_00406BIEndpointNotification_0040_0040_0040);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7EndpointHostManagerMediator_0040_00406BIEndpointHostManagerCallback_0040_0040_0040);
		((int*)P_0)[2] = 0;
		gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)((byte*)P_0 + 12), pDeviceList);
		try
		{
			EndpointHostManagerMediator* ptr2 = (EndpointHostManagerMediator*)((byte*)P_0 + 16);
			*(int*)ptr2 = 0;
			try
			{
				((int*)P_0)[5] = 0;
				((int*)P_0)[6] = 0;
				if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
				{
					WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 10, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
				}
				int num = GetSingleton((_GUID)_GUID_0a3d3343_00d9_4c61_9a86_2d778793e05f, (void**)ptr2);
				if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
				{
					WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 11, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
				}
				if (num >= 0 && *(int*)ptr2 == 0)
				{
					num = -2147418113;
				}
				((int*)P_0)[7] = num;
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostManager_003E*, void>)(&CComPtrNtv_003CIEndpointHostManager_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
	}

	internal unsafe static void EndpointHostManagerMediator_002E_007Bdtor_007D(EndpointHostManagerMediator* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7EndpointHostManagerMediator_0040_00406BIEndpointNotification_0040_0040_0040);
		((int*)P_0)[1] = (int)Unsafe.AsPointer(ref _003F_003F_7EndpointHostManagerMediator_0040_00406BIEndpointHostManagerCallback_0040_0040_0040);
		try
		{
			try
			{
				if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control))
				{
					if ((((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 12, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
					}
					if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 13, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
					}
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostManager_003E*, void>)(&CComPtrNtv_003CIEndpointHostManager_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
				throw;
			}
			CComPtrNtv_003CIEndpointHostManager_003E_002ERelease((CComPtrNtv_003CIEndpointHostManager_003E*)((byte*)P_0 + 16));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
		gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)((byte*)P_0 + 12));
	}

	internal unsafe static void EndpointHostManagerMediator_002EShutdown(EndpointHostManagerMediator* P_0)
	{
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 14, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
		}
		EndpointHostManagerMediator* ptr = (EndpointHostManagerMediator*)((byte*)P_0 + 16);
		int num = *(int*)ptr;
		if (num != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)num + 24)))((IntPtr)num, ((uint*)P_0)[5]);
			((int*)P_0)[5] = 0;
		}
		CComPtrNtv_003CIEndpointHostManager_003E_002ERelease((CComPtrNtv_003CIEndpointHostManager_003E*)ptr);
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 15, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
		}
	}

	internal unsafe static int EndpointHostManagerMediator_002EInitializeAndEnumerate(EndpointHostManagerMediator* P_0)
	{
		int num = ((int*)P_0)[7];
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 16, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
		}
		if (num >= 0)
		{
			EndpointHostManagerMediator* ptr = (EndpointHostManagerMediator*)((byte*)P_0 + 4);
			int num2 = ((int*)P_0)[4];
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IEndpointHostManagerCallback*, uint*, int>)(int)(*(uint*)(*(int*)num2 + 12)))((IntPtr)num2, (IEndpointHostManagerCallback*)ptr, (uint*)P_0 + 6);
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
			{
				WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 17, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
			}
			if (num >= 0)
			{
				int num3 = ((int*)P_0)[4];
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IUnknown*, uint*, int>)(int)(*(uint*)(*(int*)num3 + 20)))((IntPtr)num3, (IUnknown*)P_0, (uint*)P_0 + 5);
				if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
				{
					WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 18, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
				}
				if (num >= 0)
				{
					IEndpointHostManager* endpointHostManager = (IEndpointHostManager*)(int)((uint*)P_0)[4];
					gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)((byte*)P_0 + 12)).SetEndpointHostManager(endpointHostManager);
					int num4 = ((int*)P_0)[4];
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)num4 + 32)))((IntPtr)num4);
					if (WPP_GLOBAL_Control == Unsafe.AsPointer(ref WPP_GLOBAL_Control))
					{
						goto IL_019a;
					}
					if ((((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 19, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
					}
				}
			}
		}
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 20, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
		}
		goto IL_019a;
		IL_019a:
		return num;
	}

	internal unsafe static int EndpointHostManagerMediator_002EGetEndpointHostFromEndpointId(EndpointHostManagerMediator* P_0, ushort* pwszEndpointId, IEndpointHost** ppEndpointHost)
	{
		if (pwszEndpointId == null)
		{
			_ZuneShipAssert(1001u, 132u);
			return -2147467261;
		}
		if (ppEndpointHost == null)
		{
			_ZuneShipAssert(1001u, 133u);
			return -2147467261;
		}
		if (((int*)P_0)[4] == 0)
		{
			_ZuneShipAssert(1002u, 134u);
			return -2147418113;
		}
		*(int*)ppEndpointHost = 0;
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 21, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
		}
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002EP_0024AAVDeviceList_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)((byte*)P_0 + 12)) == null)
		{
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 2u)
			{
				WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 22, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
			}
			return -2147418113;
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIEndpointHostCollection_003E cComPtrNtv_003CIEndpointHostCollection_003E);
		*(int*)(&cComPtrNtv_003CIEndpointHostCollection_003E) = 0;
		int num2;
		try
		{
			int num = ((int*)P_0)[4];
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, IEndpointHostCollection**, int>)(int)(*(uint*)(*(int*)num + 36)))((IntPtr)num, 0, (IEndpointHostCollection**)(&cComPtrNtv_003CIEndpointHostCollection_003E));
			if (*(int*)(&cComPtrNtv_003CIEndpointHostCollection_003E) == 0)
			{
				_ZuneShipAssert(1002u, 152u);
				goto IL_0114;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostCollection_003E*, void>)(&CComPtrNtv_003CIEndpointHostCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHostCollection_003E);
			throw;
		}
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIEndpointHost_003E cComPtrNtv_003CIEndpointHost_003E);
			*(int*)(&cComPtrNtv_003CIEndpointHost_003E) = 0;
			try
			{
				if (num2 >= 0)
				{
					num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, IEndpointHost**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHostCollection_003E)) + 20)))((IntPtr)(*(int*)(&cComPtrNtv_003CIEndpointHostCollection_003E)), pwszEndpointId, (IEndpointHost**)(&cComPtrNtv_003CIEndpointHost_003E));
					if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 23, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num2);
					}
					if (num2 >= 0)
					{
						if (*(int*)(&cComPtrNtv_003CIEndpointHost_003E) == 0)
						{
							num2 = -2147418113;
						}
						if (num2 >= 0)
						{
							IEndpointHost* ptr = (IEndpointHost*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHost_003E));
							*(int*)(&cComPtrNtv_003CIEndpointHost_003E) = 0;
							*(int*)ppEndpointHost = (int)ptr;
						}
					}
				}
				if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
				{
					WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 24, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num2);
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHost_003E);
				throw;
			}
			CComPtrNtv_003CIEndpointHost_003E_002ERelease(&cComPtrNtv_003CIEndpointHost_003E);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostCollection_003E*, void>)(&CComPtrNtv_003CIEndpointHostCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHostCollection_003E);
			throw;
		}
		CComPtrNtv_003CIEndpointHostCollection_003E_002ERelease(&cComPtrNtv_003CIEndpointHostCollection_003E);
		return num2;
		IL_0114:
		CComPtrNtv_003CIEndpointHostCollection_003E_002ERelease(&cComPtrNtv_003CIEndpointHostCollection_003E);
		return -2147418113;
	}

	internal unsafe static int EndpointHostManagerMediator_002EOnEndpointArrival(EndpointHostManagerMediator* P_0, ushort* pwszEndpointId)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIEndpointHost_003E cComPtrNtv_003CIEndpointHost_003E);
		*(int*)(&cComPtrNtv_003CIEndpointHost_003E) = 0;
		int num;
		try
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, IEndpointHost**, int>)(int)(*(uint*)(*(int*)P_0 + 20)))((nint)P_0, pwszEndpointId, (IEndpointHost**)(&cComPtrNtv_003CIEndpointHost_003E));
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
			{
				WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 25, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
			}
			if (*(int*)(&cComPtrNtv_003CIEndpointHost_003E) == 0)
			{
				_ZuneShipAssert(1002u, 204u);
				goto IL_0075;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHost_003E);
			throw;
		}
		try
		{
			if (num < 0)
			{
				goto IL_00d9;
			}
			num = gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)((byte*)P_0 + 12)).DeviceArrived((IEndpointHost*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHost_003E)));
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control))
			{
				if ((((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
				{
					WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 26, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
				}
				goto IL_00d9;
			}
			goto end_IL_0083;
			IL_00d9:
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
			{
				WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 27, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
			}
			end_IL_0083:;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHost_003E);
			throw;
		}
		CComPtrNtv_003CIEndpointHost_003E_002ERelease(&cComPtrNtv_003CIEndpointHost_003E);
		return num;
		IL_0075:
		CComPtrNtv_003CIEndpointHost_003E_002ERelease(&cComPtrNtv_003CIEndpointHost_003E);
		return -2147418113;
	}

	internal unsafe static int EndpointHostManagerMediator_002EOnEndpointRemoval(EndpointHostManagerMediator* P_0, ushort* pwszEndpointId)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIEndpointHost_003E cComPtrNtv_003CIEndpointHost_003E);
		*(int*)(&cComPtrNtv_003CIEndpointHost_003E) = 0;
		int num;
		try
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, IEndpointHost**, int>)(int)(*(uint*)(*(int*)P_0 + 20)))((nint)P_0, pwszEndpointId, (IEndpointHost**)(&cComPtrNtv_003CIEndpointHost_003E));
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
			{
				WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 28, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
			}
			if (*(int*)(&cComPtrNtv_003CIEndpointHost_003E) == 0)
			{
				_ZuneShipAssert(1002u, 235u);
				goto IL_0075;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHost_003E);
			throw;
		}
		try
		{
			if (num < 0)
			{
				goto IL_00d9;
			}
			num = gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)((byte*)P_0 + 12)).DeviceDisconnected((IEndpointHost*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHost_003E)));
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control))
			{
				if ((((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
				{
					WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 29, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
				}
				goto IL_00d9;
			}
			goto end_IL_0083;
			IL_00d9:
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
			{
				WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 30, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), num);
			}
			end_IL_0083:;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHost_003E);
			throw;
		}
		CComPtrNtv_003CIEndpointHost_003E_002ERelease(&cComPtrNtv_003CIEndpointHost_003E);
		return num;
		IL_0075:
		CComPtrNtv_003CIEndpointHost_003E_002ERelease(&cComPtrNtv_003CIEndpointHost_003E);
		return -2147418113;
	}

	internal unsafe static void EndpointHostManagerMediator_002EEndpointHostCollectionsAvailable(EndpointHostManagerMediator* P_0, int hrResult)
	{
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_d(((ulong*)WPP_GLOBAL_Control)[6], 31, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), hrResult);
		}
		if (hrResult < 0)
		{
			return;
		}
		if (((int*)P_0)[3] == 0)
		{
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 2u)
			{
				WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 32, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
			}
			return;
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIEndpointHostCollection_003E cComPtrNtv_003CIEndpointHostCollection_003E);
		*(int*)(&cComPtrNtv_003CIEndpointHostCollection_003E) = 0;
		int num3;
		try
		{
			uint num = ((uint*)P_0)[3];
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)(int)num + 16)))((IntPtr)(int)num, ((uint*)P_0)[5]);
			((int*)P_0)[5] = 0;
			uint num2 = ((uint*)P_0)[3];
			num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, IEndpointHostCollection**, int>)(int)(*(uint*)(*(int*)(int)num2 + 36)))((IntPtr)(int)num2, 0, (IEndpointHostCollection**)(&cComPtrNtv_003CIEndpointHostCollection_003E));
			if (*(int*)(&cComPtrNtv_003CIEndpointHostCollection_003E) == 0)
			{
				if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 2u)
				{
					WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 33, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
				}
				goto IL_012e;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostCollection_003E*, void>)(&CComPtrNtv_003CIEndpointHostCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHostCollection_003E);
			throw;
		}
		try
		{
			uint num4 = 0u;
			if (num3 >= 0)
			{
				num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHostCollection_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIEndpointHostCollection_003E)), &num4);
				if (num3 < 0)
				{
					num4 = 0u;
				}
				if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
				{
					WPP_SF_dd(((ulong*)WPP_GLOBAL_Control)[6], 34, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids), (int)num4, num3);
				}
			}
			EndpointHostManagerMediator* ptr = (EndpointHostManagerMediator*)((byte*)P_0 + 8);
			gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)ptr).Initialized = true;
			uint num5 = 0u;
			if (0 < num4)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIEndpointHost_003E cComPtrNtv_003CIEndpointHost_003E);
				do
				{
					*(int*)(&cComPtrNtv_003CIEndpointHost_003E) = 0;
					try
					{
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, IEndpointHost**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHostCollection_003E)) + 16)))((IntPtr)(*(int*)(&cComPtrNtv_003CIEndpointHostCollection_003E)), num5, (IEndpointHost**)(&cComPtrNtv_003CIEndpointHost_003E)) >= 0)
						{
							gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E*)ptr).DeviceArrived((IEndpointHost*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHost_003E)));
						}
					}
					catch
					{
						//try-fault
						___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHost_003E);
						throw;
					}
					CComPtrNtv_003CIEndpointHost_003E_002ERelease(&cComPtrNtv_003CIEndpointHost_003E);
					num5++;
				}
				while (num5 < num4);
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostCollection_003E*, void>)(&CComPtrNtv_003CIEndpointHostCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHostCollection_003E);
			throw;
		}
		CComPtrNtv_003CIEndpointHostCollection_003E_002ERelease(&cComPtrNtv_003CIEndpointHostCollection_003E);
		return;
		IL_012e:
		CComPtrNtv_003CIEndpointHostCollection_003E_002ERelease(&cComPtrNtv_003CIEndpointHostCollection_003E);
	}

	internal unsafe static int EndpointHostManagerMediator_002EQueryInterface(EndpointHostManagerMediator* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_53baba84_a16e_4fbd_a84a_428297ecff07)) == 0)
		{
			if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_d8b0e1de_b8fe_4aec_bfd9_e707d8d65e1e)) != 0)
			{
				EndpointHostManagerMediator* ptr = (EndpointHostManagerMediator*)((P_0 == null) ? null : ((byte*)P_0 + 4));
				*(int*)ppInterface = (int)ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			}
			else
			{
				*(int*)ppInterface = 0;
				result = -2147467262;
			}
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint EndpointHostManagerMediator_002EAddRef(EndpointHostManagerMediator* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 2);
	}

	internal unsafe static uint EndpointHostManagerMediator_002ERelease(EndpointHostManagerMediator* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 2);
		if (num == 0 && P_0 != null)
		{
			EndpointHostManagerMediator_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static DeviceMediator* DeviceMediator_002E_007Bctor_007D(DeviceMediator* P_0, Device pDevice, IEndpointHost* pEndpointHost)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 4);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7IEndpointStatusCallback_0040_00406B_0040);
		DeviceMediator* ptr2 = (DeviceMediator*)((byte*)P_0 + 12);
		*(int*)ptr2 = (int)Unsafe.AsPointer(ref _003F_003F_7IDeviceProgress_0040DeviceAccess_0040_00406B_0040);
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BISyncProgressCallback_0040_0040_0040);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BIEndpointStatusCallback_0040_0040_0040);
		DeviceMediator* ptr3 = (DeviceMediator*)((byte*)P_0 + 8);
		*(int*)ptr3 = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BIZuneWlanCallback_0040_0040_0040);
		*(int*)ptr2 = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BIDeviceProgress_0040DeviceAccess_0040_0040_0040);
		((int*)P_0)[4] = 0;
		gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)((byte*)P_0 + 20), pDevice);
		try
		{
			DeviceMediator* ptr4 = (DeviceMediator*)((byte*)P_0 + 24);
			CComPtrNtv_003CIEndpointHost_003E_002E_007Bctor_007D((CComPtrNtv_003CIEndpointHost_003E*)ptr4, pEndpointHost);
			try
			{
				DeviceMediator* ptr5 = (DeviceMediator*)((byte*)P_0 + 28);
				*(int*)ptr5 = 0;
				try
				{
					if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 35, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
					}
					int num = *(int*)ptr4;
					if (num != 0)
					{
						bool flag = false;
						int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)num + 76)))((IntPtr)num, EEndpointHostProperty.eEndpointHostPropertyIsAvailable, &flag);
						if (num2 >= 0 && flag)
						{
							Unsafe.SkipInit(out CComPtrNtv_003CISyncEngine_003E cComPtrNtv_003CISyncEngine_003E);
							*(int*)(&cComPtrNtv_003CISyncEngine_003E) = 0;
							try
							{
								num2 = GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020ISyncEngine_003E((IEndpointHost*)(int)(*(uint*)ptr4), EEndpointHostProperty.eEndpointHostPropertySyncEngine, (ISyncEngine**)(&cComPtrNtv_003CISyncEngine_003E));
								if (num2 >= 0)
								{
									CComPtrNtv_003CISyncEngine_003E_002E_003D((CComPtrNtv_003CISyncEngine_003E*)ptr5, &cComPtrNtv_003CISyncEngine_003E);
									int num3 = *(int*)ptr5;
									((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISyncProgressCallback*, int>)(int)(*(uint*)(*(int*)num3 + 60)))((IntPtr)num3, (ISyncProgressCallback*)P_0);
								}
							}
							catch
							{
								//try-fault
								___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISyncEngine_003E*, void>)(&CComPtrNtv_003CISyncEngine_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISyncEngine_003E);
								throw;
							}
							CComPtrNtv_003CISyncEngine_003E_002ERelease(&cComPtrNtv_003CISyncEngine_003E);
						}
						DeviceMediator* ptr6 = ptr;
						num = *(int*)ptr4;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IEndpointStatusCallback*, uint*, int>)(int)(*(uint*)(*(int*)num + 12)))((IntPtr)num, (IEndpointStatusCallback*)ptr6, (uint*)P_0 + 8);
						Unsafe.SkipInit(out CComPtrNtv_003CIWlanProvider_003E cComPtrNtv_003CIWlanProvider_003E);
						*(int*)(&cComPtrNtv_003CIWlanProvider_003E) = 0;
						try
						{
							if (GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IWlanProvider_003E((IEndpointHost*)(int)(*(uint*)ptr4), EEndpointHostProperty.eEndpointHostPropertyWlanProvider, (IWlanProvider**)(&cComPtrNtv_003CIWlanProvider_003E)) >= 0 && *(int*)(&cComPtrNtv_003CIWlanProvider_003E) != 0)
							{
								DeviceMediator* ptr7 = ptr3;
								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IZuneWlanCallback*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIWlanProvider_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIWlanProvider_003E)), (IZuneWlanCallback*)ptr7);
							}
						}
						catch
						{
							//try-fault
							___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWlanProvider_003E*, void>)(&CComPtrNtv_003CIWlanProvider_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWlanProvider_003E);
							throw;
						}
						CComPtrNtv_003CIWlanProvider_003E_002ERelease(&cComPtrNtv_003CIWlanProvider_003E);
					}
					if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 36, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
					}
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISyncEngine_003E*, void>)(&CComPtrNtv_003CISyncEngine_003E_002E_007Bdtor_007D), (byte*)P_0 + 28);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), (byte*)P_0 + 24);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
			throw;
		}
		return P_0;
	}

	internal unsafe static void DeviceMediator_002E_007Bdtor_007D(DeviceMediator* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BISyncProgressCallback_0040_0040_0040);
		((int*)P_0)[1] = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BIEndpointStatusCallback_0040_0040_0040);
		((int*)P_0)[2] = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BIZuneWlanCallback_0040_0040_0040);
		((int*)P_0)[3] = (int)Unsafe.AsPointer(ref _003F_003F_7DeviceMediator_0040_00406BIDeviceProgress_0040DeviceAccess_0040_0040_0040);
		try
		{
			try
			{
				try
				{
					if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 37, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
					}
					DeviceMediator_002EShutdown(P_0);
					if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
					{
						WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 38, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
					}
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISyncEngine_003E*, void>)(&CComPtrNtv_003CISyncEngine_003E_002E_007Bdtor_007D), (byte*)P_0 + 28);
					throw;
				}
				CComPtrNtv_003CISyncEngine_003E_002ERelease((CComPtrNtv_003CISyncEngine_003E*)((byte*)P_0 + 28));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHost_003E*, void>)(&CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D), (byte*)P_0 + 24);
				throw;
			}
			CComPtrNtv_003CIEndpointHost_003E_002ERelease((CComPtrNtv_003CIEndpointHost_003E*)((byte*)P_0 + 24));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
			throw;
		}
		gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)((byte*)P_0 + 20));
	}

	internal unsafe static void DeviceMediator_002EShutdown(DeviceMediator* P_0)
	{
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 39, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
		}
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 28);
		int num = *(int*)ptr;
		if (num != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISyncProgressCallback*, int>)(int)(*(uint*)(*(int*)num + 64)))((IntPtr)num, (ISyncProgressCallback*)P_0);
		}
		DeviceMediator* ptr2 = (DeviceMediator*)((byte*)P_0 + 24);
		int num2 = *(int*)ptr2;
		if (num2 != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)num2 + 16)))((IntPtr)num2, ((uint*)P_0)[8]);
			Unsafe.SkipInit(out CComPtrNtv_003CIWlanProvider_003E cComPtrNtv_003CIWlanProvider_003E);
			*(int*)(&cComPtrNtv_003CIWlanProvider_003E) = 0;
			try
			{
				if (GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IWlanProvider_003E((IEndpointHost*)(int)(*(uint*)ptr2), EEndpointHostProperty.eEndpointHostPropertyWlanProviderIfAvailable, (IWlanProvider**)(&cComPtrNtv_003CIWlanProvider_003E)) >= 0 && *(int*)(&cComPtrNtv_003CIWlanProvider_003E) != 0)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IZuneWlanCallback*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIWlanProvider_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIWlanProvider_003E)), null);
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWlanProvider_003E*, void>)(&CComPtrNtv_003CIWlanProvider_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWlanProvider_003E);
				throw;
			}
			CComPtrNtv_003CIWlanProvider_003E_002ERelease(&cComPtrNtv_003CIWlanProvider_003E);
		}
		CComPtrNtv_003CISyncEngine_003E_002ERelease((CComPtrNtv_003CISyncEngine_003E*)ptr);
		CComPtrNtv_003CIEndpointHost_003E_002ERelease((CComPtrNtv_003CIEndpointHost_003E*)ptr2);
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[6], 40, (_GUID*)Unsafe.AsPointer(ref _003FA0x1fa77c7f_002EWPP_EndpointHostManagerMediator_cpp_Traceguids));
		}
	}

	internal unsafe static int DeviceMediator_002ESyncBegin(DeviceMediator* P_0)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 20);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) == null)
		{
			return -2147418113;
		}
		return gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).SyncBeginCallback();
	}

	internal unsafe static int DeviceMediator_002ESyncProgressed(DeviceMediator* P_0, uint uPercentComplete, uint uPercentItemComplete, uint uPercentTranscodeComplete, ushort* bstrGroup, ushort* bstrTitle, int nEngineState)
	{
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)((byte*)P_0 + 20)) == null)
		{
			return -2147418113;
		}
		return gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)((byte*)P_0 + 20)).SyncProgressCallback(uPercentComplete, uPercentItemComplete, uPercentTranscodeComplete, bstrGroup, bstrTitle, (ESyncEngineState)nEngineState);
	}

	internal unsafe static int DeviceMediator_002ESyncComplete(DeviceMediator* P_0, int hrSync)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 20);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) == null)
		{
			return -2147418113;
		}
		return gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).SyncCompleteCallback(hrSync);
	}

	internal unsafe static void DeviceMediator_002EFriendlyNameChanged(DeviceMediator* P_0, ushort* pwszFriendlyName)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 16);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).FriendlyNameChanged(pwszFriendlyName);
		}
	}

	internal unsafe static void DeviceMediator_002EEndpointStatusChanged(DeviceMediator* P_0, int hrEnumeration, ushort* pwszEndpointId, int nNewStatus)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 16);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).EndpointStatusChanged(hrEnumeration, (EEndpointStatus)nNewStatus);
		}
	}

	internal unsafe static int DeviceMediator_002EGetWlanProfilesComplete(DeviceMediator* P_0, int hr)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 12);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).GetWlanProfilesComplete(hr);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002EGetDeviceWlanNetworksComplete(DeviceMediator* P_0, int hr)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 12);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).GetDeviceWlanNetworksComplete(hr);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002EGetDeviceWlanProfilesComplete(DeviceMediator* P_0, int hr)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 12);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).GetDeviceWlanProfilesComplete(hr);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002ESetDeviceWlanProfilesComplete(DeviceMediator* P_0, int hr)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 12);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).SetDeviceWlanProfilesComplete(hr);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002EAssociateWlanDeviceComplete(DeviceMediator* P_0, int hr)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 12);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).AssociateWlanDeviceComplete(hr);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002EUnassociateWlanDeviceComplete(DeviceMediator* P_0, int hr)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 12);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).UnassociateWlanDeviceComplete(hr);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002ETestDeviceWlanComplete(DeviceMediator* P_0, int hr)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 12);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).TestDeviceWlanComplete(hr);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002EBegin(DeviceMediator* P_0, EProgressEventType eEventType, void* pvContext, int* pfCancelled)
	{
		return 0;
	}

	internal unsafe static int DeviceMediator_002EEnd(DeviceMediator* P_0, EProgressEventType eEventType, int hrCompletionCode)
	{
		DeviceMediator* ptr = (DeviceMediator*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E*)ptr).FormatComplete(hrCompletionCode);
		}
		return 0;
	}

	internal unsafe static int DeviceMediator_002EProgress(DeviceMediator* P_0, EProgressEventType eEventType, uint dwPercentComplete, int* pfCancelled)
	{
		return 0;
	}

	internal unsafe static int DeviceMediator_002EQueryInterface(DeviceMediator* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_85e3445b_c6b2_4066_b714_35fe2ddf04d6)) == 0)
		{
			if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_d62cd97d_679e_4aa9_aac4_6eadf638e3b4)) != 0)
			{
				DeviceMediator* ptr = (DeviceMediator*)((P_0 == null) ? null : ((byte*)P_0 + 4));
				*(int*)ppInterface = (int)ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			}
			else if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_bf820622_ebf9_4508_be2e_022a105fc881)) != 0)
			{
				DeviceMediator* ptr2 = (DeviceMediator*)((P_0 == null) ? null : ((byte*)P_0 + 12));
				*(int*)ppInterface = (int)ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			}
			else
			{
				*(int*)ppInterface = 0;
				result = -2147467262;
			}
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint DeviceMediator_002EAddRef(DeviceMediator* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 4);
	}

	internal unsafe static uint DeviceMediator_002ERelease(DeviceMediator* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 4);
		if (num == 0 && P_0 != null)
		{
			DeviceMediator_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E* P_0, DeviceList t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static DeviceList gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002EP_0024AAVDeviceList_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (DeviceList)((GCHandle)intPtr).Target;
	}

	internal unsafe static DeviceList gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003ADeviceList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (DeviceList)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIEndpointHostManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CIEndpointHostManager_003E* P_0)
	{
		CComPtrNtv_003CIEndpointHostManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIEndpointHostManager_003E_002ERelease(CComPtrNtv_003CIEndpointHostManager_003E* P_0)
	{
		int num = *(int*)P_0;
		IEndpointHostManager* ptr = (IEndpointHostManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E* P_0, Device t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static Device gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002EP_0024AAVDevice_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (Device)((GCHandle)intPtr).Target;
	}

	internal unsafe static Device gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003ADevice_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (Device)((GCHandle)intPtr).Target;
	}

	internal unsafe static CComPtrNtv_003CIEndpointHost_003E* CComPtrNtv_003CIEndpointHost_003E_002E_007Bctor_007D(CComPtrNtv_003CIEndpointHost_003E* P_0, IEndpointHost* lp)
	{
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIEndpointHost_003E_002E_007Bdtor_007D(CComPtrNtv_003CIEndpointHost_003E* P_0)
	{
		CComPtrNtv_003CIEndpointHost_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIEndpointHost_003E_002ERelease(CComPtrNtv_003CIEndpointHost_003E* P_0)
	{
		int num = *(int*)P_0;
		IEndpointHost* ptr = (IEndpointHost*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static ISyncEngine* CComPtrNtv_003CISyncEngine_003E_002E_003D(CComPtrNtv_003CISyncEngine_003E* P_0, CComPtrNtv_003CISyncEngine_003E* lp)
	{
		CComPtrNtv_003CISyncEngine_003E_002ERelease(P_0);
		int num = (*(int*)P_0 = *(int*)lp);
		if (num != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)num + 4)))((IntPtr)num);
		}
		return (ISyncEngine*)(int)(*(uint*)P_0);
	}

	internal unsafe static void CComPtrNtv_003CIEndpointHostCollection_003E_002E_007Bdtor_007D(CComPtrNtv_003CIEndpointHostCollection_003E* P_0)
	{
		CComPtrNtv_003CIEndpointHostCollection_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIEndpointHostCollection_003E_002ERelease(CComPtrNtv_003CIEndpointHostCollection_003E* P_0)
	{
		int num = *(int*)P_0;
		IEndpointHostCollection* ptr = (IEndpointHostCollection*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static uint _003FRelease_0040DeviceMediator_0040_0040_0024_0024FW3AGKXZ(DeviceMediator* P_0)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 4);
		return DeviceMediator_002ERelease(P_0);
	}

	internal unsafe static uint _003FRelease_0040DeviceMediator_0040_0040_0024_0024FW7AGKXZ(DeviceMediator* P_0)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 8);
		return DeviceMediator_002ERelease(P_0);
	}

	internal unsafe static uint _003FRelease_0040EndpointHostManagerMediator_0040_0040_0024_0024FW3AGKXZ(EndpointHostManagerMediator* P_0)
	{
		P_0 = (EndpointHostManagerMediator*)((byte*)P_0 - 4);
		return EndpointHostManagerMediator_002ERelease(P_0);
	}

	internal unsafe static uint _003FRelease_0040DeviceMediator_0040_0040_0024_0024FWM_0040AGKXZ(DeviceMediator* P_0)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 12);
		return DeviceMediator_002ERelease(P_0);
	}

	internal unsafe static uint _003FAddRef_0040DeviceMediator_0040_0040_0024_0024FW3AGKXZ(DeviceMediator* P_0)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 4);
		return DeviceMediator_002EAddRef(P_0);
	}

	internal unsafe static uint _003FAddRef_0040DeviceMediator_0040_0040_0024_0024FW7AGKXZ(DeviceMediator* P_0)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 8);
		return DeviceMediator_002EAddRef(P_0);
	}

	internal unsafe static uint _003FAddRef_0040EndpointHostManagerMediator_0040_0040_0024_0024FW3AGKXZ(EndpointHostManagerMediator* P_0)
	{
		P_0 = (EndpointHostManagerMediator*)((byte*)P_0 - 4);
		return EndpointHostManagerMediator_002EAddRef(P_0);
	}

	internal unsafe static int _003FQueryInterface_0040DeviceMediator_0040_0040_0024_0024FW3AGJABU_GUID_0040_0040PAPAX_0040Z(DeviceMediator* P_0, _GUID* iid, void** ppInterface)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 4);
		return DeviceMediator_002EQueryInterface(P_0, iid, ppInterface);
	}

	internal unsafe static uint _003FAddRef_0040DeviceMediator_0040_0040_0024_0024FWM_0040AGKXZ(DeviceMediator* P_0)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 12);
		return DeviceMediator_002EAddRef(P_0);
	}

	internal unsafe static int _003FQueryInterface_0040DeviceMediator_0040_0040_0024_0024FW7AGJABU_GUID_0040_0040PAPAX_0040Z(DeviceMediator* P_0, _GUID* iid, void** ppInterface)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 8);
		return DeviceMediator_002EQueryInterface(P_0, iid, ppInterface);
	}

	internal unsafe static int _003FQueryInterface_0040DeviceMediator_0040_0040_0024_0024FWM_0040AGJABU_GUID_0040_0040PAPAX_0040Z(DeviceMediator* P_0, _GUID* iid, void** ppInterface)
	{
		P_0 = (DeviceMediator*)((byte*)P_0 - 12);
		return DeviceMediator_002EQueryInterface(P_0, iid, ppInterface);
	}

	internal unsafe static int _003FQueryInterface_0040EndpointHostManagerMediator_0040_0040_0024_0024FW3AGJABU_GUID_0040_0040PAPAX_0040Z(EndpointHostManagerMediator* P_0, _GUID* iid, void** ppInterface)
	{
		P_0 = (EndpointHostManagerMediator*)((byte*)P_0 - 4);
		return EndpointHostManagerMediator_002EQueryInterface(P_0, iid, ppInterface);
	}

	internal unsafe static FeatureChangedInteropWrapper* Microsoft_002EZune_002EUtil_002EFeatureChangedInteropWrapper_002E_007Bctor_007D(FeatureChangedInteropWrapper* P_0, FeaturesChangedHandler asyncCompleteHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7FeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		FeatureChangedInteropWrapper* ptr = (FeatureChangedInteropWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[1] = 0;
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E*)ptr, asyncCompleteHandler);
			IFeatureEnablementManager* ptr2 = null;
			if (GetSingleton((_GUID)_GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr2)) >= 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IAsyncCallback*, int>)(int)(*(uint*)(*(int*)ptr2 + 48)))((nint)ptr2, (IAsyncCallback*)P_0);
			}
			if (null != ptr2)
			{
				IFeatureEnablementManager* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		return P_0;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EFeatureChangedInteropWrapper_002EQueryInterface(FeatureChangedInteropWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EFeatureChangedInteropWrapper_002EAddRef(FeatureChangedInteropWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EFeatureChangedInteropWrapper_002ERelease(FeatureChangedInteropWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0)
		{
			if (P_0 != null)
			{
				*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7FeatureChangedInteropWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E*)((byte*)P_0 + 8));
				delete(P_0);
			}
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EFeatureChangedInteropWrapper_002EOnComplete(FeatureChangedInteropWrapper* P_0, int hr)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		object? target = ((GCHandle)intPtr).Target;
		byte featuresHaveChanged = ((hr == 0) ? ((byte)1) : ((byte)0));
		target(featuresHaveChanged != 0);
		return 0;
	}

	internal unsafe static void CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D(CComPtrNtv_003CITunerConfig_003E* P_0)
	{
		CComPtrNtv_003CITunerConfig_003E_002ERelease(P_0);
	}

	internal unsafe static ITunerConfig* CComPtrNtv_003CITunerConfig_003E_002E_002D_003E(CComPtrNtv_003CITunerConfig_003E* P_0)
	{
		return (ITunerConfig*)(int)(*(uint*)P_0);
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AFeaturesChangedHandler_0020_005E_003E* P_0, FeaturesChangedHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIAsyncCallback_003E_002E_007Bdtor_007D(CComPtrNtv_003CIAsyncCallback_003E* P_0)
	{
		CComPtrNtv_003CIAsyncCallback_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CITunerConfig_003E_002ERelease(CComPtrNtv_003CITunerConfig_003E* P_0)
	{
		int num = *(int*)P_0;
		ITunerConfig* ptr = (ITunerConfig*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIAsyncCallback_003E_002ERelease(CComPtrNtv_003CIAsyncCallback_003E* P_0)
	{
		int num = *(int*)P_0;
		IAsyncCallback* ptr = (IAsyncCallback*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void WPP_SF_dSdl(ulong Logger, ushort id, _GUID* TraceGuid, int _a1, ushort* _a2, int _a3, int _a4)
	{
		//IL_0040->IL0040: Incompatible stack types: I vs Ref
		uint num2;
		if (_a2 != null)
		{
			uint num;
			if (*_a2 == 0)
			{
				num = 14u;
			}
			else
			{
				ushort* ptr = _a2;
				do
				{
					ptr++;
				}
				while (Unsafe.ReadUnaligned<short>(ptr) != 0);
				num = (uint)(((nint)((byte*)ptr - (nuint)_a2) >> 1) * 2 + 2);
			}
			num2 = num;
		}
		else
		{
			num2 = 10u;
		}
		ushort* ptr2 = (ushort*)((_a2 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040) : Unsafe.AsPointer(ref *_a2 == 0 ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY06_0024_0024CBG, _003F>(ref _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)_a2));
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, ptr2, num2, &_a3, 4u, &_a4, 4u, 0));
	}

	internal unsafe static void WPP_SF_dSl(ulong Logger, ushort id, _GUID* TraceGuid, int _a1, ushort* _a2, int _a3)
	{
		//IL_0040->IL0040: Incompatible stack types: I vs Ref
		uint num2;
		if (_a2 != null)
		{
			uint num;
			if (*_a2 == 0)
			{
				num = 14u;
			}
			else
			{
				ushort* ptr = _a2;
				do
				{
					ptr++;
				}
				while (Unsafe.ReadUnaligned<short>(ptr) != 0);
				num = (uint)(((nint)((byte*)ptr - (nuint)_a2) >> 1) * 2 + 2);
			}
			num2 = num;
		}
		else
		{
			num2 = 10u;
		}
		ushort* ptr2 = (ushort*)((_a2 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040) : Unsafe.AsPointer(ref *_a2 == 0 ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY06_0024_0024CBG, _003F>(ref _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)_a2));
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, ptr2, num2, &_a3, 4u, 0));
	}

	internal unsafe static void WPP_SF_dl(ulong Logger, ushort id, _GUID* TraceGuid, int _a1, int _a2)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, 0));
	}

	internal unsafe static void WPP_SF_dld(ulong Logger, ushort id, _GUID* TraceGuid, int _a1, int _a2, int _a3)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, &_a3, 4u, 0));
	}

	internal unsafe static void WPP_SF_q(ulong Logger, ushort id, _GUID* TraceGuid, void* _a1)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, 0));
	}

	internal unsafe static void WPP_SF_qSd(ulong Logger, ushort id, _GUID* TraceGuid, void* _a1, ushort* _a2, int _a3)
	{
		//IL_0040->IL0040: Incompatible stack types: I vs Ref
		uint num2;
		if (_a2 != null)
		{
			uint num;
			if (*_a2 == 0)
			{
				num = 14u;
			}
			else
			{
				ushort* ptr = _a2;
				do
				{
					ptr++;
				}
				while (Unsafe.ReadUnaligned<short>(ptr) != 0);
				num = (uint)(((nint)((byte*)ptr - (nuint)_a2) >> 1) * 2 + 2);
			}
			num2 = num;
		}
		else
		{
			num2 = 10u;
		}
		ushort* ptr2 = (ushort*)((_a2 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040) : Unsafe.AsPointer(ref *_a2 == 0 ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY06_0024_0024CBG, _003F>(ref _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)_a2));
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, ptr2, num2, &_a3, 4u, 0));
	}

	internal unsafe static void WPP_SF_qSll(ulong Logger, ushort id, _GUID* TraceGuid, void* _a1, ushort* _a2, int _a3, int _a4)
	{
		//IL_0040->IL0040: Incompatible stack types: I vs Ref
		uint num2;
		if (_a2 != null)
		{
			uint num;
			if (*_a2 == 0)
			{
				num = 14u;
			}
			else
			{
				ushort* ptr = _a2;
				do
				{
					ptr++;
				}
				while (Unsafe.ReadUnaligned<short>(ptr) != 0);
				num = (uint)(((nint)((byte*)ptr - (nuint)_a2) >> 1) * 2 + 2);
			}
			num2 = num;
		}
		else
		{
			num2 = 10u;
		}
		ushort* ptr2 = (ushort*)((_a2 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040) : Unsafe.AsPointer(ref *_a2 == 0 ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY06_0024_0024CBG, _003F>(ref _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)_a2));
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, ptr2, num2, &_a3, 4u, &_a4, 4u, 0));
	}

	internal unsafe static void WPP_SF_qd(ulong Logger, ushort id, _GUID* TraceGuid, void* _a1, int _a2)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, 0));
	}

	internal unsafe static void WPP_SF_qddSd(ulong Logger, ushort id, _GUID* TraceGuid, void* _a1, int _a2, int _a3, ushort* _a4, int _a5)
	{
		//IL_0040->IL0040: Incompatible stack types: I vs Ref
		uint num2;
		if (_a4 != null)
		{
			uint num;
			if (*_a4 == 0)
			{
				num = 14u;
			}
			else
			{
				ushort* ptr = _a4;
				do
				{
					ptr++;
				}
				while (Unsafe.ReadUnaligned<short>(ptr) != 0);
				num = (uint)(((nint)((byte*)ptr - (nuint)_a4) >> 1) * 2 + 2);
			}
			num2 = num;
		}
		else
		{
			num2 = 10u;
		}
		ushort* ptr2 = (ushort*)((_a4 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040) : Unsafe.AsPointer(ref *_a4 == 0 ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY06_0024_0024CBG, _003F>(ref _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)_a4));
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, &_a3, 4u, ptr2, num2, &_a5, 4u, 0));
	}

	internal unsafe static void WPP_SF_qddqd(ulong Logger, ushort id, _GUID* TraceGuid, void* _a1, int _a2, int _a3, void* _a4, int _a5)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, &_a3, 4u, &_a4, 4u, &_a5, 4u, 0));
	}

	internal unsafe static void WPP_SF_ql(ulong Logger, ushort id, _GUID* TraceGuid, void* _a1, int _a2)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, 0));
	}

	internal unsafe static CheckForUpdatesCallback* MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002E_007Bctor_007D(CheckForUpdatesCallback* P_0, FirmwareUpdater owner, DeferredInvokeHandler handler, [MarshalAs(UnmanagedType.U1)] bool requiresSyncBeforeUpdate)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_00406B_0040);
		CheckForUpdatesCallback* ptr = (CheckForUpdatesCallback*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr);
		try
		{
			CheckForUpdatesCallback* ptr2 = (CheckForUpdatesCallback*)((byte*)P_0 + 12);
			gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E*)ptr2);
			try
			{
				((int*)P_0)[1] = 0;
				gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_003D((gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E*)ptr2, owner);
				gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr, handler);
				((sbyte*)P_0)[16] = (requiresSyncBeforeUpdate ? ((sbyte)1) : ((sbyte)0));
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void* MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002E__vecDelDtor(CheckForUpdatesCallback* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			CheckForUpdatesCallback* ptr = (CheckForUpdatesCallback*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 20u, *(int*)ptr, (delegate*<void*, void>)(delegate*<CheckForUpdatesCallback*, void>)(&MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static void MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002E_007Bdtor_007D(CheckForUpdatesCallback* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CheckForUpdatesCallback_0040MicrosoftZuneLibrary_0040_00406B_0040);
		CheckForUpdatesCallback* ptr2;
		try
		{
			CheckForUpdatesCallback* ptr;
			try
			{
				ptr = (CheckForUpdatesCallback*)((byte*)P_0 + 12);
				gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_003D((gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E*)ptr, null);
				ptr2 = (CheckForUpdatesCallback*)((byte*)P_0 + 8);
				gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr2, null);
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
			gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E*)ptr);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr2);
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002EOnBegin(CheckForUpdatesCallback* P_0, IFirmwareUpdateCallbackData* pCallbackData)
	{
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002EOnProgress(CheckForUpdatesCallback* P_0, IFirmwareUpdateCallbackData* pCallbackData)
	{
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002EOnComplete(CheckForUpdatesCallback* P_0, IUnknown* pCallbackData, IFirmwareUpdateErrorInfo* pErrorInfo, ECompletionAction eCompletionAction)
	{
		if (pErrorInfo == null)
		{
			_ZuneShipAssert(1001u, 476u);
			return -2147467261;
		}
		int num = 0;
		int num2 = 0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pErrorInfo + 12)))((nint)pErrorInfo, &num2);
		FirmwareUpdater firmwareUpdater = gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_002EP_0024AAVFirmwareUpdater_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E*)((byte*)P_0 + 12));
		UpdatePackageCollection updatePackageCollection = null;
		if (pCallbackData != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateCollection_003E cComPtrNtv_003CIFirmwareUpdateCollection_003E);
			*(int*)(&cComPtrNtv_003CIFirmwareUpdateCollection_003E) = 0;
			try
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pCallbackData)))((nint)pCallbackData, (_GUID*)Unsafe.AsPointer(ref _GUID_1052ac05_e104_4a08_a83c_89b2e3df1743), (void**)(&cComPtrNtv_003CIFirmwareUpdateCollection_003E));
				if (num >= 0)
				{
					updatePackageCollection = new UpdatePackageCollection((IFirmwareUpdateCollection*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateCollection_003E)));
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateCollection_003E*, void>)(&CComPtrNtv_003CIFirmwareUpdateCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateCollection_003E);
				throw;
			}
			CComPtrNtv_003CIFirmwareUpdateCollection_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIFirmwareUpdateCollection_003E);
		}
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 6u)
		{
			WPP_SF_qddqd(_a4: (updatePackageCollection == null) ? null : updatePackageCollection.NativeCollectionPtr, Logger: ((ulong*)WPP_GLOBAL_Control)[6], id: 14, TraceGuid: (_GUID*)Unsafe.AsPointer(ref _003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), _a1: pCallbackData, _a2: num2, _a3: (int)eCompletionAction, _a5: num);
		}
		if (num2 < 0 || updatePackageCollection == null || updatePackageCollection.Count == 0)
		{
			firmwareUpdater.Reset(deviceRebooting: false);
		}
		gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_002EP_0024AAVDeferredInvokeHandler_0040Iris_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 8)).Invoke((object)new CheckForUpdatesArgs(new FirmwareUpdateErrorInfo(pErrorInfo), updatePackageCollection, ((bool*)P_0)[16]));
		return num;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002EQueryInterface(CheckForUpdatesCallback* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_f882fe79_4b03_48f3_b52b_67dc5b2dfb3b)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002EAddRef(CheckForUpdatesCallback* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002ERelease(CheckForUpdatesCallback* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(*(int*)P_0 + 24)))((nint)P_0, 1u);
		}
		return (uint)num;
	}

	internal unsafe static FirmwareUpdateMediator* MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002E_007Bctor_007D(FirmwareUpdateMediator* P_0, DeferredInvokeHandler handlerStepBegin, DeferredInvokeHandler handlerStepProgress, DeferredInvokeHandler handlerComplete)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7FirmwareUpdateMediator_0040MicrosoftZuneLibrary_0040_00406B_0040);
		FirmwareUpdateMediator* ptr = (FirmwareUpdateMediator*)((byte*)P_0 + 8);
		gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr);
		try
		{
			FirmwareUpdateMediator* ptr2 = (FirmwareUpdateMediator*)((byte*)P_0 + 12);
			gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*)ptr2);
			try
			{
				FirmwareUpdateMediator* ptr3 = (FirmwareUpdateMediator*)((byte*)P_0 + 20);
				gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr3);
				try
				{
					FirmwareUpdateMediator* ptr4 = (FirmwareUpdateMediator*)((byte*)P_0 + 24);
					gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr4);
					try
					{
						FirmwareUpdateMediator* ptr5 = (FirmwareUpdateMediator*)((byte*)P_0 + 28);
						gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr5);
						try
						{
							((int*)P_0)[4] = 0;
							((sbyte*)P_0)[4] = 0;
							gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*)ptr2, null);
							gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr3, handlerStepBegin);
							gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr4, handlerStepProgress);
							gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)ptr5, handlerComplete);
							gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr, new object());
							return P_0;
						}
						catch
						{
							//try-fault
							___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 28);
							throw;
						}
					}
					catch
					{
						//try-fault
						___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 24);
						throw;
					}
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AObject_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static int MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EOnBegin(FirmwareUpdateMediator* P_0, IFirmwareUpdateCallbackData* pCallbackData)
	{
		FirmwareUpdateMediator* ptr = (FirmwareUpdateMediator*)((byte*)P_0 + 8);
		Monitor.Enter(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr));
		if (((byte*)P_0)[4] == 0)
		{
			DeferredInvokeHandler val = gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_002EP_0024AAVDeferredInvokeHandler_0040Iris_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 20));
			FirmwareUpdateBeginArgs firmwareUpdateBeginArgs = new FirmwareUpdateBeginArgs(new UpdateStep(pCallbackData));
			fixed (ushort* a = &Unsafe.As<char, ushort>(ref PtrToStringChars(firmwareUpdateBeginArgs.Step.Name)))
			{
				try
				{
					if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 6u)
					{
						WPP_SF_qSll(((ulong*)WPP_GLOBAL_Control)[6], 15, (_GUID*)Unsafe.AsPointer(ref _003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), pCallbackData, a, firmwareUpdateBeginArgs.Step.Cancelable ? 1 : 0, firmwareUpdateBeginArgs.Step.HasProgress ? 1 : 0);
					}
					val.Invoke((object)firmwareUpdateBeginArgs);
				}
				catch
				{
					//try-fault
					a = null;
					throw;
				}
			}
		}
		Monitor.Exit(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr));
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EOnProgress(FirmwareUpdateMediator* P_0, IFirmwareUpdateCallbackData* pCallbackData)
	{
		Exception ex = null;
		int result = 0;
		Monitor.Enter(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 8)));
		if (((byte*)P_0)[4] == 0)
		{
			try
			{
				DeferredInvokeHandler val = gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_002EP_0024AAVDeferredInvokeHandler_0040Iris_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 24));
				FirmwareUpdateProgressArgs firmwareUpdateProgressArgs = new FirmwareUpdateProgressArgs(new UpdateStep(pCallbackData));
				fixed (ushort* a = &Unsafe.As<char, ushort>(ref PtrToStringChars(firmwareUpdateProgressArgs.Step.Name)))
				{
					try
					{
						if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 6u)
						{
							WPP_SF_qSd(((ulong*)WPP_GLOBAL_Control)[6], 16, (_GUID*)Unsafe.AsPointer(ref _003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), pCallbackData, a, firmwareUpdateProgressArgs.Step.Progress);
							int num = 1;
						}
						else
						{
							int num = 0;
						}
						val.Invoke((object)firmwareUpdateProgressArgs);
					}
					catch
					{
						//try-fault
						a = null;
						throw;
					}
				}
			}
			catch (Exception ex2)
			{
				_ZuneShipAssert(30009u, (uint)ex2.GetType().MetadataToken);
				result = -2147418113;
			}
		}
		Monitor.Exit(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 8)));
		return result;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EOnComplete(FirmwareUpdateMediator* P_0, IUnknown* pCallbackData, IFirmwareUpdateErrorInfo* pErrorInfo, ECompletionAction eCompletionAction)
	{
		if (pErrorInfo == null)
		{
			_ZuneShipAssert(1001u, 681u);
			return -2147467261;
		}
		int num = 0;
		int a = 0;
		Monitor.Enter(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 8)));
		if (((byte*)P_0)[4] == 0)
		{
			if (eCompletionAction == (ECompletionAction)4)
			{
				((sbyte*)P_0)[4] = 1;
			}
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pErrorInfo + 12)))((nint)pErrorInfo, &a);
			UpdateStep updateStep = null;
			Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateCallbackData_003E cComPtrNtv_003CIFirmwareUpdateCallbackData_003E);
			*(int*)(&cComPtrNtv_003CIFirmwareUpdateCallbackData_003E) = 0;
			try
			{
				if (pCallbackData != null)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pCallbackData)))((nint)pCallbackData, (_GUID*)Unsafe.AsPointer(ref _GUID_41ccb4e2_52e7_436e_b9ad_8d9962e0cd11), (void**)(&cComPtrNtv_003CIFirmwareUpdateCallbackData_003E));
					if (num >= 0)
					{
						updateStep = new UpdateStep((IFirmwareUpdateCallbackData*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateCallbackData_003E)));
					}
				}
				object s = "<null update step>";
				if (updateStep != null)
				{
					s = updateStep.Name;
				}
				fixed (ushort* a2 = &Unsafe.As<char, ushort>(ref PtrToStringChars((string)s)))
				{
					try
					{
						if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[57] >= 6u)
						{
							WPP_SF_qddSd(((ulong*)WPP_GLOBAL_Control)[6], 17, (_GUID*)Unsafe.AsPointer(ref _003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), pCallbackData, a, (int)eCompletionAction, a2, num);
						}
						DeferredInvokeHandler obj = gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_002EP_0024AAVDeferredInvokeHandler_0040Iris_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 28));
						FirmwareProcessCompleteArgs firmwareProcessCompleteArgs = new FirmwareProcessCompleteArgs(new FirmwareUpdateErrorInfo(pErrorInfo), updateStep, (CompletionAction)eCompletionAction, DisconnectDeviceOnComplete: false);
						obj.Invoke((object)firmwareProcessCompleteArgs);
					}
					catch
					{
						//try-fault
						a2 = null;
						throw;
					}
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateCallbackData_003E*, void>)(&CComPtrNtv_003CIFirmwareUpdateCallbackData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateCallbackData_003E);
				throw;
			}
			CComPtrNtv_003CIFirmwareUpdateCallbackData_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateCallbackData_003E);
		}
		Monitor.Exit(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 8)));
		return num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EFirmwareProcessCanceled(FirmwareUpdateMediator* P_0)
	{
		FirmwareUpdateMediator* ptr = (FirmwareUpdateMediator*)((byte*)P_0 + 8);
		Monitor.Enter(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr));
		if (((byte*)P_0)[4] == 0)
		{
			FirmwareUpdateMediator* ptr2 = (FirmwareUpdateMediator*)((byte*)P_0 + 12);
			if (gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_002EP_0024AAVTimer_0040Threading_0040System_0040_0040((gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*)ptr2) == null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
				TimerCallback callback = new MediatorCancelTimerCallback(P_0).TimerHit;
				gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*)ptr2, new Timer(callback, null, 60000, -1));
			}
		}
		Monitor.Exit(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr));
	}

	internal unsafe static void MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EForceCancel(FirmwareUpdateMediator* P_0)
	{
		FirmwareUpdateMediator* ptr = (FirmwareUpdateMediator*)((byte*)P_0 + 8);
		Monitor.Enter(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr));
		if (((byte*)P_0)[4] == 0)
		{
			((sbyte*)P_0)[4] = 1;
			Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
			*(int*)(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E) = 0;
			try
			{
				InternalErrorInfo* ptr2 = (InternalErrorInfo*)@new(40u);
				InternalErrorInfo* lp;
				try
				{
					lp = ((ptr2 == null) ? null : MicrosoftZuneLibrary_002EInternalErrorInfo_002E_007Bctor_007D(ptr2, -2147467260));
				}
				catch
				{
					//try-fault
					delete(ptr2);
					throw;
				}
				CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_003D(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E, (IFirmwareUpdateErrorInfo*)lp);
				DeferredInvokeHandler obj = gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_002EP_0024AAVDeferredInvokeHandler_0040Iris_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 28));
				FirmwareProcessCompleteArgs firmwareProcessCompleteArgs = new FirmwareProcessCompleteArgs(new FirmwareUpdateErrorInfo((IFirmwareUpdateErrorInfo*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E))), null, CompletionAction.Complete, DisconnectDeviceOnComplete: false);
				obj.Invoke((object)firmwareProcessCompleteArgs);
				FirmwareUpdateMediator* ptr3 = (FirmwareUpdateMediator*)((byte*)P_0 + 12);
				((IDisposable)gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*)ptr3)).Dispose();
				gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*)ptr3, null);
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E*, void>)(&CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
				throw;
			}
			CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
		}
		Monitor.Exit(gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr));
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 8)))((nint)P_0);
	}

	internal unsafe static int MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EQueryInterface(FirmwareUpdateMediator* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_f882fe79_4b03_48f3_b52b_67dc5b2dfb3b)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EAddRef(FirmwareUpdateMediator* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 4);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002ERelease(FirmwareUpdateMediator* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 4);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002E_007Bdtor_007D(FirmwareUpdateMediator* P_0)
	{
		try
		{
			try
			{
				try
				{
					try
					{
						gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 28));
					}
					catch
					{
						//try-fault
						___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 24);
						throw;
					}
					gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 24));
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
					throw;
				}
				gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E*)((byte*)P_0 + 20));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
			gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AObject_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static InternalErrorInfo* MicrosoftZuneLibrary_002EInternalErrorInfo_002E_007Bctor_007D(InternalErrorInfo* P_0, int hrStatus)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7InternalErrorInfo_0040MicrosoftZuneLibrary_0040_00406B_0040);
		WBSTRString_002E_007Bctor_007D((WBSTRString*)((byte*)P_0 + 16));
		try
		{
			WBSTRString_002E_007Bctor_007D((WBSTRString*)((byte*)P_0 + 28));
			try
			{
				((int*)P_0)[2] = hrStatus;
				((int*)P_0)[1] = 0;
				((sbyte*)P_0)[12] = 0;
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&WBSTRString_002E_007Bdtor_007D), (byte*)P_0 + 28);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&WBSTRString_002E_007Bdtor_007D), (byte*)P_0 + 16);
			throw;
		}
	}

	internal unsafe static int MicrosoftZuneLibrary_002EInternalErrorInfo_002EGetErrorCode(InternalErrorInfo* P_0, int* phrErrorCode)
	{
		if (phrErrorCode == null)
		{
			_ZuneShipAssert(1001u, 880u);
			return -2147467261;
		}
		*phrErrorCode = ((int*)P_0)[2];
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EInternalErrorInfo_002EGetErrorDescription(InternalErrorInfo* P_0, ushort** pbstrErrorDescription)
	{
		if (pbstrErrorDescription == null)
		{
			_ZuneShipAssert(1001u, 891u);
			return -2147467261;
		}
		*(int*)pbstrErrorDescription = 0;
		int num = MicrosoftZuneLibrary_002EInternalErrorInfo_002EEnsureDescriptionAndUrl(P_0);
		if (num >= 0)
		{
			num = WString_002EAllocateBSTR((WString*)((byte*)P_0 + 16), pbstrErrorDescription);
		}
		return num;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EInternalErrorInfo_002EGetErrorUrl(InternalErrorInfo* P_0, ushort** pbstrErrorUrl)
	{
		if (pbstrErrorUrl == null)
		{
			_ZuneShipAssert(1001u, 910u);
			return -2147467261;
		}
		*(int*)pbstrErrorUrl = 0;
		if (MicrosoftZuneLibrary_002EInternalErrorInfo_002EEnsureDescriptionAndUrl(P_0) >= 0)
		{
			WString_002EAllocateBSTR((WString*)((byte*)P_0 + 28), pbstrErrorUrl);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EInternalErrorInfo_002EEnsureDescriptionAndUrl(InternalErrorInfo* P_0)
	{
		int num = 0;
		int num2;
		if (((byte*)P_0)[12] == 0)
		{
			num2 = ZuneLibraryExports_002EGetMappedErrorDescriptionAndUrl(((int*)P_0)[2], eErrorCondition.eEC_None, &num, (ushort**)((byte*)P_0 + 16), (ushort**)((byte*)P_0 + 28));
			int num3 = ((num2 >= 0) ? 1 : 0);
			((sbyte*)P_0)[12] = (sbyte)num3;
		}
		else
		{
			num2 = 1;
		}
		return num2;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EInternalErrorInfo_002EQueryInterface(InternalErrorInfo* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_c3b03bab_79ec_4e44_a0ec_2e0ff3b747de)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002EInternalErrorInfo_002EAddRef(InternalErrorInfo* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002EInternalErrorInfo_002ERelease(InternalErrorInfo* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002EInternalErrorInfo_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002EInternalErrorInfo_002E_007Bdtor_007D(InternalErrorInfo* P_0)
	{
		try
		{
			WBSTRString* ptr = (WBSTRString*)((byte*)P_0 + 28);
			WString_002E_007Bdtor_007D((WString*)ptr);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&WBSTRString_002E_007Bdtor_007D), (byte*)P_0 + 16);
			throw;
		}
		WBSTRString* ptr2 = (WBSTRString*)((byte*)P_0 + 16);
		WString_002E_007Bdtor_007D((WString*)ptr2);
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E* P_0, DeferredInvokeHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static DeferredInvokeHandler gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E_002E_002EP_0024AAVDeferredInvokeHandler_0040Iris_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AIris_003A_003ADeferredInvokeHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (DeferredInvokeHandler)((GCHandle)intPtr).Target;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_003D(gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E* P_0, FirmwareUpdater t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static FirmwareUpdater gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E_002E_002EP_0024AAVFirmwareUpdater_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdater_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (FirmwareUpdater)((GCHandle)intPtr).Target;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AObject_0020_005E_003E* gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bctor_007D(gcroot_003CSystem_003A_003AObject_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CSystem_003A_003AObject_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AObject_0020_005E_003E* gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_003D(gcroot_003CSystem_003A_003AObject_0020_005E_003E* P_0, object t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static object gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040(gcroot_003CSystem_003A_003AObject_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return ((GCHandle)intPtr).Target;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E* gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_007Bctor_007D(gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E* gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_003D(gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E* P_0, Timer t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static Timer gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_002EP_0024AAVTimer_0040Threading_0040System_0040_0040(gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (Timer)((GCHandle)intPtr).Target;
	}

	internal unsafe static Timer gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E_002E_002D_003E(gcroot_003CSystem_003A_003AThreading_003A_003ATimer_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (Timer)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareMetadata_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareMetadata_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareMetadata_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateCollection_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareUpdateCollection_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareUpdateCollection_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateCallbackData_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareUpdateCallbackData_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareUpdateCallbackData_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002ERelease(P_0);
	}

	internal unsafe static IFirmwareUpdateErrorInfo* CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_003D(CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E* P_0, IFirmwareUpdateErrorInfo* lp)
	{
		CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002ERelease(P_0);
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return (IFirmwareUpdateErrorInfo*)(int)(*(uint*)P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateNotification_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareUpdateNotification_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareUpdateNotification_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIEndpointManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CIEndpointManager_003E* P_0)
	{
		CComPtrNtv_003CIEndpointManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdater2_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareUpdater2_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareUpdater2_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateCallback_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareUpdateCallback_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareUpdateCallback_003E_002ERelease(P_0);
	}

	internal unsafe static IFirmwareUpdateCallback* CComPtrNtv_003CIFirmwareUpdateCallback_003E_002E_003D(CComPtrNtv_003CIFirmwareUpdateCallback_003E* P_0, IFirmwareUpdateCallback* lp)
	{
		CComPtrNtv_003CIFirmwareUpdateCallback_003E_002ERelease(P_0);
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return (IFirmwareUpdateCallback*)(int)(*(uint*)P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdater_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareUpdater_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareUpdater_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareRestorePoint_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareRestorePoint_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareRestorePoint_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareRestorer_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareRestorer_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareRestorer_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareRestorePointCollection_003E_002E_007Bdtor_007D(CComPtrNtv_003CIFirmwareRestorePointCollection_003E* P_0)
	{
		CComPtrNtv_003CIFirmwareRestorePointCollection_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareMetadata_003E_002ERelease(CComPtrNtv_003CIFirmwareMetadata_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareMetadata* ptr = (IFirmwareMetadata*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateCollection_003E_002ERelease(CComPtrNtv_003CIFirmwareUpdateCollection_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareUpdateCollection* ptr = (IFirmwareUpdateCollection*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateCallbackData_003E_002ERelease(CComPtrNtv_003CIFirmwareUpdateCallbackData_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareUpdateCallbackData* ptr = (IFirmwareUpdateCallbackData*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002ERelease(CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareUpdateErrorInfo* ptr = (IFirmwareUpdateErrorInfo*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateNotification_003E_002ERelease(CComPtrNtv_003CIFirmwareUpdateNotification_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareUpdateNotification* ptr = (IFirmwareUpdateNotification*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIEndpointManager_003E_002ERelease(CComPtrNtv_003CIEndpointManager_003E* P_0)
	{
		int num = *(int*)P_0;
		IEndpointManager* ptr = (IEndpointManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdater2_003E_002ERelease(CComPtrNtv_003CIFirmwareUpdater2_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareUpdater2* ptr = (IFirmwareUpdater2*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdateCallback_003E_002ERelease(CComPtrNtv_003CIFirmwareUpdateCallback_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareUpdateCallback* ptr = (IFirmwareUpdateCallback*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareUpdater_003E_002ERelease(CComPtrNtv_003CIFirmwareUpdater_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareUpdater* ptr = (IFirmwareUpdater*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareRestorePoint_003E_002ERelease(CComPtrNtv_003CIFirmwareRestorePoint_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareRestorePoint* ptr = (IFirmwareRestorePoint*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareRestorer_003E_002ERelease(CComPtrNtv_003CIFirmwareRestorer_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareRestorer* ptr = (IFirmwareRestorer*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIFirmwareRestorePointCollection_003E_002ERelease(CComPtrNtv_003CIFirmwareRestorePointCollection_003E* P_0)
	{
		int num = *(int*)P_0;
		IFirmwareRestorePointCollection* ptr = (IFirmwareRestorePointCollection*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static int IUnknown_002EQueryInterface_003Cstruct_0020IFirmwareRestorer_003E(IUnknown* P_0, IFirmwareRestorer** pp)
	{
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)P_0)))((nint)P_0, (_GUID*)Unsafe.AsPointer(ref _GUID_6e2f6626_94f9_4e71_8736_f2e4c5a00a3b), (void**)pp);
	}

	internal unsafe static int GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020EEndpointClass_003E(IEndpointHost* pC, EEndpointHostProperty propId, EEndpointClass* pT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 244u);
			return -2147467261;
		}
		if (pT == null)
		{
			_ZuneShipAssert(1001u, 245u);
			return -2147467261;
		}
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pC + 68)))((nint)pC, propId, &num);
		if (num2 >= 0)
		{
			*pT = (EEndpointClass)num;
		}
		return num2;
	}

	internal unsafe static int GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IFirmwareUpdater_003E(IEndpointHost* pC, EEndpointHostProperty propId, IFirmwareUpdater** ppT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 193u);
			return -2147467261;
		}
		if (ppT == null)
		{
			_ZuneShipAssert(1001u, 194u);
			return -2147467261;
		}
		IUnknown* ptr = null;
		*(int*)ppT = 0;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, IUnknown**, int>)(int)(*(uint*)(*(int*)pC + 52)))((nint)pC, propId, &ptr);
		if (num >= 0 && ptr != null)
		{
			num = SafeQueryInterface_003Cstruct_0020IFirmwareUpdater_003E(ptr, ppT);
		}
		SafeRelease_003Cstruct_0020IUnknown_003E(&ptr);
		return num;
	}

	internal unsafe static int SafeQueryInterface_003Cstruct_0020IFirmwareUpdater_003E(IUnknown* pUnk, IFirmwareUpdater** ppT)
	{
		if (ppT != null)
		{
			*(int*)ppT = 0;
			if (pUnk != null)
			{
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pUnk)))((nint)pUnk, (_GUID*)Unsafe.AsPointer(ref _GUID_7d0dee42_94b2_49a4_99fa_bae184d835d2), (void**)ppT);
			}
			return -2147467261;
		}
		return -2147467261;
	}

	internal unsafe static void DBPropertyRequestStruct_002E_007Bdtor_007D(DBPropertyRequestStruct* P_0)
	{
		CComPropVariant_002EClear((CComPropVariant*)((byte*)P_0 + 8));
	}

	internal unsafe static DBPropertyRequestStruct* DBPropertyRequestStruct_002E_007Bctor_007D(DBPropertyRequestStruct* P_0, uint dwAtomRequested)
	{
		*(uint*)P_0 = dwAtomRequested;
		((int*)P_0)[1] = 0;
		// IL initblk instruction
		Unsafe.InitBlock((byte*)P_0 + 8, 0, 16);
		return P_0;
	}

	internal unsafe static GasGaugeMediator* GasGaugeMediator_002E_007Bctor_007D(GasGaugeMediator* P_0, GasGauge gasGauge, IGasGauge* pGasGauge)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GasGaugeMediator_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)((byte*)P_0 + 8), gasGauge);
		try
		{
			GasGaugeMediator* ptr = (GasGaugeMediator*)((byte*)P_0 + 12);
			*(int*)ptr = 0;
			if (pGasGauge != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pGasGauge + 4)))((nint)pGasGauge);
				*(int*)ptr = (int)pGasGauge;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IGasGaugeCallback*, int>)(int)(*(uint*)(*(int*)pGasGauge + 48)))((nint)pGasGauge, (IGasGaugeCallback*)P_0);
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		return P_0;
	}

	internal unsafe static void GasGaugeMediator_002E_007Bdtor_007D(GasGaugeMediator* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GasGaugeMediator_0040_00406B_0040);
		try
		{
			GasGaugeMediator_002EShutdown(P_0);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static void GasGaugeMediator_002EShutdown(GasGaugeMediator* P_0)
	{
		uint num = ((uint*)P_0)[3];
		if (num != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IGasGaugeCallback*, int>)(int)(*(uint*)(*(int*)(int)num + 52)))((IntPtr)(int)num, (IGasGaugeCallback*)P_0);
			num = ((uint*)P_0)[3];
			if (0 != num)
			{
				uint num2 = num;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num2 + 8)))((IntPtr)(int)num2);
				((int*)P_0)[3] = 0;
			}
		}
	}

	internal unsafe static void GasGaugeMediator_002ECategorySpaceUsedUpdated(GasGaugeMediator* P_0, int iSyncCategory, long llNewSchemaSpace, long llNewFreeSpace)
	{
		GasGaugeMediator* ptr = (GasGaugeMediator*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002EP_0024AAVGasGauge_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)ptr).CategorySpaceUsedUpdated((ESyncCategory)iSyncCategory, llNewSchemaSpace, llNewFreeSpace);
		}
	}

	internal unsafe static void GasGaugeMediator_002EReservedSpaceUpdated(GasGaugeMediator* P_0, long llNewReservedSpace, long llNewFreeSpace)
	{
		GasGaugeMediator* ptr = (GasGaugeMediator*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002EP_0024AAVGasGauge_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)ptr).ReservedSpaceUpdated(llNewReservedSpace, llNewFreeSpace);
		}
	}

	internal unsafe static void GasGaugeMediator_002EDeviceOverflow(GasGaugeMediator* P_0)
	{
		GasGaugeMediator* ptr = (GasGaugeMediator*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002EP_0024AAVGasGauge_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E*)ptr).DeviceOverflow();
		}
	}

	internal unsafe static int GasGaugeMediator_002EQueryInterface(GasGaugeMediator* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_7a68ebac_a036_41dc_90e4_8b39c5be83a3)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint GasGaugeMediator_002EAddRef(GasGaugeMediator* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint GasGaugeMediator_002ERelease(GasGaugeMediator* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			GasGaugeMediator_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E* P_0, GasGauge t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static GasGauge gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002EP_0024AAVGasGauge_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (GasGauge)((GCHandle)intPtr).Target;
	}

	internal unsafe static GasGauge gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003AGasGauge_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (GasGauge)((GCHandle)intPtr).Target;
	}

	internal unsafe static void Microsoft_002EZune_002EService_002E_003FA0xf8df3735_002ESetHeader(IHttpWebRequest* pRequest, string header)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(header)))
		{
			int num = *(int*)pRequest + 40;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)pRequest, ptr);
		}
	}

	internal unsafe static CWebRequestCallbackWrapper* Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002E_007Bctor_007D(CWebRequestCallbackWrapper* P_0, Uri requestUri, IHttpWebRequest* pRequest, IStream* pStream, AsyncRequestComplete requestComplete, object stateInfo)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		CWebRequestCallbackWrapper* ptr = (CWebRequestCallbackWrapper*)((byte*)P_0 + 16);
		gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AUri_0020_005E_003E*)ptr);
		try
		{
			CWebRequestCallbackWrapper* ptr2 = (CWebRequestCallbackWrapper*)((byte*)P_0 + 20);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E*)ptr2);
			try
			{
				CWebRequestCallbackWrapper* ptr3 = (CWebRequestCallbackWrapper*)((byte*)P_0 + 24);
				gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr3);
				try
				{
					((int*)P_0)[1] = 1;
					CWebRequestCallbackWrapper* ptr4 = (CWebRequestCallbackWrapper*)((byte*)P_0 + 12);
					*(int*)ptr4 = (int)pStream;
					CWebRequestCallbackWrapper* ptr5 = (CWebRequestCallbackWrapper*)((byte*)P_0 + 8);
					*(int*)ptr5 = (int)pRequest;
					gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AUri_0020_005E_003E*)ptr, requestUri);
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E*)ptr2, requestComplete);
					gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr3, stateInfo);
					uint num = *(uint*)ptr4;
					if (num != 0)
					{
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 4)))((IntPtr)(int)num);
					}
					uint num2 = *(uint*)ptr5;
					if (num2 != 0)
					{
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num2 + 4)))((IntPtr)(int)num2);
					}
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AObject_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 24);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AUri_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
			throw;
		}
		return P_0;
	}

	internal unsafe static void Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002E_007Bdtor_007D(CWebRequestCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CWebRequestCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			try
			{
				try
				{
					uint num = ((uint*)P_0)[3];
					if (num != 0)
					{
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
					}
					uint num2 = ((uint*)P_0)[2];
					if (num2 != 0)
					{
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num2 + 8)))((IntPtr)(int)num2);
					}
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AObject_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 24);
					throw;
				}
				gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 24));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
				throw;
			}
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E*)((byte*)P_0 + 20));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AUri_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
			throw;
		}
		gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AUri_0020_005E_003E*)((byte*)P_0 + 16));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002EQueryInterface(CWebRequestCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_a3138a7c_be4e_4aa1_999f_f2fdd4b3f428)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002EAddRef(CWebRequestCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002ERelease(CWebRequestCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002ERedirected(CWebRequestCallbackWrapper* P_0, IHttpWebResponse* P_1, ushort* P_2)
	{
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002EHeadersAvailable(CWebRequestCallbackWrapper* P_0, IHttpWebResponse* P_1)
	{
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002EResponseProgress(CWebRequestCallbackWrapper* P_0, IHttpWebResponse* P_1, ulong P_2, ulong P_3)
	{
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002ECWebRequestCallbackWrapper_002EResponseCompleted(CWebRequestCallbackWrapper* P_0, IHttpWebResponse* pResponse, int __unnamed001)
	{
		HttpStatusCode statusCode = (HttpStatusCode)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pResponse + 12)))((nint)pResponse);
		Uri requestUri = gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_002EP_0024AAVUri_0040System_0040_0040((gcroot_003CSystem_003A_003AUri_0020_005E_003E*)((byte*)P_0 + 16));
		AsyncRequestComplete asyncRequestComplete = gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_002EP_0024AAVAsyncRequestComplete_0040Service_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E*)((byte*)P_0 + 20));
		object stateInfo = gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_002EP_0024AAVObject_0040System_0040_0040((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 24));
		Microsoft.Zune.Service.HttpWebResponse response = new Microsoft.Zune.Service.HttpWebResponse(requestUri, statusCode, pResponse, (IStream*)(int)((uint*)P_0)[3]);
		asyncRequestComplete(response, stateInfo);
		Microsoft.Zune.Service.HttpWebRequest.OnAsyncRequestComplete((IHttpWebRequest*)(int)((uint*)P_0)[2]);
		int num = ((int*)P_0)[3];
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)num + 8)))((IntPtr)num);
		((int*)P_0)[3] = 0;
		int num2 = ((int*)P_0)[2];
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)num2 + 8)))((IntPtr)num2);
		((int*)P_0)[2] = 0;
		return 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AUri_0020_005E_003E* gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_007Bctor_007D(gcroot_003CSystem_003A_003AUri_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CSystem_003A_003AUri_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AUri_0020_005E_003E* gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_003D(gcroot_003CSystem_003A_003AUri_0020_005E_003E* P_0, Uri t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static Uri gcroot_003CSystem_003A_003AUri_0020_005E_003E_002E_002EP_0024AAVUri_0040System_0040_0040(gcroot_003CSystem_003A_003AUri_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (Uri)((GCHandle)intPtr).Target;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E* P_0, AsyncRequestComplete t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static AsyncRequestComplete gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E_002E_002EP_0024AAVAsyncRequestComplete_0040Service_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAsyncRequestComplete_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (AsyncRequestComplete)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIHttpWebRequest_003E_002E_007Bdtor_007D(CComPtrNtv_003CIHttpWebRequest_003E* P_0)
	{
		CComPtrNtv_003CIHttpWebRequest_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIHttpWebRequest_003E_002ERelease(CComPtrNtv_003CIHttpWebRequest_003E* P_0)
	{
		int num = *(int*)P_0;
		IHttpWebRequest* ptr = (IHttpWebRequest*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static uint SafeAddRef_003Cclass_0020NSSMediator_003E(NSSMediator* pUnk)
	{
		int result;
		if (pUnk != null)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pUnk + 4)))((nint)pUnk);
		}
		else
		{
			result = 0;
		}
		return (uint)result;
	}

	internal unsafe static NativeInteropNotifications* MicrosoftZuneLibrary_002ENativeInteropNotifications_002E_007Bctor_007D(NativeInteropNotifications* P_0, InteropNotifications interopNotify)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7NativeInteropNotifications_0040MicrosoftZuneLibrary_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E*)((byte*)P_0 + 8), interopNotify);
		return P_0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ENativeInteropNotifications_002EQueryInterface(NativeInteropNotifications* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_3fb2d757_8ddb_46a9_9dd2_3424e2903e46)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ENativeInteropNotifications_002EAddRef(NativeInteropNotifications* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ENativeInteropNotifications_002ERelease(NativeInteropNotifications* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002ENativeInteropNotifications_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeInteropNotifications_002E_007Bdtor_007D(NativeInteropNotifications* P_0)
	{
		gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeInteropNotifications_002EOnShowErrorDialog(NativeInteropNotifications* P_0, int hr, uint uiStringId)
	{
		NativeInteropNotifications* ptr = (NativeInteropNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E*)ptr).m_ShowErrorDialogHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E*)ptr).m_ShowErrorDialogHandler(hr, uiStringId);
		}
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E* P_0, InteropNotifications t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static InteropNotifications gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003AInteropNotifications_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (InteropNotifications)((GCHandle)intPtr).Target;
	}

	internal unsafe static void WPP_SF_DD(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1, uint _a2)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, 0));
	}

	internal unsafe static void WPP_SF_DDD(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1, uint _a2, uint _a3)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, &_a3, 4u, 0));
	}

	internal unsafe static void WPP_SF_DdS(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1, int _a2, ushort* _a3)
	{
		//IL_0040->IL0040: Incompatible stack types: I vs Ref
		uint num2;
		if (_a3 != null)
		{
			uint num;
			if (*_a3 == 0)
			{
				num = 14u;
			}
			else
			{
				ushort* ptr = _a3;
				do
				{
					ptr++;
				}
				while (Unsafe.ReadUnaligned<short>(ptr) != 0);
				num = (uint)(((nint)((byte*)ptr - (nuint)_a3) >> 1) * 2 + 2);
			}
			num2 = num;
		}
		else
		{
			num2 = 10u;
		}
		ushort* ptr2 = (ushort*)((_a3 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_19CIJIHAKK_0040_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024AA_0040) : Unsafe.AsPointer(ref *_a3 == 0 ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY06_0024_0024CBG, _003F>(ref _003F_003F_C_0040_1O_0040DPFEOIGE_0040_003F_0024AA_003F_0024DM_003F_0024AAN_003F_0024AAU_003F_0024AAL_003F_0024AAL_003F_0024AA_003F_0024DO_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)_a3));
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, ptr2, num2, 0));
	}

	internal unsafe static void WPP_SF_Dl(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1, int _a2)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, 0));
	}

	internal unsafe static void WPP_SF_Dll(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1, int _a2, int _a3)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, &_a3, 4u, 0));
	}

	internal unsafe static int CSchemaMap_002EGetIndex(ushort* wszName)
	{
		if (null == wszName)
		{
			return -1;
		}
		CSchemaMap._SCHEMAMAPENTRY* ptr = CSchemaMap_002EGetEntry(wszName);
		if (null == ptr)
		{
			return -1;
		}
		return (int)((nint)((ref *(_003F*)ptr) - (ref Unsafe.As<_0024ArrayType_0024_0024_0024BY0A_0040_0024_0024CBU_SCHEMAMAPENTRY_0040CSchemaMap_0040_0040, _003F>(ref _003Fs_rgSchemaMapEntry_0040CSchemaMap_0040_00400QBU_SCHEMAMAPENTRY_00401_0040B))) / (nint)24);
	}

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1);

	internal unsafe static void PERFTRACE_COLLECTIONEVENT(_COLLECTION_EVENT @event, ushort* pwszDetail)
	{
		if ((uint)Unsafe.As<EtwControlerState, byte>(ref Unsafe.AddByteOffset(ref g_EtwControlerState, 8)) <= 1u || (Unsafe.As<EtwControlerState, int>(ref Unsafe.AddByteOffset(ref g_EtwControlerState, 4)) & 0x10) == 0)
		{
			return;
		}
		Unsafe.SkipInit(out _PERFTRACE_COLLECTIONEVENT_RECORD pERFTRACE_COLLECTIONEVENT_RECORD);
		// IL initblk instruction
		Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref pERFTRACE_COLLECTIONEVENT_RECORD, 2), 0, 62);
		*(short*)(&pERFTRACE_COLLECTIONEVENT_RECORD) = 64;
		Unsafe.As<_PERFTRACE_COLLECTIONEVENT_RECORD, int>(ref Unsafe.AddByteOffset(ref pERFTRACE_COLLECTIONEVENT_RECORD, 44)) = 1179648;
		// IL cpblk instruction
		Unsafe.CopyBlock(ref Unsafe.AddByteOffset(ref pERFTRACE_COLLECTIONEVENT_RECORD, 24), ref _GUID_0e0fc989_9392_467b_92fc_80d4c1999a97, 16);
		Unsafe.As<_PERFTRACE_COLLECTIONEVENT_RECORD, byte>(ref Unsafe.AddByteOffset(ref pERFTRACE_COLLECTIONEVENT_RECORD, 4)) = (byte)@event;
		ushort* ptr = (ushort*)((pwszDetail == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040) : pwszDetail);
		Unsafe.As<_PERFTRACE_COLLECTIONEVENT_RECORD, long>(ref Unsafe.AddByteOffset(ref pERFTRACE_COLLECTIONEVENT_RECORD, 48)) = (nint)ptr;
		ushort* ptr2 = ptr;
		if (Unsafe.ReadUnaligned<short>(ptr) != 0)
		{
			do
			{
				ptr2++;
			}
			while (Unsafe.ReadUnaligned<short>(ptr2) != 0);
		}
		uint num = (uint)((nint)((byte*)ptr2 - (nuint)ptr) >> 1);
		Unsafe.As<_PERFTRACE_COLLECTIONEVENT_RECORD, uint>(ref Unsafe.AddByteOffset(ref pERFTRACE_COLLECTIONEVENT_RECORD, 56)) = num * 2 + 2;
		TraceEvent(Unsafe.As<EtwControlerState, ulong>(ref Unsafe.AddByteOffset(ref g_EtwControlerState, 16)), (_EVENT_TRACE_HEADER*)(&pERFTRACE_COLLECTIONEVENT_RECORD));
	}

	[DllImport("ZuneNativeLib", CharSet = CharSet.Unicode)]
	public static extern void SQMAddWrapper(string sqmDataId, int nData);

	internal unsafe static WMISGetAlbumForAlbumIdCallbackWrapper* MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002E_007Bctor_007D(WMISGetAlbumForAlbumIdCallbackWrapper* P_0, GetAlbumForAlbumIdCompleteHandler callbackHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7WMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_00406B_0040);
		WMISGetAlbumForAlbumIdCallbackWrapper* ptr = (WMISGetAlbumForAlbumIdCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[1] = 1;
			gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E*)ptr, callbackHandler);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002E_007Bdtor_007D(WMISGetAlbumForAlbumIdCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7WMISGetAlbumForAlbumIdCallbackWrapper_0040MicrosoftZuneLibrary_0040_00406B_0040);
		gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002EQueryInterface(WMISGetAlbumForAlbumIdCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_7f89b907_d770_41d1_9fd7_5fd3777649b9)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002EAddRef(WMISGetAlbumForAlbumIdCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002ERelease(WMISGetAlbumForAlbumIdCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002EOnComplete(WMISGetAlbumForAlbumIdCallbackWrapper* P_0, long WMISAlbumId, int Volume, int hr, IAlbumInfo* pAlbumInfo)
	{
		AlbumMetadata albumMetadata = null;
		if (pAlbumInfo != null)
		{
			albumMetadata = new AlbumMetadata(pAlbumInfo);
		}
		gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_002EP_0024AAVGetAlbumForAlbumIdCompleteHandler_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E*)((byte*)P_0 + 8))(WMISAlbumId, Volume, hr, albumMetadata);
		return 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E* P_0, GetAlbumForAlbumIdCompleteHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static GetAlbumForAlbumIdCompleteHandler gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E_002E_002EP_0024AAVGetAlbumForAlbumIdCompleteHandler_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003AGetAlbumForAlbumIdCompleteHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (GetAlbumForAlbumIdCompleteHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static CMBRBandwidthTestEventSink* MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002E_007Bctor_007D(CMBRBandwidthTestEventSink* P_0, BandwidthTestInterop managedBandwidthInterop, [MarshalAs(UnmanagedType.U1)] bool async)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_00406B_0040);
		gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E*)((byte*)P_0 + 4), managedBandwidthInterop);
		try
		{
			((int*)P_0)[2] = 0;
			((sbyte*)P_0)[12] = (async ? ((sbyte)1) : ((sbyte)0));
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 4);
			throw;
		}
	}

	internal unsafe static void MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002E_007Bdtor_007D(CMBRBandwidthTestEventSink* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CMBRBandwidthTestEventSink_0040MicrosoftZunePlayback_0040_00406B_0040);
		gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E*)((byte*)P_0 + 4));
	}

	internal unsafe static int MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002EQueryInterface(CMBRBandwidthTestEventSink* P_0, _GUID* riid, void** ppUnknown)
	{
		int result = 0;
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_e4f957fa_2742_4393_9518_c2705fb5c517)) == 0)
		{
			result = -2147467262;
		}
		else
		{
			*(int*)ppUnknown = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002EAddRef(CMBRBandwidthTestEventSink* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 2);
	}

	internal unsafe static uint MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002ERelease(CMBRBandwidthTestEventSink* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 2);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002EUpdate(CMBRBandwidthTestEventSink* P_0, _MBRHEURISTICDATA heuristicData)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		BandwidthUpdateArgs bandwidthUpdateArgs = new BandwidthUpdateArgs(Unsafe.ReadUnaligned<long>(ref *(byte*)(&heuristicData)), Unsafe.As<_MBRHEURISTICDATA, float>(ref Unsafe.AddByteOffset(ref heuristicData, 8)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 12)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 16)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 20)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 24)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 28)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 32)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 36)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref heuristicData, 40)), Unsafe.As<_MBRHEURISTICDATA, MBRHeuristicState>(ref Unsafe.AddByteOffset(ref heuristicData, 44)));
		if (((bool*)P_0)[12])
		{
			object[] array = new object[2]
			{
				gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_002EP_0024AAVBandwidthTestInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E*)((byte*)P_0 + 4)),
				bandwidthUpdateArgs
			};
			Application.DeferredInvoke(new DeferredInvokeHandler(BandwidthTestInterop.DeferredOnUpdate), (object)array);
		}
		else
		{
			gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E*)((byte*)P_0 + 4)).OnUpdate(bandwidthUpdateArgs);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECMBRBandwidthTestEventSink_002EError(CMBRBandwidthTestEventSink* P_0, uint dwHresult)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		BandwidthTestErrorArgs bandwidthTestErrorArgs = new BandwidthTestErrorArgs((int)dwHresult);
		if (((bool*)P_0)[12])
		{
			object[] array = new object[2]
			{
				gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_002EP_0024AAVBandwidthTestInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E*)((byte*)P_0 + 4)),
				bandwidthTestErrorArgs
			};
			Application.DeferredInvoke(new DeferredInvokeHandler(BandwidthTestInterop.DeferredOnError), (object)array);
		}
		else
		{
			gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E*)((byte*)P_0 + 4)).OnError(bandwidthTestErrorArgs);
		}
		return 0;
	}

	internal unsafe static gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E* gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E* P_0, BandwidthTestInterop t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static BandwidthTestInterop gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_002EP_0024AAVBandwidthTestInterop_0040MicrosoftZunePlayback_0040_0040(gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (BandwidthTestInterop)((GCHandle)intPtr).Target;
	}

	internal unsafe static BandwidthTestInterop gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZunePlayback_003A_003ABandwidthTestInterop_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (BandwidthTestInterop)((GCHandle)intPtr).Target;
	}

	internal unsafe static MessagingCallbackWrapper* Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002E_007Bctor_007D(MessagingCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E*)((byte*)P_0 + 8));
		try
		{
			gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 12));
			try
			{
				((int*)P_0)[1] = 0;
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AObject_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(MessagingCallback callback, object state, IMessagingCallback** ppMessagingCallback)
	{
		if (ppMessagingCallback == null)
		{
			_ZuneShipAssert(1001u, 71u);
			return -2147467261;
		}
		MessagingCallbackWrapper* ptr = (MessagingCallbackWrapper*)@new(16u);
		MessagingCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002E_007Bctor_007D(ptr));
		}
		catch
		{
			//try-fault
			delete(ptr);
			throw;
		}
		int num;
		if (ptr2 == null)
		{
			num = -2147024882;
		}
		else
		{
			num = Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002EInit(ptr2, callback, state);
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, (_GUID*)Unsafe.AsPointer(ref _GUID_39977545_c867_4bd4_b5a1_e4ee473a1e8b), (void**)ppMessagingCallback);
				if (num >= 0)
				{
					goto IL_006c;
				}
			}
			Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002E_007Bdtor_007D(ptr2);
			delete(ptr2);
		}
		goto IL_006c;
		IL_006c:
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002E_007Bdtor_007D(MessagingCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MessagingCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002EInit(MessagingCallbackWrapper* P_0, MessagingCallback callback, object state)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E*)((byte*)P_0 + 8), callback);
		gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 12), state);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002EQueryInterface(MessagingCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			*(int*)ppUnknown = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			return 0;
		}
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_39977545_c867_4bd4_b5a1_e4ee473a1e8b)) != 0)
		{
			*(int*)ppUnknown = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002EAddRef(MessagingCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ERelease(MessagingCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002EOnComplete(MessagingCallbackWrapper* P_0, int hrResponse, IZuneNetResponse* pZuneNetRespone)
	{
		HRESULT hr = hrResponse;
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		object target = ((GCHandle)intPtr).Target;
		IntPtr intPtr2 = new IntPtr((void*)(int)((uint*)P_0)[2]);
		((GCHandle)intPtr2).Target(hr, target);
		return 0;
	}

	internal unsafe static AddCommentCallbackWrapper* Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002E_007Bctor_007D(AddCommentCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E*)((byte*)P_0 + 8));
		try
		{
			((int*)P_0)[1] = 0;
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002ECreateInstance(CommentCallback callback, IMessagingCallback** ppMessagingCallback)
	{
		if (ppMessagingCallback == null)
		{
			_ZuneShipAssert(1001u, 188u);
			return -2147467261;
		}
		AddCommentCallbackWrapper* ptr = (AddCommentCallbackWrapper*)@new(12u);
		AddCommentCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002E_007Bctor_007D(ptr));
		}
		catch
		{
			//try-fault
			delete(ptr);
			throw;
		}
		int num;
		if (ptr2 == null)
		{
			num = -2147024882;
		}
		else
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E*)((byte*)ptr2 + 8), callback);
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, (_GUID*)Unsafe.AsPointer(ref _GUID_39977545_c867_4bd4_b5a1_e4ee473a1e8b), (void**)ppMessagingCallback);
			if (num < 0)
			{
				Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002E_007Bdtor_007D(ptr2);
				delete(ptr2);
			}
		}
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002E_007Bdtor_007D(AddCommentCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AddCommentCallbackWrapper_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002EQueryInterface(AddCommentCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			*(int*)ppUnknown = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			return 0;
		}
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_39977545_c867_4bd4_b5a1_e4ee473a1e8b)) != 0)
		{
			*(int*)ppUnknown = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002EAddRef(AddCommentCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002ERelease(AddCommentCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002EOnComplete(AddCommentCallbackWrapper* P_0, int hrResponse, IZuneNetResponse* pZuneNetRespone)
	{
		int num = 0;
		if (pZuneNetRespone == null)
		{
			num = -2147467261;
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIZuneNetCommentResponse_003E cComPtrNtv_003CIZuneNetCommentResponse_003E);
		*(int*)(&cComPtrNtv_003CIZuneNetCommentResponse_003E) = 0;
		try
		{
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pZuneNetRespone)))((nint)pZuneNetRespone, (_GUID*)Unsafe.AsPointer(ref _GUID_1221e242_5ca1_4024_8789_9778b59ca5de), (void**)(&cComPtrNtv_003CIZuneNetCommentResponse_003E));
			}
			_GUID gUID_NULL = GUID_NULL;
			if (num >= 0)
			{
				Unsafe.SkipInit(out _GUID gUID);
				int num2 = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, _GUID*>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIZuneNetCommentResponse_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIZuneNetCommentResponse_003E)), &gUID);
				// IL cpblk instruction
				Unsafe.CopyBlock(ref gUID_NULL, num2, 16);
			}
			Guid commentId = GUIDToGuid(gUID_NULL);
			HRESULT hr = hrResponse;
			IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
			((GCHandle)intPtr).Target(hr, commentId);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIZuneNetCommentResponse_003E*, void>)(&CComPtrNtv_003CIZuneNetCommentResponse_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIZuneNetCommentResponse_003E);
			throw;
		}
		IZuneNetCommentResponse* ptr = (IZuneNetCommentResponse*)(int)(*(uint*)(&cComPtrNtv_003CIZuneNetCommentResponse_003E));
		if (*(int*)(&cComPtrNtv_003CIZuneNetCommentResponse_003E) != 0)
		{
			*(int*)(&cComPtrNtv_003CIZuneNetCommentResponse_003E) = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
		return num;
	}

	internal unsafe static MessagingSubscriber* Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002E_007Bctor_007D(MessagingSubscriber* P_0, MessagingNotifier owner)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040);
		MessagingSubscriber* ptr = (MessagingSubscriber*)((byte*)P_0 + 4);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[2] = 0;
			gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr, owner);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 4);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002E_007Bdtor_007D(MessagingSubscriber* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MessagingSubscriber_0040Messaging_0040Zune_0040Microsoft_0040_00406B_0040);
		MessagingSubscriber* ptr;
		try
		{
			ptr = (MessagingSubscriber*)((byte*)P_0 + 4);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr, null);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 4);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr);
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002ESubscribe(MessagingSubscriber* P_0, EZuneNetMessagingEventType eventType)
	{
		INotifyManager* ptr = null;
		int num = GetSingleton((_GUID)_GUID_fd0ba7bb_76c8_4451_8842_7138fa2edd72, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, void*, int, int, INotifySubscriber*, int>)(int)(*(uint*)(*(int*)ptr + 32)))((nint)ptr, 6, null, (int)eventType, -1, (INotifySubscriber*)P_0);
			INotifyManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002EUnsubscribe(MessagingSubscriber* P_0, EZuneNetMessagingEventType eventType)
	{
		INotifyManager* ptr = null;
		int num = GetSingleton((_GUID)_GUID_fd0ba7bb_76c8_4451_8842_7138fa2edd72, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, void*, int, int, INotifySubscriber*, int>)(int)(*(uint*)(*(int*)ptr + 40)))((nint)ptr, 6, null, (int)eventType, -1, (INotifySubscriber*)P_0);
			INotifyManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	internal unsafe static uint Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002EAddRef(MessagingSubscriber* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 2);
	}

	internal unsafe static uint Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002ERelease(MessagingSubscriber* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 2);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002EQueryInterface(MessagingSubscriber* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 1308u);
			return -2147467261;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00e9004f_0cab_40ff_98ae_fad1a5ca594d)) != 0)
		{
			*(int*)ppv = (int)P_0;
		}
		else
		{
			if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0)
			{
				*(int*)ppv = 0;
				return -2147467262;
			}
			*(int*)ppv = (int)P_0;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002ENotification(MessagingSubscriber* P_0, int iCategory, void* pSourceInstance, int iType, int iSubType, IUnknown* pData)
	{
		switch (iType)
		{
		case 1:
		{
			MessagingSubscriber* ptr2 = (MessagingSubscriber*)((byte*)P_0 + 4);
			if (gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002EP_0024AAVMessagingNotifier_0040Messaging_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr2) != null)
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr2).raise_OnDeviceMessagesPosted(iSubType);
			}
			break;
		}
		case 2:
		{
			MessagingSubscriber* ptr3 = (MessagingSubscriber*)((byte*)P_0 + 4);
			if (gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002EP_0024AAVMessagingNotifier_0040Messaging_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr3) != null)
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr3).raise_OnComposeCompleted(iSubType);
			}
			break;
		}
		case 3:
		{
			MessagingSubscriber* ptr = (MessagingSubscriber*)((byte*)P_0 + 4);
			if (gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002EP_0024AAVMessagingNotifier_0040Messaging_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr) != null)
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E*)ptr).raise_OnDeviceCartItemsPosted(iSubType);
			}
			break;
		}
		}
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EMessaging_002EMessagingSubscriber_002ENotification(MessagingSubscriber* P_0, int iCategory, void* pSourceInstance, ushort* strType, ushort* strSubType, IUnknown* pData)
	{
		return -2147418113;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingCallback_0020_005E_003E* P_0, MessagingCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003ACommentCallback_0020_005E_003E* P_0, CommentCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIZuneNetCommentResponse_003E_002E_007Bdtor_007D(CComPtrNtv_003CIZuneNetCommentResponse_003E* P_0)
	{
		CComPtrNtv_003CIZuneNetCommentResponse_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIMessagingCallback_003E_002E_007Bdtor_007D(CComPtrNtv_003CIMessagingCallback_003E* P_0)
	{
		CComPtrNtv_003CIMessagingCallback_003E_002ERelease(P_0);
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E* P_0, MessagingNotifier t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static MessagingNotifier gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002EP_0024AAVMessagingNotifier_0040Messaging_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (MessagingNotifier)((GCHandle)intPtr).Target;
	}

	internal unsafe static MessagingNotifier gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoft_003A_003AZune_003A_003AMessaging_003A_003AMessagingNotifier_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (MessagingNotifier)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIZuneNetCommentResponse_003E_002ERelease(CComPtrNtv_003CIZuneNetCommentResponse_003E* P_0)
	{
		int num = *(int*)P_0;
		IZuneNetCommentResponse* ptr = (IZuneNetCommentResponse*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIMessagingCallback_003E_002ERelease(CComPtrNtv_003CIMessagingCallback_003E* P_0)
	{
		int num = *(int*)P_0;
		IMessagingCallback* ptr = (IMessagingCallback*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static NativeMetadataNotifications* MicrosoftZuneLibrary_002ENativeMetadataNotifications_002E_007Bctor_007D(NativeMetadataNotifications* P_0, MetadataMgrNotifications metadataNotify)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7NativeMetadataNotifications_0040MicrosoftZuneLibrary_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)((byte*)P_0 + 8), metadataNotify);
		return P_0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EQueryInterface(NativeMetadataNotifications* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_d67cdf64_5ea9_44ea_bf5c_29a422f4c23f)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EAddRef(NativeMetadataNotifications* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ENativeMetadataNotifications_002ERelease(NativeMetadataNotifications* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002ENativeMetadataNotifications_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002E_007Bdtor_007D(NativeMetadataNotifications* P_0)
	{
		gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnFileAdded(NativeMetadataNotifications* P_0, METADATA_ADD_MEDIA* addFile)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileAddedHandler != null)
		{
			IntPtr sourceUrl = (IntPtr)(void*)(int)(*(uint*)addFile);
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileAddedHandler(sourceUrl, ((EMediaTypes*)addFile)[1]);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnFileError(NativeMetadataNotifications* P_0, ushort* pwszUrl)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileErrorHandler != null)
		{
			IntPtr sourceUrl = (IntPtr)pwszUrl;
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileErrorHandler(sourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnFileRemoved(NativeMetadataNotifications* P_0, ushort* pwszUrl)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileRemovedHandler != null)
		{
			IntPtr sourceUrl = (IntPtr)pwszUrl;
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileRemovedHandler(sourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnBeginScanDirectory(NativeMetadataNotifications* P_0, ushort* pwszUrl)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginScanDirectoryHandler != null)
		{
			IntPtr sourceUrl = (IntPtr)pwszUrl;
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginScanDirectoryHandler(sourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnEndScanDirectory(NativeMetadataNotifications* P_0, ushort* pwszUrl)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndScanDirectoryHandler != null)
		{
			IntPtr sourceUrl = (IntPtr)pwszUrl;
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndScanDirectoryHandler(sourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnScanComplete(NativeMetadataNotifications* P_0)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_ScanCompletedHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_ScanCompletedHandler();
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnBeginFileChange(NativeMetadataNotifications* P_0)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginFileChangeHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginFileChangeHandler();
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnEndFileChange(NativeMetadataNotifications* P_0)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndFileChangeHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndFileChangeHandler();
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnMetadataUpdate(NativeMetadataNotifications* P_0, METADATA_UPDATE_ALBUM* albumUpdate)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_MetadataUpdateHandler != null)
		{
			IntPtr albumTitle = (IntPtr)(void*)(int)((uint*)albumUpdate)[1];
			IntPtr artistName = (IntPtr)(void*)(int)(*(uint*)albumUpdate);
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_MetadataUpdateHandler(artistName, albumTitle);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnBeginMetadataLifecycle(NativeMetadataNotifications* P_0)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginMetadataLifecycleHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginMetadataLifecycleHandler();
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnEndMetadataLifecycle(NativeMetadataNotifications* P_0)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndMetadataLifecycleHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndMetadataLifecycleHandler();
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnBeginMigrate(NativeMetadataNotifications* P_0)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginMigrateHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_BeginMigrateHandler();
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnEndMigrate(NativeMetadataNotifications* P_0, int hrMigration)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndMigrateHandler != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_EndMigrateHandler();
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnFileDeleteFailed(NativeMetadataNotifications* P_0, ushort* pwszUrl)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileDeleteFailedHandler != null)
		{
			IntPtr sourceUrl = (IntPtr)pwszUrl;
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FileDeleteFailedHandler(sourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ENativeMetadataNotifications_002EOnFingerprintingFile(NativeMetadataNotifications* P_0, ushort* pwszUrl)
	{
		NativeMetadataNotifications* ptr = (NativeMetadataNotifications*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FingerprintingFileHandler != null)
		{
			IntPtr sourceUrl = (IntPtr)pwszUrl;
			gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E*)ptr).m_FingerprintingFileHandler(sourceUrl);
		}
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E* P_0, MetadataMgrNotifications t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static MetadataMgrNotifications gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003AMetadataMgrNotifications_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (MetadataMgrNotifications)((GCHandle)intPtr).Target;
	}

	internal unsafe static NSSMediator* NSSMediator_002E_007Bctor_007D(NSSMediator* P_0, INSSManager* pNSSManager, HMESettings hmeSettings)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7NSSMediator_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		NSSMediator* ptr = (NSSMediator*)((byte*)P_0 + 8);
		gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E*)ptr);
		try
		{
			if (null != pNSSManager)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNSSManager + 4)))((nint)pNSSManager);
			}
			((int*)P_0)[3] = (int)pNSSManager;
			if (pNSSManager != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IUnknown*, ushort**, int>)(int)(*(uint*)(*(int*)pNSSManager + 56)))((nint)pNSSManager, (IUnknown*)P_0, (ushort**)((byte*)P_0 + 16));
			}
			gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_003D((gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E*)ptr, hmeSettings);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void NSSMediator_002EShutdown(NSSMediator* P_0)
	{
		uint num = ((uint*)P_0)[3];
		if (num != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)(int)num + 60)))((IntPtr)(int)num, (ushort*)(int)((uint*)P_0)[4]);
		}
		int num2 = ((int*)P_0)[4];
		if (num2 != 0)
		{
			SysFreeString((ushort*)num2);
		}
		uint num3 = ((uint*)P_0)[3];
		if (0 != num3)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num3 + 8)))((IntPtr)(int)num3);
			((int*)P_0)[3] = 0;
		}
	}

	internal unsafe static int NSSMediator_002ENotify(NSSMediator* P_0, EventReason Reason, ushort* ID, tagVARIANT* pData)
	{
		NSSMediator* ptr = (NSSMediator*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_002EP_0024AAVHMESettings_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E*)ptr) != null && ((EventReason)1001 == Reason || (EventReason)1002 == Reason))
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E*)ptr).NSSDeviceListChange();
		}
		return 0;
	}

	internal unsafe static int NSSMediator_002EQueryInterface(NSSMediator* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_78046458_f53a_43b3_a59a_b608960e3a6b)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint NSSMediator_002EAddRef(NSSMediator* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint NSSMediator_002ERelease(NSSMediator* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			NSSMediator_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void NSSMediator_002E_007Bdtor_007D(NSSMediator* P_0)
	{
		gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E*)((byte*)P_0 + 8));
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_003D(gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E* P_0, HMESettings t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static HMESettings gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_002EP_0024AAVHMESettings_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (HMESettings)((GCHandle)intPtr).Target;
	}

	internal unsafe static HMESettings gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003AHMESettings_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (HMESettings)((GCHandle)intPtr).Target;
	}

	internal unsafe static CPlayerInteropEventSink* MicrosoftZunePlayback_002ECPlayerInteropEventSink_002E_007Bctor_007D(CPlayerInteropEventSink* P_0, PlayerInterop managedPlayer)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerEvents_0040_0040_0040);
		((int*)P_0)[1] = (int)Unsafe.AsPointer(ref _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCTransportEvents_0040_0040_0040);
		((int*)P_0)[2] = (int)Unsafe.AsPointer(ref _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerSetUriEvents_0040_0040_0040);
		CPlayerInteropEventSink* ptr = (CPlayerInteropEventSink*)((byte*)P_0 + 16);
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[3] = 1;
			gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_003D((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)ptr, managedPlayer);
			((int*)P_0)[5] = 0;
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
			throw;
		}
	}

	internal unsafe static void MicrosoftZunePlayback_002ECPlayerInteropEventSink_002E_007Bdtor_007D(CPlayerInteropEventSink* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerEvents_0040_0040_0040);
		((int*)P_0)[1] = (int)Unsafe.AsPointer(ref _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCTransportEvents_0040_0040_0040);
		((int*)P_0)[2] = (int)Unsafe.AsPointer(ref _003F_003F_7CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_00406BIMCPlayerSetUriEvents_0040_0040_0040);
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)((byte*)P_0 + 16));
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EQueryInterface(CPlayerInteropEventSink* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_576ef3d5_d89e_482e_ad2c_889c8e504983)) == 0)
		{
			if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_b1d19423_6060_493c_90cd_c6e39f0fa760)) != 0)
			{
				CPlayerInteropEventSink* ptr = (CPlayerInteropEventSink*)((P_0 == null) ? null : ((byte*)P_0 + 4));
				*(int*)ppUnknown = (int)ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
				return 0;
			}
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EAddRef(CPlayerInteropEventSink* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 3);
	}

	internal unsafe static uint MicrosoftZunePlayback_002ECPlayerInteropEventSink_002ERelease(CPlayerInteropEventSink* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 3);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZunePlayback_002ECPlayerInteropEventSink_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EAlertOccurred(CPlayerInteropEventSink* P_0, MCHResultAnnouncement* pAlert)
	{
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_002EP_0024AAVPlayerInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)((byte*)P_0 + 16)).OnAlertOccurred(pAlert);
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EStatusChanged(CPlayerInteropEventSink* P_0, MCPlayerStatus* pStatus)
	{
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_002EP_0024AAVPlayerInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)((byte*)P_0 + 16)).OnStatusChanged(pStatus);
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EStreamChanged(CPlayerInteropEventSink* P_0, MCStreamChangeType type, MCStreamDescriptor* pDescriptor)
	{
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EPropertyChanged(CPlayerInteropEventSink* P_0, ushort* wzkey, byte* pData, uint dataLength)
	{
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_002EP_0024AAVPlayerInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)((byte*)P_0 + 16)).OnPropertyChanged(wzkey, pData, dataLength);
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002ETransportStatusChanged(CPlayerInteropEventSink* P_0, MCTransportStatus* pTransportStatus)
	{
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_002EP_0024AAVPlayerInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)((byte*)P_0 + 12)).OnTransportStatusChanged(pTransportStatus);
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002ETransportPositionChanged(CPlayerInteropEventSink* P_0, long position, long minSeekPosition, long maxSeekPosition)
	{
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_002EP_0024AAVPlayerInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)((byte*)P_0 + 12)).OnTransportPositionChanged(position, minSeekPosition, maxSeekPosition);
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EProgressiveDownloadFileIsClosed(CPlayerInteropEventSink* P_0)
	{
		uint num = ((uint*)P_0)[4];
		if (num != 0)
		{
			SetEvent((void*)(int)num);
		}
		return 0;
	}

	internal unsafe static int MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EUriSet(CPlayerInteropEventSink* P_0, ushort* wzUri, uint dwParam)
	{
		gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_002EP_0024AAVPlayerInterop_0040MicrosoftZunePlayback_0040_0040((gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E*)((byte*)P_0 + 8)).OnUriSet(wzUri, dwParam);
		return 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E* gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E* gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_003D(gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E* P_0, PlayerInterop t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static PlayerInterop gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E_002E_002EP_0024AAVPlayerInterop_0040MicrosoftZunePlayback_0040_0040(gcroot_003CMicrosoftZunePlayback_003A_003APlayerInterop_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (PlayerInterop)((GCHandle)intPtr).Target;
	}

	internal unsafe static uint _003FAddRef_0040CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_0040_0024_0024FW3AGKXZ(CPlayerInteropEventSink* P_0)
	{
		P_0 = (CPlayerInteropEventSink*)((byte*)P_0 - 4);
		return MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EAddRef(P_0);
	}

	internal unsafe static uint _003FAddRef_0040CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_0040_0024_0024FW7AGKXZ(CPlayerInteropEventSink* P_0)
	{
		P_0 = (CPlayerInteropEventSink*)((byte*)P_0 - 8);
		return MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EAddRef(P_0);
	}

	internal unsafe static int _003FQueryInterface_0040CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_0040_0024_0024FW3AGJABU_GUID_0040_0040PAPAX_0040Z(CPlayerInteropEventSink* P_0, _GUID* riid, void** ppUnknown)
	{
		P_0 = (CPlayerInteropEventSink*)((byte*)P_0 - 4);
		return MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EQueryInterface(P_0, riid, ppUnknown);
	}

	internal unsafe static int _003FQueryInterface_0040CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_0040_0024_0024FW7AGJABU_GUID_0040_0040PAPAX_0040Z(CPlayerInteropEventSink* P_0, _GUID* riid, void** ppUnknown)
	{
		P_0 = (CPlayerInteropEventSink*)((byte*)P_0 - 8);
		return MicrosoftZunePlayback_002ECPlayerInteropEventSink_002EQueryInterface(P_0, riid, ppUnknown);
	}

	internal unsafe static uint _003FRelease_0040CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_0040_0024_0024FW3AGKXZ(CPlayerInteropEventSink* P_0)
	{
		P_0 = (CPlayerInteropEventSink*)((byte*)P_0 - 4);
		return MicrosoftZunePlayback_002ECPlayerInteropEventSink_002ERelease(P_0);
	}

	internal unsafe static uint _003FRelease_0040CPlayerInteropEventSink_0040MicrosoftZunePlayback_0040_0040_0024_0024FW7AGKXZ(CPlayerInteropEventSink* P_0)
	{
		P_0 = (CPlayerInteropEventSink*)((byte*)P_0 - 8);
		return MicrosoftZunePlayback_002ECPlayerInteropEventSink_002ERelease(P_0);
	}

	internal unsafe static PlaylistAsyncOperation* Microsoft_002EZune_002EPlaylist_002EPlaylistAsyncOperation_002E_007Bctor_007D(PlaylistAsyncOperation* P_0)
	{
		*(int*)P_0 = -1;
		((int*)P_0)[1] = 0;
		gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E*)((byte*)P_0 + 8));
		return P_0;
	}

	internal unsafe static void Microsoft_002EZune_002EPlaylist_002EPlaylistAsyncOperation_002E_007Bdtor_007D(PlaylistAsyncOperation* P_0)
	{
		try
		{
			PlaylistAsyncOperation* ptr = (PlaylistAsyncOperation*)((byte*)P_0 + 4);
			uint num = *(uint*)ptr;
			if (num != 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
				*(int*)ptr = 0;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EPlaylist_002EPlaylistAsyncOperation_002EAsyncSavePlaylistAsStatic(PlaylistAsyncOperation* P_0, int nPlaylistId, IPlaylistManager* pPlaylistManager, PlaylistAsyncOperationCompleted playlistAsyncOperationCompleted)
	{
		if (pPlaylistManager == null)
		{
			return -2147467261;
		}
		if (null == playlistAsyncOperationCompleted)
		{
			return -2147467261;
		}
		int result = 0;
		*(int*)P_0 = nPlaylistId;
		((int*)P_0)[1] = (int)pPlaylistManager;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pPlaylistManager + 4)))((nint)pPlaylistManager);
		gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E*)((byte*)P_0 + 8), playlistAsyncOperationCompleted);
		if (QueueUserWorkItem((delegate* unmanaged[Stdcall, Stdcall]<void*, uint>)__unep_0040_003FSavePlaylistAsStaticThreadProc_0040PlaylistAsyncOperation_0040Playlist_0040Zune_0040Microsoft_0040_0040_0024_0024FCGKPAX_0040Z, P_0, 0u) == 0)
		{
			uint lastError = GetLastError();
			result = (((int)lastError > 0) ? ((int)(lastError & 0xFFFF) | -2147024896) : ((int)lastError));
		}
		return result;
	}

	internal unsafe static uint Microsoft_002EZune_002EPlaylist_002EPlaylistAsyncOperation_002ESavePlaylistAsStaticThreadProc(void* lpParameter)
	{
		int num = Microsoft_002EZune_002EPlaylist_002EPlaylistAsyncOperation_002ESavePlaylistAsStatic((PlaylistAsyncOperation*)lpParameter);
		if (lpParameter != null)
		{
			Microsoft_002EZune_002EPlaylist_002EPlaylistAsyncOperation_002E_007Bdtor_007D((PlaylistAsyncOperation*)lpParameter);
			delete(lpParameter);
		}
		return (uint)(num & 0xFFFF);
	}

	internal unsafe static int Microsoft_002EZune_002EPlaylist_002EPlaylistAsyncOperation_002ESavePlaylistAsStatic(PlaylistAsyncOperation* P_0)
	{
		PlaylistAsyncOperationCompleted playlistAsyncOperationCompleted = gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_002EP_0024AAVPlaylistAsyncOperationCompleted_0040Playlist_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E*)((byte*)P_0 + 8));
		int num = ((int*)P_0)[1];
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)num + 40)))((IntPtr)num, *(int*)P_0);
		if (null != playlistAsyncOperationCompleted)
		{
			HRESULT hRESULT = new HRESULT(num2);
			playlistAsyncOperationCompleted(hRESULT);
		}
		return num2;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E* P_0, PlaylistAsyncOperationCompleted t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static PlaylistAsyncOperationCompleted gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E_002E_002EP_0024AAVPlaylistAsyncOperationCompleted_0040Playlist_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003APlaylist_003A_003APlaylistAsyncOperationCompleted_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (PlaylistAsyncOperationCompleted)((GCHandle)intPtr).Target;
	}

	internal unsafe static QuickMixCallbackProxy* Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bctor_007D(QuickMixCallbackProxy* P_0, SimilarMediaBatchHandler similarBatchHandler, BatchEndHandler batchEndHandler)
	{
		QuickMixCallbackProxy* ptr = (QuickMixCallbackProxy*)((byte*)P_0 + 4);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7IQuickMixStatusCallback_0040_00406B_0040);
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixSessionCallback_0040_0040_0040);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixStatusCallback_0040_0040_0040);
		((int*)P_0)[2] = 1;
		QuickMixCallbackProxy* ptr2 = (QuickMixCallbackProxy*)((byte*)P_0 + 12);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E*)ptr2);
		try
		{
			QuickMixCallbackProxy* ptr3 = (QuickMixCallbackProxy*)((byte*)P_0 + 16);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E*)ptr3);
			try
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E*)((byte*)P_0 + 20));
				try
				{
					gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E*)ptr2, similarBatchHandler);
					gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E*)ptr3, batchEndHandler);
					return P_0;
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
	}

	internal unsafe static QuickMixCallbackProxy* Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bctor_007D(QuickMixCallbackProxy* P_0, QuickMixProgressHandler progressHandler)
	{
		QuickMixCallbackProxy* ptr = (QuickMixCallbackProxy*)((byte*)P_0 + 4);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7IQuickMixStatusCallback_0040_00406B_0040);
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixSessionCallback_0040_0040_0040);
		*(int*)ptr = (int)Unsafe.AsPointer(ref _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixStatusCallback_0040_0040_0040);
		((int*)P_0)[2] = 1;
		gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E*)((byte*)P_0 + 12));
		try
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E*)((byte*)P_0 + 16));
			try
			{
				QuickMixCallbackProxy* ptr2 = (QuickMixCallbackProxy*)((byte*)P_0 + 20);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E*)ptr2);
				try
				{
					gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E*)ptr2, progressHandler);
					return P_0;
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 20);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bdtor_007D(QuickMixCallbackProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixSessionCallback_0040_0040_0040);
		((int*)P_0)[1] = (int)Unsafe.AsPointer(ref _003F_003F_7QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_00406BIQuickMixStatusCallback_0040_0040_0040);
		try
		{
			try
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E*)((byte*)P_0 + 20));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
				throw;
			}
			gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E*)((byte*)P_0 + 16));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E*)((byte*)P_0 + 12));
	}

	internal unsafe static int Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002EQueryInterface(QuickMixCallbackProxy* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_588d1e9b_4619_4520_ad4d_f4880b74a506)) == 0)
		{
			if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_e76e9fcf_b179_412c_b12e_e1a454c9bfbb)) != 0)
			{
				QuickMixCallbackProxy* ptr = (QuickMixCallbackProxy*)((P_0 == null) ? null : ((byte*)P_0 + 4));
				*(int*)ppUnknown = (int)ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
				return 0;
			}
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002EAddRef(QuickMixCallbackProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 2);
	}

	internal unsafe static uint Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002ERelease(QuickMixCallbackProxy* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 2);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002ESimilarMediaBatch(QuickMixCallbackProxy* P_0, uint cItems, QUICK_MIX_MEDIA_INFO** ppItems)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		SimilarMediaBatchHandler target = (SimilarMediaBatchHandler)((GCHandle)intPtr).Target;
		if (!(null != target))
		{
			return;
		}
		ArrayList arrayList = new ArrayList((int)cItems);
		uint num = 0u;
		if (0 < cItems)
		{
			do
			{
				QUICK_MIX_MEDIA_INFO* ptr = (QUICK_MIX_MEDIA_INFO*)(int)(*(uint*)((int)(num * 4) + (byte*)ppItems));
				string title = new string((char*)(int)((uint*)ptr)[10]);
				string artistName = new string((char*)(int)((uint*)ptr)[16]);
				Guid serviceMediaId = GUIDToGuid(*(_GUID*)((byte*)ptr + 4));
				QuickMixItem value = new QuickMixItem(((int*)ptr)[5], title, artistName, ((int*)ptr)[8], ((bool*)ptr)[36], serviceMediaId);
				arrayList.Add(value);
				num++;
			}
			while (num < cItems);
		}
		IntPtr intPtr2 = new IntPtr((void*)(int)((uint*)P_0)[3]);
		((GCHandle)intPtr2).Target(arrayList);
	}

	internal unsafe static void Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002EBatchEnd(QuickMixCallbackProxy* P_0, int hrBatch)
	{
		HRESULT hr = new HRESULT(hrBatch);
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[4]);
		((GCHandle)intPtr).Target(hr);
	}

	internal unsafe static void Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002EOnProgress(QuickMixCallbackProxy* P_0, float fpProgress, int nSecondsRemaining)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[4]);
		((GCHandle)intPtr).Target(fpProgress, nSecondsRemaining);
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ASimilarMediaBatchHandler_0020_005E_003E* P_0, SimilarMediaBatchHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003ABatchEndHandler_0020_005E_003E* P_0, BatchEndHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AQuickMix_003A_003AQuickMixProgressHandler_0020_005E_003E* P_0, QuickMixProgressHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIQuickMixManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CIQuickMixManager_003E* P_0)
	{
		CComPtrNtv_003CIQuickMixManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIQuickMixSession_003E_002E_007Bdtor_007D(CComPtrNtv_003CIQuickMixSession_003E* P_0)
	{
		CComPtrNtv_003CIQuickMixSession_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIQuickMixManager_003E_002ERelease(CComPtrNtv_003CIQuickMixManager_003E* P_0)
	{
		int num = *(int*)P_0;
		IQuickMixManager* ptr = (IQuickMixManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIQuickMixSession_003E_002ERelease(CComPtrNtv_003CIQuickMixSession_003E* P_0)
	{
		int num = *(int*)P_0;
		IQuickMixSession* ptr = (IQuickMixSession*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static uint _003FRelease_0040QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_0040_0024_0024FW3AGKXZ(QuickMixCallbackProxy* P_0)
	{
		P_0 = (QuickMixCallbackProxy*)((byte*)P_0 - 4);
		return Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002ERelease(P_0);
	}

	internal unsafe static uint _003FAddRef_0040QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_0040_0024_0024FW3AGKXZ(QuickMixCallbackProxy* P_0)
	{
		P_0 = (QuickMixCallbackProxy*)((byte*)P_0 - 4);
		return Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002EAddRef(P_0);
	}

	internal unsafe static int _003FQueryInterface_0040QuickMixCallbackProxy_0040QuickMix_0040Zune_0040Microsoft_0040_0040_0024_0024FW3AGJABU_GUID_0040_0040PAPAX_0040Z(QuickMixCallbackProxy* P_0, _GUID* riid, void** ppUnknown)
	{
		P_0 = (QuickMixCallbackProxy*)((byte*)P_0 - 4);
		return Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002EQueryInterface(P_0, riid, ppUnknown);
	}

	internal unsafe static RadioStationProxy* Microsoft_002EZune_002EUtil_002ERadioStationProxy_002E_007Bctor_007D(RadioStationProxy* P_0, RadioStationProgressHandler radioStationProgressHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7RadioStationProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 1;
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E*)((byte*)P_0 + 8), radioStationProgressHandler);
		return P_0;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002ERadioStationProxy_002EOnComplete(RadioStationProxy* P_0, int hr)
	{
		RadioStationProgressHandler radioStationProgressHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E_002E_002EP_0024AAVRadioStationProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E*)((byte*)P_0 + 8));
		HRESULT hr2 = hr;
		radioStationProgressHandler(hr2);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002ERadioStationProxy_002EQueryInterface(RadioStationProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 97u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002ERadioStationProxy_002EAddRef(RadioStationProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002ERadioStationProxy_002ERelease(RadioStationProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 1);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002EUtil_002ERadioStationProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002ERadioStationProxy_002E_007Bdtor_007D(RadioStationProxy* P_0)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E* P_0, RadioStationProgressHandler t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static RadioStationProgressHandler gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E_002E_002EP_0024AAVRadioStationProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ARadioStationProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (RadioStationProgressHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static RecordManagerCallback* MicrosoftZuneLibrary_002ERecordManagerCallback_002E_007Bctor_007D(RecordManagerCallback* P_0, ZuneLibraryCDRecorder recorder)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7RecordManagerCallback_0040MicrosoftZuneLibrary_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)((byte*)P_0 + 8), recorder);
		return P_0;
	}

	internal unsafe static void MicrosoftZuneLibrary_002ERecordManagerCallback_002EOnRecordStart(RecordManagerCallback* P_0, ushort* pwszSourceUrl)
	{
		RecordManagerCallback* ptr = (RecordManagerCallback*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDRecorder_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr).OnRecordStart(pwszSourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ERecordManagerCallback_002EOnRecordPause(RecordManagerCallback* P_0, ushort* pwszSourceUrl)
	{
		RecordManagerCallback* ptr = (RecordManagerCallback*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDRecorder_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr).OnRecordPause(pwszSourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ERecordManagerCallback_002EOnRecordResume(RecordManagerCallback* P_0, ushort* pwszSourceUrl)
	{
		RecordManagerCallback* ptr = (RecordManagerCallback*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDRecorder_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr).OnRecordResume(pwszSourceUrl);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ERecordManagerCallback_002EOnRecordStop(RecordManagerCallback* P_0, ushort* pwszSourceUrl, int hr)
	{
		RecordManagerCallback* ptr = (RecordManagerCallback*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDRecorder_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr).OnRecordStop(pwszSourceUrl, hr);
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002ERecordManagerCallback_002EOnRecordProgress(RecordManagerCallback* P_0, ushort* pwszSourceUrl, int iTick)
	{
		RecordManagerCallback* ptr = (RecordManagerCallback*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDRecorder_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)ptr).OnRecordProgress(pwszSourceUrl, iTick);
		}
	}

	internal unsafe static int MicrosoftZuneLibrary_002ERecordManagerCallback_002EQueryInterface(RecordManagerCallback* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_dbb19183_e14e_49cc_a75a_0dbf88f7cc57)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ERecordManagerCallback_002EAddRef(RecordManagerCallback* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint MicrosoftZuneLibrary_002ERecordManagerCallback_002ERelease(RecordManagerCallback* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			MicrosoftZuneLibrary_002ERecordManagerCallback_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void MicrosoftZuneLibrary_002ERecordManagerCallback_002E_007Bdtor_007D(RecordManagerCallback* P_0)
	{
		gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E* P_0, ZuneLibraryCDRecorder t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static ZuneLibraryCDRecorder gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002EP_0024AAVZuneLibraryCDRecorder_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (ZuneLibraryCDRecorder)((GCHandle)intPtr).Target;
	}

	internal unsafe static ZuneLibraryCDRecorder gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003AZuneLibraryCDRecorder_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (ZuneLibraryCDRecorder)((GCHandle)intPtr).Target;
	}

	internal unsafe static RefreshCallback* Microsoft_002EZune_002EConfiguration_002ERefreshCallback_002E_007Bctor_007D(RefreshCallback* P_0, TunerInfoHandler tunerInfoHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7RefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040);
		RefreshCallback* ptr = (RefreshCallback*)((byte*)P_0 + 12);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[1] = 1;
			((int*)P_0)[2] = 0;
			gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*)ptr, tunerInfoHandler);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002ERefreshCallback_002EQueryInterface(RefreshCallback* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_9e9b9023_b31c_47ed_b609_58361bdae7d3)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EConfiguration_002ERefreshCallback_002EAddRef(RefreshCallback* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EConfiguration_002ERefreshCallback_002ERelease(RefreshCallback* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0)
		{
			if (P_0 != null)
			{
				*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7RefreshCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*)((byte*)P_0 + 12));
				delete(P_0);
			}
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002ERefreshCallback_002EOnDataReady(RefreshCallback* P_0, int cTunerInfo, TunerInfo* rgTunerInfo, ushort* pwszNextPCDeregistrationDate, ushort* pwszNextSubscriptionDeviceDeregistrationDate, ushort* pwszNextAppStoreDeviceDeregistrationDate)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		((TunerInfoHandler)((GCHandle)intPtr).Target).UpdateTunerInfoLists(cTunerInfo, rgTunerInfo, pwszNextPCDeregistrationDate, pwszNextSubscriptionDeviceDeregistrationDate, pwszNextAppStoreDeviceDeregistrationDate);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002ERefreshCallback_002EOnError(RefreshCallback* P_0, int hrErrorCode)
	{
		((int*)P_0)[2] = hrErrorCode;
		return 0;
	}

	internal unsafe static DeregisterCallback* Microsoft_002EZune_002EConfiguration_002EDeregisterCallback_002E_007Bctor_007D(DeregisterCallback* P_0, TunerInfoHandler tunerInfoHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040);
		DeregisterCallback* ptr = (DeregisterCallback*)((byte*)P_0 + 12);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[1] = 1;
			((int*)P_0)[2] = 0;
			gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*)ptr, tunerInfoHandler);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002EDeregisterCallback_002EQueryInterface(DeregisterCallback* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_0714c405_844f_457c_b141_7664765cb87a)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EConfiguration_002EDeregisterCallback_002EAddRef(DeregisterCallback* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EConfiguration_002EDeregisterCallback_002ERelease(DeregisterCallback* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0)
		{
			if (P_0 != null)
			{
				*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DeregisterCallback_0040Configuration_0040Zune_0040Microsoft_0040_00406B_0040);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E*)((byte*)P_0 + 12));
				delete(P_0);
			}
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002EDeregisterCallback_002EOnSuccess(DeregisterCallback* P_0, ushort* pwszTunerId, ETunerType tunerType, ETunerRegisterType eTunerRegisterType)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		((TunerInfoHandler)((GCHandle)intPtr).Target).FinishRemoveTunerInfo(pwszTunerId, (TunerType)tunerType, (TunerRegisterType)eTunerRegisterType);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EConfiguration_002EDeregisterCallback_002EOnError(DeregisterCallback* P_0, ushort* pwszTunerId, ETunerType tunerType, ETunerRegisterType eTunerRegisterType, int hrErrorCode)
	{
		((int*)P_0)[2] = hrErrorCode;
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		((TunerInfoHandler)((GCHandle)intPtr).Target).ReportError(((int*)P_0)[2]);
		return 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AConfiguration_003A_003ATunerInfoHandler_0020_005E_003E* P_0, TunerInfoHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static MusicTrackMetadata* MusicTrackMetadata_002E_007Bctor_007D(MusicTrackMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MusicTrackMetadata_0040_00406B_0040);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 20, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 36, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 52, ref GUID_NULL, 16);
		((int*)P_0)[17] = 0;
		((int*)P_0)[18] = 0;
		((int*)P_0)[19] = 0;
		((int*)P_0)[20] = 0;
		((int*)P_0)[21] = 0;
		((int*)P_0)[22] = 0;
		((int*)P_0)[23] = 0;
		((int*)P_0)[24] = 0;
		((int*)P_0)[25] = 0;
		((int*)P_0)[26] = 0;
		((int*)P_0)[27] = 0;
		((int*)P_0)[28] = 0;
		return P_0;
	}

	internal unsafe static void MusicTrackMetadata_002E_007Bdtor_007D(MusicTrackMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MusicTrackMetadata_0040_00406B_0040);
		MusicTrackMetadata_002EClear(P_0);
	}

	internal unsafe static MusicAlbumMetadata* MusicAlbumMetadata_002E_007Bctor_007D(MusicAlbumMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MusicAlbumMetadata_0040_00406B_0040);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 20, ref GUID_NULL, 16);
		((int*)P_0)[9] = 0;
		((int*)P_0)[10] = 0;
		((int*)P_0)[11] = 0;
		((int*)P_0)[12] = 0;
		((int*)P_0)[13] = 0;
		((int*)P_0)[14] = 0;
		((int*)P_0)[15] = 0;
		((int*)P_0)[16] = 0;
		((int*)P_0)[17] = 0;
		((int*)P_0)[18] = 0;
		return P_0;
	}

	internal unsafe static void MusicAlbumMetadata_002E_007Bdtor_007D(MusicAlbumMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7MusicAlbumMetadata_0040_00406B_0040);
		MusicAlbumMetadata_002EClear(P_0);
	}

	internal unsafe static VideoMetadata* VideoMetadata_002E_007Bctor_007D(VideoMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7VideoMetadata_0040_00406B_0040);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		((int*)P_0)[5] = -1;
		((int*)P_0)[6] = 0;
		((int*)P_0)[7] = 0;
		((int*)P_0)[8] = 0;
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 36, ref GUID_NULL, 16);
		((int*)P_0)[13] = 0;
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 56, ref GUID_NULL, 16);
		((int*)P_0)[18] = 0;
		((int*)P_0)[19] = 0;
		((int*)P_0)[20] = 0;
		((int*)P_0)[21] = 0;
		((int*)P_0)[22] = 0;
		((int*)P_0)[23] = 0;
		((int*)P_0)[24] = 0;
		((int*)P_0)[25] = 0;
		((int*)P_0)[26] = 0;
		((int*)P_0)[27] = 0;
		((int*)P_0)[28] = 0;
		((int*)P_0)[29] = 0;
		((int*)P_0)[30] = 0;
		((int*)P_0)[31] = 0;
		return P_0;
	}

	internal unsafe static void VideoMetadata_002E_007Bdtor_007D(VideoMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7VideoMetadata_0040_00406B_0040);
		VideoMetadata_002EClear(P_0);
	}

	internal unsafe static AppMetadata* AppMetadata_002E_007Bctor_007D(AppMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AppMetadata_0040_00406B_0040);
		// IL cpblk instruction
		Unsafe.CopyBlock((byte*)P_0 + 4, ref GUID_NULL, 16);
		((int*)P_0)[5] = 0;
		((int*)P_0)[6] = 0;
		((int*)P_0)[7] = 0;
		((int*)P_0)[8] = 0;
		((int*)P_0)[9] = 0;
		((int*)P_0)[10] = 0;
		((int*)P_0)[11] = 0;
		((int*)P_0)[12] = 0;
		((int*)P_0)[13] = 0;
		((int*)P_0)[14] = 0;
		((int*)P_0)[15] = 0;
		((int*)P_0)[16] = 0;
		((int*)P_0)[17] = 0;
		((int*)P_0)[19] = 0;
		return P_0;
	}

	internal unsafe static void AppMetadata_002E_007Bdtor_007D(AppMetadata* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AppMetadata_0040_00406B_0040);
		AppMetadata_002EClear(P_0);
	}

	internal unsafe static GetBalancesCallbackWrapper* Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002E_007Bctor_007D(GetBalancesCallbackWrapper* P_0, GetBalancesCompleteCallback completeCallback, GetBalancesErrorCallback errorCallback)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		GetBalancesCallbackWrapper* ptr = (GetBalancesCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E*)ptr);
		try
		{
			GetBalancesCallbackWrapper* ptr2 = (GetBalancesCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E*)ptr2);
			try
			{
				((int*)P_0)[1] = 1;
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E*)ptr, completeCallback);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E*)ptr2, errorCallback);
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002E_007Bdtor_007D(GetBalancesCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetBalancesCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002EQueryInterface(GetBalancesCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002EAddRef(GetBalancesCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002ERelease(GetBalancesCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002E__delDtor(P_0, 1u);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002EOnSuccess(GetBalancesCallbackWrapper* P_0, int pointsBalance, int freeTrackBalance)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		((GCHandle)intPtr).Target(pointsBalance, freeTrackBalance);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002EOnError(GetBalancesCallbackWrapper* P_0, int hr)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		object? target = ((GCHandle)intPtr).Target;
		HRESULT hr2 = hr;
		target(hr2);
		return 0;
	}

	internal unsafe static void* Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002E__delDtor(GetBalancesCallbackWrapper* P_0, uint P_1)
	{
		Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static GetOffersCallbackWrapper* Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002E_007Bctor_007D(GetOffersCallbackWrapper* P_0, GetOffersCompleteCallback completeCallback, GetOffersErrorCallback errorCallback, IDictionary mapIdToContext)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		GetOffersCallbackWrapper* ptr = (GetOffersCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E*)ptr);
		try
		{
			GetOffersCallbackWrapper* ptr2 = (GetOffersCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E*)ptr2);
			try
			{
				GetOffersCallbackWrapper* ptr3 = (GetOffersCallbackWrapper*)((byte*)P_0 + 16);
				gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E*)ptr3);
				try
				{
					((int*)P_0)[1] = 1;
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E*)ptr, completeCallback);
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E*)ptr2, errorCallback);
					gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E*)ptr3, mapIdToContext);
					return P_0;
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002E_007Bdtor_007D(GetOffersCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			try
			{
				gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E*)((byte*)P_0 + 16));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002EQueryInterface(GetOffersCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002EAddRef(GetOffersCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002ERelease(GetOffersCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002EOnSuccess(GetOffersCallbackWrapper* P_0, IMusicTrackCollection* pTrackCollection, IMusicAlbumCollection* pAlbumCollection, IVideoCollection* pVideoCollection, IAppCollection* pAppCollection)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[4]);
		IDictionary target = (IDictionary)((GCHandle)intPtr).Target;
		AlbumOfferCollection albumOfferCollection = new AlbumOfferCollection();
		int num = albumOfferCollection.Init(pAlbumCollection, target);
		TrackOfferCollection trackOfferCollection = new TrackOfferCollection();
		if (num >= 0)
		{
			num = trackOfferCollection.Init(pTrackCollection, target);
		}
		VideoOfferCollection videoOfferCollection = new VideoOfferCollection();
		if (num >= 0)
		{
			num = videoOfferCollection.Init(pVideoCollection);
		}
		AppOfferCollection appOfferCollection = new AppOfferCollection();
		if (num >= 0)
		{
			num = appOfferCollection.Init(pAppCollection);
		}
		int totalSubscriptionFreeTracks = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pTrackCollection + 56)))((nint)pTrackCollection);
		IntPtr intPtr2 = new IntPtr((void*)(int)((uint*)P_0)[2]);
		((GCHandle)intPtr2).Target(albumOfferCollection, trackOfferCollection, videoOfferCollection, appOfferCollection, totalSubscriptionFreeTracks);
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002EOnError(GetOffersCallbackWrapper* P_0, int hr)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		object? target = ((GCHandle)intPtr).Target;
		HRESULT hr2 = hr;
		target(hr2);
		return 0;
	}

	internal unsafe static GetOfferDetailsCallbackWrapper* Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002E_007Bctor_007D(GetOfferDetailsCallbackWrapper* P_0, _GUID offerId, object state, IService* pService, GetOfferDetailsCompleteCallback completeCallback, GetOfferDetailsErrorCallback errorCallback)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		GetOfferDetailsCallbackWrapper* ptr = (GetOfferDetailsCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr);
		try
		{
			GetOfferDetailsCallbackWrapper* ptr2 = (GetOfferDetailsCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E*)ptr2);
			try
			{
				GetOfferDetailsCallbackWrapper* ptr3 = (GetOfferDetailsCallbackWrapper*)((byte*)P_0 + 16);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E*)ptr3);
				try
				{
					GetOfferDetailsCallbackWrapper* ptr4 = (GetOfferDetailsCallbackWrapper*)((byte*)P_0 + 36);
					*(int*)ptr4 = 0;
					try
					{
						((int*)P_0)[1] = 1;
						CComPtrNtv_003CIService_003E_002E_003D((CComPtrNtv_003CIService_003E*)ptr4, pService);
						// IL cpblk instruction
						Unsafe.CopyBlock((byte*)P_0 + 20, ref offerId, 16);
						gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)ptr, state);
						gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E*)ptr2, completeCallback);
						gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E*)ptr3, errorCallback);
						return P_0;
					}
					catch
					{
						//try-fault
						___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIService_003E*, void>)(&CComPtrNtv_003CIService_003E_002E_007Bdtor_007D), (byte*)P_0 + 36);
						throw;
					}
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AObject_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002E_007Bdtor_007D(GetOfferDetailsCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetOfferDetailsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			try
			{
				try
				{
					CComPtrNtv_003CIService_003E_002ERelease((CComPtrNtv_003CIService_003E*)((byte*)P_0 + 36));
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
					throw;
				}
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E*)((byte*)P_0 + 16));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AObject_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CSystem_003A_003AObject_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AObject_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002EQueryInterface(GetOfferDetailsCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002EAddRef(GetOfferDetailsCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002ERelease(GetOfferDetailsCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002EOnSuccess(GetOfferDetailsCallbackWrapper* P_0, IMediaOfferCollection* pOffers)
	{
		int num = 0;
		if (pOffers == null)
		{
			num = -2147467261;
		}
		int num2 = 0;
		if (num >= 0)
		{
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pOffers + 12)))((nint)pOffers);
			if (num2 > 1)
			{
				num = -2147418113;
			}
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIMusicAlbumCollection_003E cComPtrNtv_003CIMusicAlbumCollection_003E);
		*(int*)(&cComPtrNtv_003CIMusicAlbumCollection_003E) = 0;
		try
		{
			if (num >= 0)
			{
				int num3 = ((int*)P_0)[9];
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMusicAlbumCollection**, int>)(int)(*(uint*)(*(int*)num3 + 20)))((IntPtr)num3, (IMusicAlbumCollection**)(&cComPtrNtv_003CIMusicAlbumCollection_003E));
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIMusicTrackCollection_003E cComPtrNtv_003CIMusicTrackCollection_003E);
			*(int*)(&cComPtrNtv_003CIMusicTrackCollection_003E) = 0;
			try
			{
				if (num >= 0)
				{
					int num4 = ((int*)P_0)[9];
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMusicTrackCollection**, int>)(int)(*(uint*)(*(int*)num4 + 24)))((IntPtr)num4, (IMusicTrackCollection**)(&cComPtrNtv_003CIMusicTrackCollection_003E));
				}
				Unsafe.SkipInit(out CComPtrNtv_003CIVideoCollection_003E cComPtrNtv_003CIVideoCollection_003E);
				*(int*)(&cComPtrNtv_003CIVideoCollection_003E) = 0;
				try
				{
					if (num >= 0)
					{
						int num5 = ((int*)P_0)[9];
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IVideoCollection**, int>)(int)(*(uint*)(*(int*)num5 + 32)))((IntPtr)num5, (IVideoCollection**)(&cComPtrNtv_003CIVideoCollection_003E));
					}
					Microsoft.Zune.Service.EClientTypeFlags clientTypes = Microsoft.Zune.Service.EClientTypeFlags.None;
					if (num2 > 0)
					{
						_GUID gUID_NULL = GUID_NULL;
						EPurchaseOfferType ePurchaseOfferType = (EPurchaseOfferType)(-1);
						_GUID gUID_NULL2 = GUID_NULL;
						Unsafe.SkipInit(out CComPtrNtv_003CIMediaRights_003E cComPtrNtv_003CIMediaRights_003E);
						*(int*)(&cComPtrNtv_003CIMediaRights_003E) = 0;
						try
						{
							if (num >= 0)
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, _GUID*, EPurchaseOfferType*, ushort**, _GUID*, EClientTypeFlags*, IMediaRights**, int>)(int)(*(uint*)(*(int*)pOffers + 16)))((nint)pOffers, 0, &gUID_NULL, &ePurchaseOfferType, null, &gUID_NULL2, (EClientTypeFlags*)(&clientTypes), (IMediaRights**)(&cComPtrNtv_003CIMediaRights_003E));
								if (num >= 0)
								{
									if (IsEqualGUID(&gUID_NULL, (_GUID*)((byte*)P_0 + 20)) == 0)
									{
										num = -2147418113;
									}
									if (num >= 0 && ePurchaseOfferType >= (EPurchaseOfferType)2 && ePurchaseOfferType <= (EPurchaseOfferType)5)
									{
										int num6 = ((int*)P_0)[9];
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, IContextData*, IMediaRights*, ushort*, int, int, IVideoCollection*, IMusicAlbumCollection*, int>)(int)(*(uint*)(*(int*)num6 + 348)))((IntPtr)num6, &gUID_NULL2, null, (IMediaRights*)(int)(*(uint*)(&cComPtrNtv_003CIMediaRights_003E)), null, 0, 0, (IVideoCollection*)(int)(*(uint*)(&cComPtrNtv_003CIVideoCollection_003E)), null);
									}
								}
							}
						}
						catch
						{
							//try-fault
							___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMediaRights_003E*, void>)(&CComPtrNtv_003CIMediaRights_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMediaRights_003E);
							throw;
						}
						IMediaRights* ptr = (IMediaRights*)(int)(*(uint*)(&cComPtrNtv_003CIMediaRights_003E));
						if (*(int*)(&cComPtrNtv_003CIMediaRights_003E) != 0)
						{
							*(int*)(&cComPtrNtv_003CIMediaRights_003E) = 0;
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
						}
					}
					AlbumOfferCollection albumOfferCollection = new AlbumOfferCollection();
					if (num >= 0)
					{
						num = albumOfferCollection.Init((IMusicAlbumCollection*)(int)(*(uint*)(&cComPtrNtv_003CIMusicAlbumCollection_003E)), null);
					}
					TrackOfferCollection trackOfferCollection = new TrackOfferCollection();
					if (num >= 0)
					{
						num = trackOfferCollection.Init((IMusicTrackCollection*)(int)(*(uint*)(&cComPtrNtv_003CIMusicTrackCollection_003E)), null);
					}
					VideoOfferCollection videoOfferCollection = new VideoOfferCollection();
					if (num < 0)
					{
						goto IL_01e7;
					}
					num = videoOfferCollection.Init((IVideoCollection*)(int)(*(uint*)(&cComPtrNtv_003CIVideoCollection_003E)));
					if (num < 0)
					{
						goto IL_01e7;
					}
					IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
					GetOfferDetailsCompleteCallback target = (GetOfferDetailsCompleteCallback)((GCHandle)intPtr).Target;
					if (null == target)
					{
						num = 1;
					}
					else
					{
						IntPtr intPtr2 = new IntPtr((void*)(int)((uint*)P_0)[2]);
						object target2 = ((GCHandle)intPtr2).Target;
						target(albumOfferCollection, trackOfferCollection, videoOfferCollection, clientTypes, target2);
						num = 0;
					}
					goto end_IL_006c;
					IL_01e7:
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)P_0 + 16)))((nint)P_0, num);
					end_IL_006c:;
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIVideoCollection_003E*, void>)(&CComPtrNtv_003CIVideoCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIVideoCollection_003E);
					throw;
				}
				IVideoCollection* ptr2 = (IVideoCollection*)(int)(*(uint*)(&cComPtrNtv_003CIVideoCollection_003E));
				if (*(int*)(&cComPtrNtv_003CIVideoCollection_003E) != 0)
				{
					*(int*)(&cComPtrNtv_003CIVideoCollection_003E) = 0;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMusicTrackCollection_003E*, void>)(&CComPtrNtv_003CIMusicTrackCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMusicTrackCollection_003E);
				throw;
			}
			IMusicTrackCollection* ptr3 = (IMusicTrackCollection*)(int)(*(uint*)(&cComPtrNtv_003CIMusicTrackCollection_003E));
			if (*(int*)(&cComPtrNtv_003CIMusicTrackCollection_003E) != 0)
			{
				*(int*)(&cComPtrNtv_003CIMusicTrackCollection_003E) = 0;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr3 + 8)))((nint)ptr3);
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMusicAlbumCollection_003E*, void>)(&CComPtrNtv_003CIMusicAlbumCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMusicAlbumCollection_003E);
			throw;
		}
		IMusicAlbumCollection* ptr4 = (IMusicAlbumCollection*)(int)(*(uint*)(&cComPtrNtv_003CIMusicAlbumCollection_003E));
		if (*(int*)(&cComPtrNtv_003CIMusicAlbumCollection_003E) != 0)
		{
			*(int*)(&cComPtrNtv_003CIMusicAlbumCollection_003E) = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr4 + 8)))((nint)ptr4);
		}
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002EOnError(GetOfferDetailsCallbackWrapper* P_0, int hr)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[4]);
		GetOfferDetailsErrorCallback target = (GetOfferDetailsErrorCallback)((GCHandle)intPtr).Target;
		int result;
		if (null == target)
		{
			result = 1;
		}
		else
		{
			IntPtr intPtr2 = new IntPtr((void*)(int)((uint*)P_0)[2]);
			object target2 = ((GCHandle)intPtr2).Target;
			HRESULT hr2 = hr;
			target(hr2, target2);
			result = 0;
		}
		return result;
	}

	internal unsafe static GetBillingOffersCallbackWrapper* Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E_007Bctor_007D(GetBillingOffersCallbackWrapper* P_0, GetBillingOffersCompleteCallback completeCallback, GetBillingOffersErrorCallback errorCallback)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		GetBillingOffersCallbackWrapper* ptr = (GetBillingOffersCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E*)ptr);
		try
		{
			GetBillingOffersCallbackWrapper* ptr2 = (GetBillingOffersCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E*)ptr2);
			try
			{
				((int*)P_0)[1] = 1;
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E*)ptr, completeCallback);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E*)ptr2, errorCallback);
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E_007Bdtor_007D(GetBillingOffersCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetBillingOffersCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002EQueryInterface(GetBillingOffersCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002EAddRef(GetBillingOffersCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002ERelease(GetBillingOffersCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E__delDtor(P_0, 1u);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002EOnSuccess(GetBillingOffersCallbackWrapper* P_0, IBillingOfferCollection* pNonMediaCollection)
	{
		BillingOfferCollection billingOfferCollection = new BillingOfferCollection();
		int result = billingOfferCollection.Init(pNonMediaCollection);
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		((GCHandle)intPtr).Target(billingOfferCollection);
		return result;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002EOnError(GetBillingOffersCallbackWrapper* P_0, int hr)
	{
		HRESULT hr2 = hr;
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		((GCHandle)intPtr).Target(hr2);
		return 0;
	}

	internal unsafe static void* Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E__delDtor(GetBillingOffersCallbackWrapper* P_0, uint P_1)
	{
		Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static GetPaymentInstrumentsCallbackWrapper* Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002E_007Bctor_007D(GetPaymentInstrumentsCallbackWrapper* P_0, GetPaymentInstrumentsCompleteCallback completeCallback, GetPaymentInstrumentsErrorCallback errorCallback)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		GetPaymentInstrumentsCallbackWrapper* ptr = (GetPaymentInstrumentsCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E*)ptr);
		try
		{
			GetPaymentInstrumentsCallbackWrapper* ptr2 = (GetPaymentInstrumentsCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E*)ptr2);
			try
			{
				((int*)P_0)[1] = 1;
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E*)ptr, completeCallback);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E*)ptr2, errorCallback);
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002E_007Bdtor_007D(GetPaymentInstrumentsCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7GetPaymentInstrumentsCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002EQueryInterface(GetPaymentInstrumentsCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002EAddRef(GetPaymentInstrumentsCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002ERelease(GetPaymentInstrumentsCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002E__delDtor(P_0, 1u);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002EOnSuccess(GetPaymentInstrumentsCallbackWrapper* P_0, ICreditCardCollection* pCreditCardCollection)
	{
		CreditCardCollection creditCardCollection = new CreditCardCollection();
		int result = creditCardCollection.Init(pCreditCardCollection);
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		((GCHandle)intPtr).Target(creditCardCollection);
		return result;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002EOnError(GetPaymentInstrumentsCallbackWrapper* P_0, int hr)
	{
		HRESULT hr2 = hr;
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		((GCHandle)intPtr).Target(hr2);
		return 0;
	}

	internal unsafe static void* Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002E__delDtor(GetPaymentInstrumentsCallbackWrapper* P_0, uint P_1)
	{
		Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static AddPaymentInstrumentCallbackWrapper* Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002E_007Bctor_007D(AddPaymentInstrumentCallbackWrapper* P_0, PaymentInstrument paymentInstrument, AddPaymentInstrumentCompleteCallback completeCallback, AddPaymentInstrumentErrorCallback errorCallback)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		AddPaymentInstrumentCallbackWrapper* ptr = (AddPaymentInstrumentCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E*)ptr);
		try
		{
			AddPaymentInstrumentCallbackWrapper* ptr2 = (AddPaymentInstrumentCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E*)ptr2);
			try
			{
				AddPaymentInstrumentCallbackWrapper* ptr3 = (AddPaymentInstrumentCallbackWrapper*)((byte*)P_0 + 16);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E*)ptr3);
				try
				{
					((int*)P_0)[1] = 1;
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E*)ptr3, paymentInstrument);
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E*)ptr, completeCallback);
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E*)ptr2, errorCallback);
					return P_0;
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002E_007Bdtor_007D(AddPaymentInstrumentCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7AddPaymentInstrumentCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			try
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E*)((byte*)P_0 + 16));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002EQueryInterface(AddPaymentInstrumentCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002EAddRef(AddPaymentInstrumentCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002ERelease(AddPaymentInstrumentCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002EOnSuccess(AddPaymentInstrumentCallbackWrapper* P_0, ushort* pcwstrPaymentInstrumentId)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[4]);
		((PaymentInstrument)((GCHandle)intPtr).Target).Id = new string((char*)pcwstrPaymentInstrumentId);
		IntPtr intPtr2 = new IntPtr((void*)(int)((uint*)P_0)[4]);
		PaymentInstrument target = (PaymentInstrument)((GCHandle)intPtr2).Target;
		IntPtr intPtr3 = new IntPtr((void*)(int)((uint*)P_0)[2]);
		((GCHandle)intPtr3).Target(target);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002EOnError(AddPaymentInstrumentCallbackWrapper* P_0, int hr)
	{
		HRESULT hr2 = hr;
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		((GCHandle)intPtr).Target(hr2);
		return 0;
	}

	internal unsafe static PurchaseOffersCallbackWrapper* Microsoft_002EZune_002EUtil_002EPurchaseOffersCallbackWrapper_002E_007Bctor_007D(PurchaseOffersCallbackWrapper* P_0, PurchaseOffersCompleteHandler purchaseCompleteHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7PurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		PurchaseOffersCallbackWrapper* ptr = (PurchaseOffersCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E*)ptr);
		try
		{
			((int*)P_0)[1] = 1;
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E*)ptr, purchaseCompleteHandler);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EPurchaseOffersCallbackWrapper_002EQueryInterface(PurchaseOffersCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_0629124a_eb99_447e_9537_f10628f23b78)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EPurchaseOffersCallbackWrapper_002EAddRef(PurchaseOffersCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EPurchaseOffersCallbackWrapper_002ERelease(PurchaseOffersCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0)
		{
			if (P_0 != null)
			{
				*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7PurchaseOffersCallbackWrapper_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
				gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E*)((byte*)P_0 + 8));
				delete(P_0);
			}
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EPurchaseOffersCallbackWrapper_002EOnPurchaseOffersComplete(PurchaseOffersCallbackWrapper* P_0, int hr, ushort* pcwszRedirectUrl, ushort* pcwszPurchaseHandle)
	{
		string redirectUrl = new string((char*)pcwszRedirectUrl);
		string handle = new string((char*)pcwszPurchaseHandle);
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		object? target = ((GCHandle)intPtr).Target;
		HRESULT hr2 = hr;
		target(hr2, redirectUrl, handle);
		return 0;
	}

	internal unsafe static DownloadCallbackWrapper* Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002E_007Bctor_007D(DownloadCallbackWrapper* P_0, DownloadEventHandler eventHandler, DownloadEventProgressHandler progressHandler, EventHandler allPendingHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		DownloadCallbackWrapper* ptr = (DownloadCallbackWrapper*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E*)ptr);
		try
		{
			DownloadCallbackWrapper* ptr2 = (DownloadCallbackWrapper*)((byte*)P_0 + 12);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E*)ptr2);
			try
			{
				DownloadCallbackWrapper* ptr3 = (DownloadCallbackWrapper*)((byte*)P_0 + 16);
				gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E*)ptr3);
				try
				{
					((int*)P_0)[1] = 1;
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E*)ptr, eventHandler);
					gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E*)ptr2, progressHandler);
					gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E*)ptr3, allPendingHandler);
					return P_0;
				}
				catch
				{
					//try-fault
					___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E*, void>)(&gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
					throw;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002E_007Bdtor_007D(DownloadCallbackWrapper* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7DownloadCallbackWrapper_0040Service_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			try
			{
				gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E*)((byte*)P_0 + 16));
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
			gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EQueryInterface(DownloadCallbackWrapper* P_0, _GUID* riid, void** ppUnknown)
	{
		if (IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) == 0 && IsEqualGUID(riid, (_GUID*)Unsafe.AsPointer(ref _GUID_2644d109_c175_4da3_b051_4085b4254c83)) == 0)
		{
			return -2147467262;
		}
		*(int*)ppUnknown = (int)P_0;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		return 0;
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EAddRef(DownloadCallbackWrapper* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002ERelease(DownloadCallbackWrapper* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EOnPending(DownloadCallbackWrapper* P_0, _GUID mediaId)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		object? target = ((GCHandle)intPtr).Target;
		HRESULT hr = -2147483638;
		Guid mediaId2 = GUIDToGuid(mediaId);
		target(mediaId2, hr);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EOnAllPending(DownloadCallbackWrapper* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[4]);
		((GCHandle)intPtr).Target(null, null);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EOnProgress(DownloadCallbackWrapper* P_0, _GUID mediaId, float percent)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[3]);
		object? target = ((GCHandle)intPtr).Target;
		Guid mediaId2 = GUIDToGuid(mediaId);
		target(mediaId2, percent);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EOnComplete(DownloadCallbackWrapper* P_0, _GUID mediaId, int hr)
	{
		IntPtr intPtr = new IntPtr((void*)(int)((uint*)P_0)[2]);
		object? target = ((GCHandle)intPtr).Target;
		HRESULT hr2 = hr;
		Guid mediaId2 = GUIDToGuid(mediaId);
		target(mediaId2, hr2);
		return 0;
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EProgressivePlaybackReleaseFile(DownloadCallbackWrapper* P_0)
	{
		PlayerInterop.Instance?.ProgressivePlaybackReleaseFile();
	}

	internal unsafe static void Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002EProgressivePlaybackReopenFile(DownloadCallbackWrapper* P_0)
	{
		PlayerInterop.Instance?.ProgressivePlaybackReopenFile();
	}

	internal unsafe static CComPtrNtv_003CIContextData_003E* CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(CComPtrNtv_003CIContextData_003E* P_0)
	{
		*(int*)P_0 = 0;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(CComPtrNtv_003CIContextData_003E* P_0)
	{
		CComPtrNtv_003CIContextData_003E_002ERelease(P_0);
	}

	internal unsafe static IContextData* CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(CComPtrNtv_003CIContextData_003E* P_0)
	{
		return (IContextData*)(int)(*(uint*)P_0);
	}

	internal unsafe static IContextData** CComPtrNtv_003CIContextData_003E_002E_0026(CComPtrNtv_003CIContextData_003E* P_0)
	{
		return (IContextData**)P_0;
	}

	internal unsafe static CComPtrNtv_003CIPriceInfo_003E* CComPtrNtv_003CIPriceInfo_003E_002E_007Bctor_007D(CComPtrNtv_003CIPriceInfo_003E* P_0)
	{
		*(int*)P_0 = 0;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D(CComPtrNtv_003CIPriceInfo_003E* P_0)
	{
		CComPtrNtv_003CIPriceInfo_003E_002ERelease(P_0);
	}

	internal unsafe static IPriceInfo* CComPtrNtv_003CIPriceInfo_003E_002E_002EPAUIPriceInfo_0040_0040(CComPtrNtv_003CIPriceInfo_003E* P_0)
	{
		return (IPriceInfo*)(int)(*(uint*)P_0);
	}

	internal unsafe static IPriceInfo** CComPtrNtv_003CIPriceInfo_003E_002E_0026(CComPtrNtv_003CIPriceInfo_003E* P_0)
	{
		return (IPriceInfo**)P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesCompleteCallback_0020_005E_003E* P_0, GetBalancesCompleteCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBalancesErrorCallback_0020_005E_003E* P_0, GetBalancesErrorCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersCompleteCallback_0020_005E_003E* P_0, GetOffersCompleteCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOffersErrorCallback_0020_005E_003E* P_0, GetOffersErrorCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E* gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E_002E_007Bctor_007D(gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E* gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E_002E_003D(gcroot_003CSystem_003A_003ACollections_003A_003AIDictionary_0020_005E_003E* P_0, IDictionary t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsCompleteCallback_0020_005E_003E* P_0, GetOfferDetailsCompleteCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetOfferDetailsErrorCallback_0020_005E_003E* P_0, GetOfferDetailsErrorCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static IService* CComPtrNtv_003CIService_003E_002E_003D(CComPtrNtv_003CIService_003E* P_0, IService* lp)
	{
		CComPtrNtv_003CIService_003E_002ERelease(P_0);
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return (IService*)(int)(*(uint*)P_0);
	}

	internal unsafe static void CComPtrNtv_003CIMusicAlbumCollection_003E_002E_007Bdtor_007D(CComPtrNtv_003CIMusicAlbumCollection_003E* P_0)
	{
		CComPtrNtv_003CIMusicAlbumCollection_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIMusicTrackCollection_003E_002E_007Bdtor_007D(CComPtrNtv_003CIMusicTrackCollection_003E* P_0)
	{
		CComPtrNtv_003CIMusicTrackCollection_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIVideoCollection_003E_002E_007Bdtor_007D(CComPtrNtv_003CIVideoCollection_003E* P_0)
	{
		CComPtrNtv_003CIVideoCollection_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIMediaRights_003E_002E_007Bdtor_007D(CComPtrNtv_003CIMediaRights_003E* P_0)
	{
		CComPtrNtv_003CIMediaRights_003E_002ERelease(P_0);
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersCompleteCallback_0020_005E_003E* P_0, GetBillingOffersCompleteCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetBillingOffersErrorCallback_0020_005E_003E* P_0, GetBillingOffersErrorCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsCompleteCallback_0020_005E_003E* P_0, GetPaymentInstrumentsCompleteCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AGetPaymentInstrumentsErrorCallback_0020_005E_003E* P_0, GetPaymentInstrumentsErrorCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentCompleteCallback_0020_005E_003E* P_0, AddPaymentInstrumentCompleteCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003AAddPaymentInstrumentErrorCallback_0020_005E_003E* P_0, AddPaymentInstrumentErrorCallback t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003APaymentInstrument_0020_005E_003E* P_0, PaymentInstrument t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003APurchaseOffersCompleteHandler_0020_005E_003E* P_0, PurchaseOffersCompleteHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventHandler_0020_005E_003E* P_0, DownloadEventHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AService_003A_003ADownloadEventProgressHandler_0020_005E_003E* P_0, DownloadEventProgressHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E* gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E* gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E_002E_003D(gcroot_003CSystem_003A_003AEventHandler_0020_005E_003E* P_0, EventHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D(CComPtrNtv_003CISignInState_003E* P_0)
	{
		CComPtrNtv_003CISignInState_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIGetOfferDetailsCallback_003E_002E_007Bdtor_007D(CComPtrNtv_003CIGetOfferDetailsCallback_003E* P_0)
	{
		CComPtrNtv_003CIGetOfferDetailsCallback_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIGetOfferDetailsCallback_003E_002EAttach(CComPtrNtv_003CIGetOfferDetailsCallback_003E* P_0, IGetOfferDetailsCallback* p2)
	{
		uint num = *(uint*)P_0;
		if ((IGetOfferDetailsCallback*)(int)num != p2)
		{
			if (num != 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			}
			*(int*)P_0 = (int)p2;
		}
	}

	internal unsafe static void CComPtrNtv_003CITokenDetails_003E_002E_007Bdtor_007D(CComPtrNtv_003CITokenDetails_003E* P_0)
	{
		CComPtrNtv_003CITokenDetails_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIAppCollection_003E_002E_007Bdtor_007D(CComPtrNtv_003CIAppCollection_003E* P_0)
	{
		CComPtrNtv_003CIAppCollection_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIContextData_003E_002ERelease(CComPtrNtv_003CIContextData_003E* P_0)
	{
		int num = *(int*)P_0;
		IContextData* ptr = (IContextData*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIPriceInfo_003E_002ERelease(CComPtrNtv_003CIPriceInfo_003E* P_0)
	{
		int num = *(int*)P_0;
		IPriceInfo* ptr = (IPriceInfo*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIMusicAlbumCollection_003E_002ERelease(CComPtrNtv_003CIMusicAlbumCollection_003E* P_0)
	{
		int num = *(int*)P_0;
		IMusicAlbumCollection* ptr = (IMusicAlbumCollection*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIMusicTrackCollection_003E_002ERelease(CComPtrNtv_003CIMusicTrackCollection_003E* P_0)
	{
		int num = *(int*)P_0;
		IMusicTrackCollection* ptr = (IMusicTrackCollection*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIVideoCollection_003E_002ERelease(CComPtrNtv_003CIVideoCollection_003E* P_0)
	{
		int num = *(int*)P_0;
		IVideoCollection* ptr = (IVideoCollection*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIMediaRights_003E_002ERelease(CComPtrNtv_003CIMediaRights_003E* P_0)
	{
		int num = *(int*)P_0;
		IMediaRights* ptr = (IMediaRights*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CISignInState_003E_002ERelease(CComPtrNtv_003CISignInState_003E* P_0)
	{
		int num = *(int*)P_0;
		ISignInState* ptr = (ISignInState*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIGetOfferDetailsCallback_003E_002ERelease(CComPtrNtv_003CIGetOfferDetailsCallback_003E* P_0)
	{
		int num = *(int*)P_0;
		IGetOfferDetailsCallback* ptr = (IGetOfferDetailsCallback*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CITokenDetails_003E_002ERelease(CComPtrNtv_003CITokenDetails_003E* P_0)
	{
		int num = *(int*)P_0;
		ITokenDetails* ptr = (ITokenDetails*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIAppCollection_003E_002ERelease(CComPtrNtv_003CIAppCollection_003E* P_0)
	{
		int num = *(int*)P_0;
		IAppCollection* ptr = (IAppCollection*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static DynamicArray_003Cunsigned_0020short_0020_002A_003E* DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bctor_007D(DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7_003F_0024DynamicArray_0040PAG_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		((int*)P_0)[2] = 0;
		((int*)P_0)[3] = 0;
		return P_0;
	}

	internal unsafe static void DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7_003F_0024DynamicArray_0040PAG_0040_00406B_0040);
		if (((int*)P_0)[3] > 0)
		{
			delete_005B_005D((void*)(int)((uint*)P_0)[1]);
		}
		((int*)P_0)[1] = 0;
		((int*)P_0)[2] = 0;
		((int*)P_0)[3] = 0;
	}

	internal unsafe static ushort** DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_005B_005D(DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_0, int iIndx)
	{
		uint* ptr = null;
		if (iIndx < 0 || (iIndx >= ((int*)P_0)[2] && iIndx >= ((int*)P_0)[3]))
		{
			*ptr = 0u;
		}
		if (iIndx >= ((int*)P_0)[2])
		{
			((int*)P_0)[2] = iIndx + 1;
		}
		return (ushort**)(iIndx * 4 + ((int*)P_0)[1]);
	}

	internal unsafe static void DynamicArray_003Cunsigned_0020short_0020_002A_003E_002ERemoveAllElements(DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_0, [MarshalAs(UnmanagedType.U1)] bool fFreeMem)
	{
		if (fFreeMem)
		{
			if (((int*)P_0)[3] > 0)
			{
				delete_005B_005D((void*)(int)((uint*)P_0)[1]);
			}
			((int*)P_0)[1] = 0;
			((int*)P_0)[3] = 0;
		}
		((int*)P_0)[2] = 0;
	}

	internal unsafe static void* DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E__vecDelDtor(DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			DynamicArray_003Cunsigned_0020short_0020_002A_003E* ptr = (DynamicArray_003Cunsigned_0020short_0020_002A_003E*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 16u, *(int*)ptr, (delegate*<void*, void>)(delegate*<DynamicArray_003Cunsigned_0020short_0020_002A_003E*, void>)(&DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static VirtualSubscriptionEpisodeListProxy* Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002E_007Bctor_007D(VirtualSubscriptionEpisodeListProxy* P_0, SubscriptionSeriesInfo seriesInfo, VirtualSubscriptionEpisodeList episodeList)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7VirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 1;
		((int*)P_0)[2] = 0;
		VirtualSubscriptionEpisodeListProxy* ptr = (VirtualSubscriptionEpisodeListProxy*)((byte*)P_0 + 12);
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E*)ptr);
		try
		{
			VirtualSubscriptionEpisodeListProxy* ptr2 = (VirtualSubscriptionEpisodeListProxy*)((byte*)P_0 + 16);
			gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E*)ptr2);
			try
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E*)ptr2, seriesInfo);
				gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E*)ptr, episodeList);
				return P_0;
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002E_007Bdtor_007D(VirtualSubscriptionEpisodeListProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7VirtualSubscriptionEpisodeListProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			try
			{
				VirtualSubscriptionEpisodeListProxy* ptr = (VirtualSubscriptionEpisodeListProxy*)((byte*)P_0 + 8);
				uint num = *(uint*)ptr;
				if (0 != num)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
					*(int*)ptr = 0;
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 16);
				throw;
			}
			gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E*)((byte*)P_0 + 16));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E*)((byte*)P_0 + 12));
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002EQueryInterface(VirtualSubscriptionEpisodeListProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 1116u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_cd97f5a6_9e6e_47a1_9b64_888484f91fc7)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002EAddRef(VirtualSubscriptionEpisodeListProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002ERelease(VirtualSubscriptionEpisodeListProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 1);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002ESeriesInfoRetrieved(VirtualSubscriptionEpisodeListProxy* P_0, IMSMediaSchemaPropertySet* pSeriesPropertySet)
	{
		if (pSeriesPropertySet == null)
		{
			_ZuneShipAssert(1001u, 1167u);
			return -2147467261;
		}
		uint num = ((uint*)P_0)[2];
		if (0 != num)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			((int*)P_0)[2] = 0;
		}
		((int*)P_0)[2] = (int)pSeriesPropertySet;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pSeriesPropertySet + 4)))((nint)pSeriesPropertySet);
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_002EP_0024AAVSubscriptionSeriesInfo_0040Subscription_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E*)((byte*)P_0 + 16))?.SetPropertySet(pSeriesPropertySet);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002EEpisodeInfoRetrieved(VirtualSubscriptionEpisodeListProxy* P_0, IMSMediaSchemaPropertySet* pEpisodePropertySet)
	{
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		int num;
		try
		{
			Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = ((int*)P_0)[2];
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)Unsafe.As<CComPropVariant, uint>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) + 4)))((IntPtr)Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)));
			*(short*)(&cComPropVariant) = 13;
			tagPROPVARIANT tagPROPVARIANT2 = (tagPROPVARIANT)cComPropVariant;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)pEpisodePropertySet + 28)))((nint)pEpisodePropertySet, 3229616385u, tagPROPVARIANT2);
			if (num >= 0)
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_002EP_0024AAVVirtualSubscriptionEpisodeList_0040Subscription_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E*)((byte*)P_0 + 12))?.AddItem(pEpisodePropertySet);
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		CComPropVariant_002EClear(&cComPropVariant);
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002ERefreshCompleted(VirtualSubscriptionEpisodeListProxy* P_0)
	{
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		int num2;
		try
		{
			int num = ((int*)P_0)[2];
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)num + 24)))((IntPtr)num, 100663562u, 0u, (tagPROPVARIANT*)(&cComPropVariant));
			if (num2 >= 0)
			{
				gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_002EP_0024AAVVirtualSubscriptionEpisodeList_0040Subscription_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E*)((byte*)P_0 + 12))?.AsyncOperationCompleted(Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)));
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		CComPropVariant_002EClear(&cComPropVariant);
		return num2;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E* P_0, VirtualSubscriptionEpisodeList t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static VirtualSubscriptionEpisodeList gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E_002E_002EP_0024AAVVirtualSubscriptionEpisodeList_0040Subscription_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003AVirtualSubscriptionEpisodeList_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (VirtualSubscriptionEpisodeList)((GCHandle)intPtr).Target;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E* P_0, SubscriptionSeriesInfo t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static SubscriptionSeriesInfo gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E_002E_002EP_0024AAVSubscriptionSeriesInfo_0040Subscription_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionSeriesInfo_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (SubscriptionSeriesInfo)((GCHandle)intPtr).Target;
	}

	internal unsafe static void DBPropertySubmitStruct_002E_007Bdtor_007D(DBPropertySubmitStruct* P_0)
	{
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool CComPropVariant_002EIsNullOrEmpty(CComPropVariant* P_0)
	{
		ushort num = *(ushort*)P_0;
		int num2 = ((num == 0 || num == 1) ? 1 : 0);
		bool flag = (byte)num2 != 0;
		if (!flag)
		{
			switch (num)
			{
			case 8:
			{
				uint num4 = ((uint*)P_0)[2];
				flag = num4 == 0;
				if (!flag)
				{
					uint num5 = SysStringLen((ushort*)(int)num4);
					int num6 = ((0 == *(ushort*)(int)((uint*)P_0)[2] || 0 == num5) ? 1 : 0);
					flag = (byte)num6 != 0;
				}
				break;
			}
			case 72:
			{
				uint num3 = ((uint*)P_0)[2];
				flag = num3 == 0;
				if (!flag)
				{
					bool flag2 = ((IsEqualGUID((_GUID*)(int)num3, (_GUID*)Unsafe.AsPointer(ref GUID_NULL)) != 0) ? true : false);
					flag = flag2;
				}
				break;
			}
			}
		}
		return flag;
	}

	internal unsafe static CSubscriptionEventProxy* Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002E_007Bctor_007D(CSubscriptionEventProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 1;
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E*)((byte*)P_0 + 28));
		try
		{
			((int*)P_0)[8] = -1;
			((int*)P_0)[9] = -1;
			((int*)P_0)[10] = -1;
			VariantInit((tagVARIANT*)((byte*)P_0 + 8));
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 28);
			throw;
		}
	}

	internal unsafe static void Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002E_007Bdtor_007D(CSubscriptionEventProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CSubscriptionEventProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040);
		try
		{
			VariantClear((tagVARIANT*)((byte*)P_0 + 8));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 28);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E*)((byte*)P_0 + 28));
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EInitialize(CSubscriptionEventProxy* P_0, SubscriptionEventHandler subscriptionEventHandler, int nSubscriptionMediaId, EMediaTypes eSubscriptionMediaType, string subscriptionTitle, SubscriptionAction targetAction, [MarshalAs(UnmanagedType.U1)] bool userInitiated)
	{
		INotifyManager* ptr = null;
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E*)((byte*)P_0 + 28), subscriptionEventHandler);
		((int*)P_0)[6] = (int)targetAction;
		((int*)P_0)[9] = nSubscriptionMediaId;
		((int*)P_0)[10] = (int)eSubscriptionMediaType;
		((sbyte*)P_0)[44] = (userInitiated ? ((sbyte)1) : ((sbyte)0));
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref PtrToStringChars(subscriptionTitle)))
		{
			ushort* ptr3 = SysAllocString(ptr2);
			((int*)P_0)[4] = (int)ptr3;
			int num;
			if (null != ptr3)
			{
				((short*)P_0)[4] = 8;
				num = GetSingleton((_GUID)_GUID_fd0ba7bb_76c8_4451_8842_7138fa2edd72, (void**)(&ptr));
				if (num >= 0)
				{
					((int*)P_0)[8] = 1;
					if (SubscriptionAction.RefreshStarted == targetAction)
					{
						((int*)P_0)[8] = 0;
					}
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, void*, int, int, INotifySubscriber*, int>)(int)(*(uint*)(*(int*)ptr + 32)))((nint)ptr, 3, null, ((int*)P_0)[8], -1, (INotifySubscriber*)P_0);
					INotifyManager* intPtr = ptr;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				}
			}
			else
			{
				num = -2147024882;
			}
			return num;
		}
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EUninitialize(CSubscriptionEventProxy* P_0)
	{
		INotifyManager* ptr = null;
		int num = GetSingleton((_GUID)_GUID_fd0ba7bb_76c8_4451_8842_7138fa2edd72, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, void*, int, int, INotifySubscriber*, int>)(int)(*(uint*)(*(int*)ptr + 40)))((nint)ptr, 3, null, ((int*)P_0)[8], -1, (INotifySubscriber*)P_0);
			INotifyManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002ENotification(CSubscriptionEventProxy* P_0, int iCategory, void* pSourceInstance, int iType, int iSubType, IUnknown* pData)
	{
		Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_0040 _0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402);
		DBPropertyRequestStruct_002E_007Bctor_007D((DBPropertyRequestStruct*)(&_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402), 233u);
		try
		{
			DBPropertyRequestStruct_002E_007Bctor_007D((DBPropertyRequestStruct*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402, 24)), 234u);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&DBPropertyRequestStruct_002E_007Bdtor_007D), &_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402);
			throw;
		}
		int num;
		try
		{
			num = ZuneLibraryExports_002EGetFieldValues(iSubType, EListType.eSubscriptionList, 2, (DBPropertyRequestStruct*)(&_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402), null);
			if (0 == num && 0 == Unsafe.As<_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402, 4)) && 0 == Unsafe.As<_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402, 28)))
			{
				if (((int*)P_0)[9] == Unsafe.As<_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402, 16)) && ((int*)P_0)[10] == Unsafe.As<_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402, 40)))
				{
					SubscriptionEventHandler subscriptionEventHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_002EP_0024AAVSubscriptionEventHandler_0040Subscription_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E*)((byte*)P_0 + 28));
					SubscriptonEventArguments args = new SubscriptonEventArguments(subscriptionTitle: new string((char*)(int)((uint*)P_0)[4]), action: ((SubscriptionAction*)P_0)[6], eMediaType: ((EMediaTypes*)P_0)[10], userInitiated: ((bool*)P_0)[44]);
					subscriptionEventHandler(args);
					num = Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EUninitialize(P_0);
				}
			}
			else
			{
				Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EUninitialize(P_0);
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindVecDtor((delegate*<void*, uint, int, delegate*<void*, void>, void>)(&__ehvec_dtor), (void*)(&_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402), 24u, 2, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&DBPropertyRequestStruct_002E_007Bdtor_007D));
			throw;
		}
		__ehvec_dtor(&_0024ArrayType_0024_0024_0024BY01UDBPropertyRequestStruct_0040_00402, 24u, 2, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&DBPropertyRequestStruct_002E_007Bdtor_007D));
		return num;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002ENotification(CSubscriptionEventProxy* P_0, int iCategory, void* pSourceInstance, ushort* strType, ushort* strSubType, IUnknown* pData)
	{
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EQueryInterface(CSubscriptionEventProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 286u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00e9004f_0cab_40ff_98ae_fad1a5ca594d)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EAddRef(CSubscriptionEventProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002ERelease(CSubscriptionEventProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 1);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static CSubscriptionCredentialProviderProxy* Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002E_007Bctor_007D(CSubscriptionCredentialProviderProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CSubscriptionCredentialProviderProxy_0040Subscription_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 1;
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8));
		return P_0;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002EInitialize(CSubscriptionCredentialProviderProxy* P_0, SubscriptionCredentialHandler subscriptionCredentialHandler)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8), subscriptionCredentialHandler);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002EGetCredential(CSubscriptionCredentialProviderProxy* P_0, int iSubscriptionMediaId, ushort* pwszTargetUrl, EAuthenticationScheme eAuthScheme, int fIsAuthSchemeSafe, ushort* pwszRealm, ushort* pwszLastUserName, int hrLastAuthError, ushort** pbstrUserName, ushort** pbstrPassword, int* pfSave)
	{
		if (pwszTargetUrl == null)
		{
			_ZuneShipAssert(1001u, 463u);
			return -2147467261;
		}
		if (pbstrUserName == null)
		{
			_ZuneShipAssert(1001u, 464u);
			return -2147467261;
		}
		if (pbstrPassword == null)
		{
			_ZuneShipAssert(1001u, 465u);
			return -2147467261;
		}
		if (pfSave == null)
		{
			_ZuneShipAssert(1001u, 466u);
			return -2147467261;
		}
		*(int*)pbstrUserName = 0;
		*(int*)pbstrPassword = 0;
		*pfSave = 0;
		int result = 0;
		string targetUrl = new string((char*)pwszTargetUrl);
		string realm = null;
		string lastUserName = null;
		if (pwszRealm != null)
		{
			realm = new string((char*)pwszRealm);
		}
		if (pwszLastUserName != null)
		{
			lastUserName = new string((char*)pwszLastUserName);
		}
		AuthenticationSchemes authScheme;
		if (eAuthScheme != 0)
		{
			if (eAuthScheme != (EAuthenticationScheme)1)
			{
				if (eAuthScheme != (EAuthenticationScheme)2)
				{
					if (eAuthScheme != (EAuthenticationScheme)3)
					{
						result = -2147418113;
						goto IL_015b;
					}
					authScheme = AuthenticationSchemes.Negotiate;
				}
				else
				{
					authScheme = AuthenticationSchemes.Ntlm;
				}
			}
			else
			{
				authScheme = AuthenticationSchemes.Digest;
			}
		}
		else
		{
			authScheme = AuthenticationSchemes.Basic;
		}
		byte isAuthSchemeSafe = (byte)((fIsAuthSchemeSafe != 0) ? 1 : 0);
		SubscriptonCredentialRequestArguments subscriptonCredentialRequestArguments = new SubscriptonCredentialRequestArguments(iSubscriptionMediaId, targetUrl, authScheme, isAuthSchemeSafe != 0, realm, lastUserName, hrLastAuthError);
		if (gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_002EP_0024AAVSubscriptionCredentialHandler_0040Subscription_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8))(subscriptonCredentialRequestArguments))
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(subscriptonCredentialRequestArguments.Credential.UserName)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref PtrToStringChars(subscriptonCredentialRequestArguments.Credential.Password)))
					{
						try
						{
							*(int*)pbstrUserName = (int)SysAllocString(ptr);
							*(int*)pbstrPassword = (int)SysAllocString(ptr2);
							*pfSave = (subscriptonCredentialRequestArguments.Save ? 1 : 0);
							if (*(int*)pbstrUserName == 0 || *(int*)pbstrPassword == 0)
							{
								result = -2147024882;
							}
						}
						catch
						{
							//try-fault
							ptr2 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		else
		{
			result = -2147467260;
		}
		goto IL_015b;
		IL_015b:
		return result;
	}

	internal unsafe static int Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002EQueryInterface(CSubscriptionCredentialProviderProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 563u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_c1764920_30cf_4e56_81db_202d03556cb9)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002EAddRef(CSubscriptionCredentialProviderProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002ERelease(CSubscriptionCredentialProviderProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 1);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002E_007Bdtor_007D(CSubscriptionCredentialProviderProxy* P_0)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal static int Microsoft_002EZune_002ESubscription_002EIsWellFormedUriString([In][Out] ref string feedUrl)
	{
		int result = 0;
		try
		{
			Uri uri = new Uri(feedUrl, UriKind.Absolute);
			feedUrl = uri.ToString();
		}
		catch (UriFormatException)
		{
			result = -1072884976;
		}
		return result;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E* P_0, SubscriptionEventHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static SubscriptionEventHandler gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E_002E_002EP_0024AAVSubscriptionEventHandler_0040Subscription_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionEventHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (SubscriptionEventHandler)((GCHandle)intPtr).Target;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E* P_0, SubscriptionCredentialHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static SubscriptionCredentialHandler gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E_002E_002EP_0024AAVSubscriptionCredentialHandler_0040Subscription_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003ASubscription_003A_003ASubscriptionCredentialHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (SubscriptionCredentialHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static CComPropVariant* CComPropVariant_002E_003D(CComPropVariant* P_0, int nSrc)
	{
		if (*(ushort*)P_0 != 3)
		{
			CComPropVariant_002EClear(P_0);
			*(short*)P_0 = 3;
		}
		((int*)P_0)[2] = nSrc;
		return P_0;
	}

	internal unsafe static void WPP_SF_LLd(ulong Logger, ushort id, _GUID* TraceGuid, uint _a1, uint _a2, int _a3)
	{
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(&_a1, 4u, &_a2, 4u, &_a3, 4u, 0));
	}

	internal unsafe static void CComPtrNtv_003CIDeviceSyncRulesProvider_003E_002E_007Bdtor_007D(CComPtrNtv_003CIDeviceSyncRulesProvider_003E* P_0)
	{
		CComPtrNtv_003CIDeviceSyncRulesProvider_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIDeviceSyncRulesProvider_003E_002ERelease(CComPtrNtv_003CIDeviceSyncRulesProvider_003E* P_0)
	{
		int num = *(int*)P_0;
		IDeviceSyncRulesProvider* ptr = (IDeviceSyncRulesProvider*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static int GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020ISyncRulesView_003E(IEndpointHost* pC, EEndpointHostProperty propId, ISyncRulesView** ppT)
	{
		if (pC == null)
		{
			_ZuneShipAssert(1001u, 193u);
			return -2147467261;
		}
		if (ppT == null)
		{
			_ZuneShipAssert(1001u, 194u);
			return -2147467261;
		}
		IUnknown* ptr = null;
		*(int*)ppT = 0;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, IUnknown**, int>)(int)(*(uint*)(*(int*)pC + 52)))((nint)pC, propId, &ptr);
		if (num >= 0 && ptr != null)
		{
			num = SafeQueryInterface_003Cstruct_0020ISyncRulesView_003E(ptr, ppT);
		}
		SafeRelease_003Cstruct_0020IUnknown_003E(&ptr);
		return num;
	}

	internal unsafe static int SafeQueryInterface_003Cstruct_0020ISyncRulesView_003E(IUnknown* pUnk, ISyncRulesView** ppT)
	{
		if (ppT != null)
		{
			*(int*)ppT = 0;
			if (pUnk != null)
			{
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)pUnk)))((nint)pUnk, (_GUID*)Unsafe.AsPointer(ref _GUID_f4f0a85f_d136_46d4_ab5d_950d93006ae2), (void**)ppT);
			}
			return -2147467261;
		}
		return -2147467261;
	}

	internal unsafe static SyncRulesViewMediator* SyncRulesViewMediator_002E_007Bctor_007D(SyncRulesViewMediator* P_0, SyncRulesView syncRulesView, ISyncRulesView* pSyncRulesView)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7SyncRulesViewMediator_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*)((byte*)P_0 + 8), syncRulesView);
		try
		{
			SyncRulesViewMediator* ptr = (SyncRulesViewMediator*)((byte*)P_0 + 12);
			*(int*)ptr = 0;
			try
			{
				if (pSyncRulesView != null)
				{
					CComPtrNtv_003CISyncRulesView_003E_002E_003D((CComPtrNtv_003CISyncRulesView_003E*)ptr, pSyncRulesView);
					int num = *(int*)ptr;
					((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, ISyncRulesViewCallback*, int>)(int)(*(uint*)(*(int*)num + 36)))((IntPtr)num, (ISyncRulesViewCallback*)P_0);
				}
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISyncRulesView_003E*, void>)(&CComPtrNtv_003CISyncRulesView_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		return P_0;
	}

	internal unsafe static void SyncRulesViewMediator_002E_007Bdtor_007D(SyncRulesViewMediator* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7SyncRulesViewMediator_0040_00406B_0040);
		try
		{
			try
			{
				SyncRulesViewMediator_002EShutdown(P_0);
			}
			catch
			{
				//try-fault
				___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISyncRulesView_003E*, void>)(&CComPtrNtv_003CISyncRulesView_003E_002E_007Bdtor_007D), (byte*)P_0 + 12);
				throw;
			}
			CComPtrNtv_003CISyncRulesView_003E_002ERelease((CComPtrNtv_003CISyncRulesView_003E*)((byte*)P_0 + 12));
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*, void>)(&gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static void SyncRulesViewMediator_002EShutdown(SyncRulesViewMediator* P_0)
	{
		SyncRulesViewMediator* ptr = (SyncRulesViewMediator*)((byte*)P_0 + 12);
		int num = *(int*)ptr;
		if (num != 0)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, ISyncRulesViewCallback*, int>)(int)(*(uint*)(*(int*)num + 40)))((IntPtr)num, (ISyncRulesViewCallback*)P_0);
			CComPtrNtv_003CISyncRulesView_003E_002ERelease((CComPtrNtv_003CISyncRulesView_003E*)ptr);
		}
	}

	internal unsafe static void SyncRulesViewMediator_002EItemAdded(SyncRulesViewMediator* P_0, uint iItem)
	{
		SyncRulesViewMediator* ptr = (SyncRulesViewMediator*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_002EP_0024AAVSyncRulesView_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*)ptr).InvokeItemAdded(iItem);
		}
	}

	internal unsafe static void SyncRulesViewMediator_002EItemUpdated(SyncRulesViewMediator* P_0, uint iItem)
	{
		SyncRulesViewMediator* ptr = (SyncRulesViewMediator*)((byte*)P_0 + 8);
		if (gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_002EP_0024AAVSyncRulesView_0040MicrosoftZuneLibrary_0040_0040((gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*)ptr) != null)
		{
			gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E*)ptr).InvokeItemUpdated(iItem);
		}
	}

	internal unsafe static int SyncRulesViewMediator_002EQueryInterface(SyncRulesViewMediator* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_8c6c709a_8ed8_4dc2_a530_b206f30497c9)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint SyncRulesViewMediator_002EAddRef(SyncRulesViewMediator* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint SyncRulesViewMediator_002ERelease(SyncRulesViewMediator* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			SyncRulesViewMediator_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E* gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E* P_0, SyncRulesView t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static SyncRulesView gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_002EP_0024AAVSyncRulesView_0040MicrosoftZuneLibrary_0040_0040(gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (SyncRulesView)((GCHandle)intPtr).Target;
	}

	internal unsafe static SyncRulesView gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoftZuneLibrary_003A_003ASyncRulesView_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (SyncRulesView)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CISyncRulesView_003E_002E_007Bdtor_007D(CComPtrNtv_003CISyncRulesView_003E* P_0)
	{
		CComPtrNtv_003CISyncRulesView_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CISyncRulesView_003E_002ERelease(CComPtrNtv_003CISyncRulesView_003E* P_0)
	{
		int num = *(int*)P_0;
		ISyncRulesView* ptr = (ISyncRulesView*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static ISyncRulesView* CComPtrNtv_003CISyncRulesView_003E_002E_003D(CComPtrNtv_003CISyncRulesView_003E* P_0, ISyncRulesView* lp)
	{
		CComPtrNtv_003CISyncRulesView_003E_002ERelease(P_0);
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return (ISyncRulesView*)(int)(*(uint*)P_0);
	}

	internal unsafe static void _003FA0xfcba3ca2_002E_003F_003F__Esm_gcTaskbarPlayer_0040Util_0040Zune_0040Microsoft_0040_0040YMXXZ()
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E*)Unsafe.AsPointer(ref Microsoft_002EZune_002EUtil_002Esm_gcTaskbarPlayer));
		_atexit_m((delegate*<void>)(&_003FA0xfcba3ca2_002E_003F_003F__Fsm_gcTaskbarPlayer_0040Util_0040Zune_0040Microsoft_0040_0040YMXXZ));
	}

	internal unsafe static void _003FA0xfcba3ca2_002E_003F_003F__Fsm_gcTaskbarPlayer_0040Util_0040Zune_0040Microsoft_0040_0040YMXXZ()
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E*)Unsafe.AsPointer(ref Microsoft_002EZune_002EUtil_002Esm_gcTaskbarPlayer));
	}

	internal unsafe static void _003FA0xfcba3ca2_002E_003F_003F__E_003FA0xfcba3ca2_0040s_spTrayDeskBand_0040Util_0040Zune_0040Microsoft_0040_0040YMXXZ()
	{
		_atexit_m((delegate*<void>)(&_003FA0xfcba3ca2_002E_003F_003F__F_003FA0xfcba3ca2_0040s_spTrayDeskBand_0040Util_0040Zune_0040Microsoft_0040_0040YMXXZ));
	}

	internal unsafe static void _003FA0xfcba3ca2_002E_003F_003F__F_003FA0xfcba3ca2_0040s_spTrayDeskBand_0040Util_0040Zune_0040Microsoft_0040_0040YMXXZ()
	{
		CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E_002ERelease((CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E*)Unsafe.AsPointer(ref Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand));
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002ECommandDispatcherWindowProc(HWND__* hWnd, uint uMsg, uint wParam, int lParam)
	{
		TaskbarPlayer taskbarPlayer = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_002EP_0024AAVTaskbarPlayer_0040Util_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E*)Unsafe.AsPointer(ref Microsoft_002EZune_002EUtil_002Esm_gcTaskbarPlayer));
		if (taskbarPlayer != null)
		{
			switch (uMsg)
			{
			default:
				if (uMsg == taskbarPlayer.m_uTaskbarPlayerCommandMsg)
				{
					taskbarPlayer.OnCommandMsg((ETaskbarPlayerCommand)wParam, lParam);
				}
				break;
			case 275u:
				taskbarPlayer.OnTimer(hWnd, wParam);
				break;
			case 2u:
				taskbarPlayer.OnDestory(hWnd);
				break;
			case 1u:
				taskbarPlayer.OnCreate(hWnd);
				break;
			}
		}
		return DefWindowProcW(hWnd, uMsg, wParam, lParam);
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E* P_0, TaskbarPlayer t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static TaskbarPlayer gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_002EP_0024AAVTaskbarPlayer_0040Util_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (TaskbarPlayer)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E_002ERelease(CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E* P_0)
	{
		int num = *(int*)P_0;
		ITrayDeskBand* ptr = (ITrayDeskBand*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CITelemetryManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CITelemetryManager_003E* P_0)
	{
		CComPtrNtv_003CITelemetryManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CITelemetryManager_003E_002ERelease(CComPtrNtv_003CITelemetryManager_003E* P_0)
	{
		int num = *(int*)P_0;
		ITelemetryManager* ptr = (ITelemetryManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static UpdateProxy* Microsoft_002EZune_002EUtil_002EUpdateProxy_002E_007Bctor_007D(UpdateProxy* P_0, UpdateProgressHandler updateProgressHandler)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7UpdateProxy_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 1;
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E*)((byte*)P_0 + 8), updateProgressHandler);
		return P_0;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EUpdateProxy_002EUpdateCheckBegan(UpdateProxy* P_0)
	{
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EUpdateProxy_002EUpdateCheckComplete(UpdateProxy* P_0, [MarshalAs(UnmanagedType.U1)] bool fUpdateFound, [MarshalAs(UnmanagedType.U1)] bool fCriticalUpdateFound)
	{
		UpdateProgressHandler updateProgressHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E_002E_002EP_0024AAVUpdateProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E*)((byte*)P_0 + 8));
		UpdateCheckEventArguments args = new UpdateCheckEventArguments(fUpdateFound, fCriticalUpdateFound, 0);
		updateProgressHandler(args);
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EUpdateProxy_002EUpdateCheckFailed(UpdateProxy* P_0, int hrFailure)
	{
		UpdateProgressHandler updateProgressHandler = gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E_002E_002EP_0024AAVUpdateProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E*)((byte*)P_0 + 8));
		UpdateCheckEventArguments args = new UpdateCheckEventArguments(updateFound: false, criticalUpdateFound: false, hrFailure);
		updateProgressHandler(args);
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EUpdateProxy_002EQueryInterface(UpdateProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 110u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_4ae247ea_52dd_46b7_acd4_7127b1054eef)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EUpdateProxy_002EAddRef(UpdateProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EUpdateProxy_002ERelease(UpdateProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 1);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002EUtil_002EUpdateProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EUpdateProxy_002E_007Bdtor_007D(UpdateProxy* P_0)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E*)((byte*)P_0 + 8));
	}

	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E* P_0, UpdateProgressHandler t)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(t)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	internal unsafe static UpdateProgressHandler gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E_002E_002EP_0024AAVUpdateProgressHandler_0040Util_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProgressHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (UpdateProgressHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIUpdateManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CIUpdateManager_003E* P_0)
	{
		CComPtrNtv_003CIUpdateManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIUpdateManager_003E_002ERelease(CComPtrNtv_003CIUpdateManager_003E* P_0)
	{
		int num = *(int*)P_0;
		IUpdateManager* ptr = (IUpdateManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static int SafeRelease_003Cclass_0020Microsoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProxy_003E(UpdateProxy** pUnk)
	{
		uint num = *(uint*)pUnk;
		int result;
		if (num != 0)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)(int)num + 8)))((IntPtr)(int)num);
			*(int*)pUnk = 0;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	internal unsafe static CUserCredentialProviderProxy* Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002E_007Bctor_007D(CUserCredentialProviderProxy* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7CUserCredentialProviderProxy_0040UserCredential_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 1;
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8));
		return P_0;
	}

	internal unsafe static int Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002EInitialize(CUserCredentialProviderProxy* P_0, UserCredentialHandler userCredentialHandler)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8), userCredentialHandler);
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002EGetCredential(CUserCredentialProviderProxy* P_0, ushort* pwszTargetUrl, ushort* pwszHost, EAuthenticationScheme eAuthScheme, int fIsAuthSchemeSafe, ushort* pwszRealm, ushort* pwszLastUserName, int hrLastAuthError, ushort** pbstrUserName, ushort** pbstrPassword, int* pfSave)
	{
		if (pbstrUserName == null)
		{
			_ZuneShipAssert(1001u, 150u);
			return -2147467261;
		}
		if (pbstrPassword == null)
		{
			_ZuneShipAssert(1001u, 151u);
			return -2147467261;
		}
		if (pfSave == null)
		{
			_ZuneShipAssert(1001u, 152u);
			return -2147467261;
		}
		*(int*)pbstrUserName = 0;
		*(int*)pbstrPassword = 0;
		*pfSave = 0;
		int result = 0;
		string realm = null;
		string lastUserName = null;
		if (pwszRealm != null)
		{
			realm = new string((char*)pwszRealm);
		}
		if (pwszLastUserName != null)
		{
			lastUserName = new string((char*)pwszLastUserName);
		}
		AuthenticationSchemes authScheme;
		if (eAuthScheme != 0)
		{
			if (eAuthScheme != (EAuthenticationScheme)1)
			{
				if (eAuthScheme != (EAuthenticationScheme)2)
				{
					if (eAuthScheme != (EAuthenticationScheme)3)
					{
						result = -2147418113;
						goto IL_0148;
					}
					authScheme = AuthenticationSchemes.Negotiate;
				}
				else
				{
					authScheme = AuthenticationSchemes.Ntlm;
				}
			}
			else
			{
				authScheme = AuthenticationSchemes.Digest;
			}
		}
		else
		{
			authScheme = AuthenticationSchemes.Basic;
		}
		string targetUrl = new string((char*)pwszTargetUrl);
		string host = new string((char*)pwszHost);
		byte isAuthSchemeSafe = (byte)((fIsAuthSchemeSafe != 0) ? 1 : 0);
		UserCredentialRequestArguments userCredentialRequestArguments = new UserCredentialRequestArguments(targetUrl, host, authScheme, isAuthSchemeSafe != 0, realm, lastUserName, hrLastAuthError);
		if (gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_002EP_0024AAVUserCredentialHandler_0040UserCredential_0040Zune_0040Microsoft_0040_0040((gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8))(userCredentialRequestArguments))
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(userCredentialRequestArguments.Credential.UserName)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref PtrToStringChars(userCredentialRequestArguments.Credential.Password)))
					{
						try
						{
							*(int*)pbstrUserName = (int)SysAllocString(ptr);
							*(int*)pbstrPassword = (int)SysAllocString(ptr2);
							*pfSave = (userCredentialRequestArguments.Save ? 1 : 0);
							if (*(int*)pbstrUserName == 0 || *(int*)pbstrPassword == 0)
							{
								result = -2147024882;
							}
						}
						catch
						{
							//try-fault
							ptr2 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		else
		{
			result = -2147467260;
		}
		goto IL_0148;
		IL_0148:
		return result;
	}

	internal unsafe static int Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002EQueryInterface(CUserCredentialProviderProxy* P_0, _GUID* iid, void** ppv)
	{
		if (ppv == null)
		{
			_ZuneShipAssert(1001u, 245u);
			return -2147467261;
		}
		*(int*)ppv = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_00000000_0000_0000_c000_000000000046)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_dafb3bf7_33d7_495c_9855_829248b88ba6)) != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
			*(int*)ppv = (int)P_0;
			return 0;
		}
		return -2147467262;
	}

	internal unsafe static uint Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002EAddRef(CUserCredentialProviderProxy* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002ERelease(CUserCredentialProviderProxy* P_0)
	{
		uint num = (uint)InterlockedDecrement((int*)P_0 + 1);
		if (0 == num && P_0 != null)
		{
			Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return num;
	}

	internal unsafe static void Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002E_007Bdtor_007D(CUserCredentialProviderProxy* P_0)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E*)((byte*)P_0 + 8));
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E* P_0, UserCredentialHandler t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static UserCredentialHandler gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E_002E_002EP_0024AAVUserCredentialHandler_0040UserCredential_0040Zune_0040Microsoft_0040_0040(gcroot_003CMicrosoft_003A_003AZune_003A_003AUserCredential_003A_003AUserCredentialHandler_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (UserCredentialHandler)((GCHandle)intPtr).Target;
	}

	internal unsafe static DynamicArray_003Cint_003E* DynamicArray_003Cint_003E_002E_007Bctor_007D(DynamicArray_003Cint_003E* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7_003F_0024DynamicArray_0040H_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		((int*)P_0)[2] = 0;
		((int*)P_0)[3] = 0;
		return P_0;
	}

	internal unsafe static void DynamicArray_003Cint_003E_002E_007Bdtor_007D(DynamicArray_003Cint_003E* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7_003F_0024DynamicArray_0040H_0040_00406B_0040);
		if (((int*)P_0)[3] > 0)
		{
			delete_005B_005D((void*)(int)((uint*)P_0)[1]);
		}
		((int*)P_0)[1] = 0;
		((int*)P_0)[2] = 0;
		((int*)P_0)[3] = 0;
	}

	internal unsafe static int* DynamicArray_003Cint_003E_002E_005B_005D(DynamicArray_003Cint_003E* P_0, int iIndx)
	{
		uint* ptr = null;
		if (iIndx < 0 || (iIndx >= ((int*)P_0)[2] && iIndx >= ((int*)P_0)[3]))
		{
			*ptr = 0u;
		}
		if (iIndx >= ((int*)P_0)[2])
		{
			((int*)P_0)[2] = iIndx + 1;
		}
		return (int*)(iIndx * 4 + ((int*)P_0)[1]);
	}

	internal unsafe static void* DynamicArray_003Cint_003E_002E__vecDelDtor(DynamicArray_003Cint_003E* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			DynamicArray_003Cint_003E* ptr = (DynamicArray_003Cint_003E*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 16u, *(int*)ptr, (delegate*<void*, void>)(delegate*<DynamicArray_003Cint_003E*, void>)(&DynamicArray_003Cint_003E_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		DynamicArray_003Cint_003E_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static void InitIsLonghornOrBetter()
	{
		bool flag = false;
		Unsafe.SkipInit(out _OSVERSIONINFOW oSVERSIONINFOW);
		*(int*)(&oSVERSIONINFOW) = 276;
		if (1 == GetVersionExW(&oSVERSIONINFOW))
		{
			int num = ((Unsafe.As<_OSVERSIONINFOW, int>(ref Unsafe.AddByteOffset(ref oSVERSIONINFOW, 16)) == 2 && (uint)Unsafe.As<_OSVERSIONINFOW, int>(ref Unsafe.AddByteOffset(ref oSVERSIONINFOW, 4)) >= 6u) ? 1 : 0);
			flag = (byte)num != 0;
		}
		_003FA0x7bb9ee51_002Es_bIsLonghornOrBetter = flag;
		_003FA0x7bb9ee51_002Es_bIsLonghornOrBetterInitialized = true;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool IsLonghornOrBetter()
	{
		if (!_003FA0x7bb9ee51_002Es_bIsLonghornOrBetterInitialized)
		{
			InitIsLonghornOrBetter();
		}
		return _003FA0x7bb9ee51_002Es_bIsLonghornOrBetter;
	}

	internal unsafe static string GetErrorDescription(int hr)
	{
		void* ptr = null;
		if (FormatMessageW(4864u, null, (uint)(hr & 0xFFFF), 0u, (ushort*)(&ptr), 0u, null) == 0)
		{
			return $"Unknown Error: 0x{hr:x}";
		}
		string arg = new string((char*)ptr);
		string result = $"{arg} Error: 0x{hr:x}";
		if (ptr != null)
		{
			LocalFree(ptr);
		}
		return result;
	}

	internal unsafe static _GUID GuidToGUID(Guid guid)
	{
		fixed (byte* ptr = &guid.ToByteArray()[0])
		{
			Unsafe.SkipInit(out _GUID result);
			// IL cpblk instruction
			Unsafe.CopyBlock(ref result, ptr, 16);
			return result;
		}
	}

	internal unsafe static Guid GUIDToGuid(_GUID guid)
	{
		return new Guid(*(uint*)(&guid), Unsafe.As<_GUID, ushort>(ref Unsafe.AddByteOffset(ref guid, 4)), Unsafe.As<_GUID, ushort>(ref Unsafe.AddByteOffset(ref guid, 6)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 8)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 9)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 10)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 11)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 12)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 13)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 14)), Unsafe.As<_GUID, byte>(ref Unsafe.AddByteOffset(ref guid, 15)));
	}

	internal unsafe static string[] BstrArrayToStringArray(DynamicArray_003Cunsigned_0020short_0020_002A_003E* bstrArray)
	{
		string[] array = null;
		int num = ((int*)bstrArray)[2];
		try
		{
			array = new string[num];
			for (int i = 0; i < num; i++)
			{
				ushort** ptr = DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_005B_005D(bstrArray, i);
				array[i] = new string((char*)(int)(*(uint*)ptr));
			}
			return array;
		}
		finally
		{
			int num2 = 0;
			if (0 < num)
			{
				do
				{
					if (*(int*)DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_005B_005D(bstrArray, num2) != 0)
					{
						SysFreeString((ushort*)(int)(*(uint*)DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_005B_005D(bstrArray, num2)));
					}
					num2++;
				}
				while (num2 < num);
			}
		}
	}

	internal unsafe static _SYSTEMTIME DateTimeToSystemTime(DateTime dateTime)
	{
		Unsafe.SkipInit(out _SYSTEMTIME result);
		*(short*)(&result) = 0;
		// IL initblk instruction
		Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref result, 2), 0, 14);
		_FILETIME fILETIME = DateTimeToFileTime(dateTime);
		FileTimeToSystemTime(&fILETIME, &result);
		return result;
	}

	internal unsafe static _FILETIME DateTimeToFileTime(DateTime dateTime)
	{
		Unsafe.SkipInit(out _FILETIME result);
		*(int*)(&result) = 0;
		// IL initblk instruction
		Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref result, 4), 0, 4);
		try
		{
			Unsafe.WriteUnaligned(ref *(byte*)(&result), dateTime.ToFileTimeUtc());
		}
		catch (Exception)
		{
		}
		return result;
	}

	internal unsafe static DateTime SystemTimeToDateTime(_SYSTEMTIME stValue)
	{
		Unsafe.SkipInit(out _FILETIME ftValue);
		*(int*)(&ftValue) = 0;
		// IL initblk instruction
		Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref ftValue, 4), 0, 4);
		SystemTimeToFileTime(&stValue, &ftValue);
		return FileTimeToDateTime(ftValue);
	}

	internal static DateTime FileTimeToDateTime(_FILETIME ftValue)
	{
		Unsafe.SkipInit(out long fileTime);
		// IL cpblk instruction
		Unsafe.CopyBlock(ref fileTime, ref ftValue, 8);
		DateTime result = DateTime.MinValue;
		try
		{
			result = DateTime.FromFileTimeUtc(fileTime);
		}
		catch (Exception)
		{
		}
		return result;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002E_003FA0x7bb9ee51_002ESendInstantMessengerMessage(tagCOPYDATASTRUCT* pcds)
	{
		HWND__* ptr = FindWindowExW(null, null, (ushort*)Unsafe.AsPointer(ref _003F_003F_C_0040_1CC_0040NMGFJFON_0040_003F_0024AAM_003F_0024AAs_003F_0024AAn_003F_0024AAM_003F_0024AAs_003F_0024AAg_003F_0024AAr_003F_0024AAU_003F_0024AAI_003F_0024AAM_003F_0024AAa_003F_0024AAn_003F_0024AAa_003F_0024AAg_003F_0024AAe_003F_0024AAr_003F_0024AA_003F_0024AA_0040), (ushort*)Unsafe.AsPointer(ref _003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040));
		if (ptr == null)
		{
			return;
		}
		Unsafe.SkipInit(out uint num);
		while (SendMessageTimeoutW(ptr, 74u, 0u, (int)pcds, 2u, 300u, &num) != 0)
		{
			ptr = FindWindowExW(null, ptr, (ushort*)Unsafe.AsPointer(ref _003F_003F_C_0040_1CC_0040NMGFJFON_0040_003F_0024AAM_003F_0024AAs_003F_0024AAn_003F_0024AAM_003F_0024AAs_003F_0024AAg_003F_0024AAr_003F_0024AAU_003F_0024AAI_003F_0024AAM_003F_0024AAa_003F_0024AAn_003F_0024AAa_003F_0024AAg_003F_0024AAe_003F_0024AAr_003F_0024AA_003F_0024AA_0040), (ushort*)Unsafe.AsPointer(ref _003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040));
			if (ptr == null)
			{
				return;
			}
		}
		uint lastError = GetLastError();
		_ZuneShipAssert(1004u, 284u);
	}

	internal static string Microsoft_002EZune_002EUtil_002EPrepareStringForBroadcast(string str)
	{
		if (null != str)
		{
			str = str.Replace("\\0", "");
			str = str.Replace("{", "[");
			return str.Replace("}", "]");
		}
		return str;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002E_003FA0x7bb9ee51_002ENotifyInstantMessenger(string AlbumName, string ArtistName, string TrackTitle, int TrackNumber, Guid ZuneMediaId)
	{
		string text = string.Format("ZUNE\\0Music\\01\\0{{0}} - {{1}}\\0{0}\\0{1}\\0{2}\\0zune:{{{3}}}\\0", new object[4]
		{
			Microsoft_002EZune_002EUtil_002EPrepareStringForBroadcast(TrackTitle),
			Microsoft_002EZune_002EUtil_002EPrepareStringForBroadcast(ArtistName),
			Microsoft_002EZune_002EUtil_002EPrepareStringForBroadcast(AlbumName),
			ZuneMediaId
		});
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(text)))
		{
			Unsafe.SkipInit(out tagCOPYDATASTRUCT tagCOPYDATASTRUCT2);
			Unsafe.As<tagCOPYDATASTRUCT, int>(ref Unsafe.AddByteOffset(ref tagCOPYDATASTRUCT2, 4)) = text.Length * 2 + 2;
			*(int*)(&tagCOPYDATASTRUCT2) = 1351;
			Unsafe.As<tagCOPYDATASTRUCT, int>(ref Unsafe.AddByteOffset(ref tagCOPYDATASTRUCT2, 8)) = (int)ptr;
			Microsoft_002EZune_002EUtil_002E_003FA0x7bb9ee51_002ESendInstantMessengerMessage(&tagCOPYDATASTRUCT2);
		}
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002E_003FA0x7bb9ee51_002EResetInstantMessenger()
	{
		string text = "ZUNE\\0Music\\00\\0{0}\\0";
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref PtrToStringChars(text)))
		{
			Unsafe.SkipInit(out tagCOPYDATASTRUCT tagCOPYDATASTRUCT2);
			Unsafe.As<tagCOPYDATASTRUCT, int>(ref Unsafe.AddByteOffset(ref tagCOPYDATASTRUCT2, 4)) = text.Length * 2 + 2;
			*(int*)(&tagCOPYDATASTRUCT2) = 1351;
			Unsafe.As<tagCOPYDATASTRUCT, int>(ref Unsafe.AddByteOffset(ref tagCOPYDATASTRUCT2, 8)) = (int)ptr;
			Microsoft_002EZune_002EUtil_002E_003FA0x7bb9ee51_002ESendInstantMessengerMessage(&tagCOPYDATASTRUCT2);
		}
	}

	internal unsafe static void CComPtrNtv_003CIWinLiveInformation_003E_002E_007Bdtor_007D(CComPtrNtv_003CIWinLiveInformation_003E* P_0)
	{
		CComPtrNtv_003CIWinLiveInformation_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIWinLiveAvailableInformation_003E_002E_007Bdtor_007D(CComPtrNtv_003CIWinLiveAvailableInformation_003E* P_0)
	{
		CComPtrNtv_003CIWinLiveAvailableInformation_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIWinLiveSignup_003E_002E_007Bdtor_007D(CComPtrNtv_003CIWinLiveSignup_003E* P_0)
	{
		CComPtrNtv_003CIWinLiveSignup_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIWinLiveInformation_003E_002ERelease(CComPtrNtv_003CIWinLiveInformation_003E* P_0)
	{
		int num = *(int*)P_0;
		IWinLiveInformation* ptr = (IWinLiveInformation*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIWinLiveAvailableInformation_003E_002ERelease(CComPtrNtv_003CIWinLiveAvailableInformation_003E* P_0)
	{
		int num = *(int*)P_0;
		IWinLiveAvailableInformation* ptr = (IWinLiveAvailableInformation*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIWinLiveSignup_003E_002ERelease(CComPtrNtv_003CIWinLiveSignup_003E* P_0)
	{
		int num = *(int*)P_0;
		IWinLiveSignup* ptr = (IWinLiveSignup*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static int IUnknown_002EQueryInterface_003Cstruct_0020IWinLiveSignup_003E(IUnknown* P_0, IWinLiveSignup** pp)
	{
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)P_0)))((nint)P_0, (_GUID*)Unsafe.AsPointer(ref _GUID_3e87c005_24d9_4446_a1b9_2c062230f2c5), (void**)pp);
	}

	internal unsafe static void CComPtrNtv_003CIJumpListEntry_003E_002E_007Bdtor_007D(CComPtrNtv_003CIJumpListEntry_003E* P_0)
	{
		CComPtrNtv_003CIJumpListEntry_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIJumpListCategory_003E_002E_007Bdtor_007D(CComPtrNtv_003CIJumpListCategory_003E* P_0)
	{
		CComPtrNtv_003CIJumpListCategory_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIJumpListEntry_003E_002ERelease(CComPtrNtv_003CIJumpListEntry_003E* P_0)
	{
		int num = *(int*)P_0;
		IJumpListEntry* ptr = (IJumpListEntry*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIJumpListCategory_003E_002ERelease(CComPtrNtv_003CIJumpListCategory_003E* P_0)
	{
		int num = *(int*)P_0;
		IJumpListCategory* ptr = (IJumpListCategory*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static Win7ShellManagerMediator* Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002E_007Bctor_007D(Win7ShellManagerMediator* P_0, Win7ShellManager manager)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7Win7ShellManagerMediator_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		((int*)P_0)[1] = 0;
		Win7ShellManagerMediator* ptr = (Win7ShellManagerMediator*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E*)ptr);
		try
		{
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E*)ptr, manager);
			return P_0;
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002EWindowPositionKeypressDetected(Win7ShellManagerMediator* P_0, EWindowPositionKeys eKey)
	{
		return gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E*)((byte*)P_0 + 8)).WindowPositionKeyPressDetected((WindowPositionKeys)eKey);
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002EThumbBarButtonPressed(Win7ShellManagerMediator* P_0, uint iUniqueID)
	{
		return gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E*)((byte*)P_0 + 8)).ThumbBarButtonPressed(iUniqueID);
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002EMonitorChanged(Win7ShellManagerMediator* P_0)
	{
		return gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E*)((byte*)P_0 + 8)).MonitorChanged();
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002EQueryInterface(Win7ShellManagerMediator* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_3a6ee87c_36d0_4535_a664_6cc41f5029e6)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002EAddRef(Win7ShellManagerMediator* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002ERelease(Win7ShellManagerMediator* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002E_007Bdtor_007D(P_0);
			delete(P_0);
		}
		return (uint)num;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002E_007Bdtor_007D(Win7ShellManagerMediator* P_0)
	{
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E*)((byte*)P_0 + 8));
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E* P_0, Win7ShellManager t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static Win7ShellManager gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AWin7ShellManager_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (Win7ShellManager)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIWin7ShellManager_003E_002E_007Bdtor_007D(CComPtrNtv_003CIWin7ShellManager_003E* P_0)
	{
		CComPtrNtv_003CIWin7ShellManager_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIWin7ShellManager_003E_002ERelease(CComPtrNtv_003CIWin7ShellManager_003E* P_0)
	{
		int num = *(int*)P_0;
		IWin7ShellManager* ptr = (IWin7ShellManager*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIThumbBarButton_003E_002E_007Bdtor_007D(CComPtrNtv_003CIThumbBarButton_003E* P_0)
	{
		CComPtrNtv_003CIThumbBarButton_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIThumbBarButton_003E_002ERelease(CComPtrNtv_003CIThumbBarButton_003E* P_0)
	{
		int num = *(int*)P_0;
		IThumbBarButton* ptr = (IThumbBarButton*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void WPP_INIT_CONTROL_ARRAY(WPP_PROJECT_CONTROL_BLOCK* Arr)
	{
		((int*)Arr)[4] = 0;
		*(int*)Arr = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref WPP_MAIN_CB, 32));
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
		Arr = (WPP_PROJECT_CONTROL_BLOCK*)((byte*)Arr + 32);
		((int*)Arr)[4] = 0;
		*(int*)Arr = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref WPP_MAIN_CB, 64));
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
		Arr = (WPP_PROJECT_CONTROL_BLOCK*)((byte*)Arr + 32);
		((int*)Arr)[4] = 0;
		*(int*)Arr = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref WPP_MAIN_CB, 96));
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
		Arr = (WPP_PROJECT_CONTROL_BLOCK*)((byte*)Arr + 32);
		((int*)Arr)[4] = 0;
		*(int*)Arr = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref WPP_MAIN_CB, 128));
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
		Arr = (WPP_PROJECT_CONTROL_BLOCK*)((byte*)Arr + 32);
		((int*)Arr)[4] = 0;
		*(int*)Arr = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref WPP_MAIN_CB, 160));
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
		Arr = (WPP_PROJECT_CONTROL_BLOCK*)((byte*)Arr + 32);
		((int*)Arr)[4] = 0;
		*(int*)Arr = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref WPP_MAIN_CB, 192));
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
		Arr = (WPP_PROJECT_CONTROL_BLOCK*)((byte*)Arr + 32);
		((int*)Arr)[4] = 0;
		*(int*)Arr = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref WPP_MAIN_CB, 224));
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
		Arr = (WPP_PROJECT_CONTROL_BLOCK*)((byte*)Arr + 32);
		((int*)Arr)[4] = 0;
		*(int*)Arr = 0;
		((sbyte*)Arr)[24] = 1;
		((sbyte*)Arr)[25] = 8;
		((short*)Arr)[13] = 0;
		((int*)Arr)[7] = 0;
	}

	internal unsafe static void WPP_INIT_GUID_ARRAY(_GUID** Arr)
	{
		*(int*)Arr = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_CtlGuidZune);
		Arr = (_GUID**)((byte*)Arr + 4);
		*(int*)Arr = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_CtlGuidZuneSync);
		Arr = (_GUID**)((byte*)Arr + 4);
		*(int*)Arr = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_CtlGuidZuneLibrary);
		Arr = (_GUID**)((byte*)Arr + 4);
		*(int*)Arr = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_CtlGuidZunePlayback);
		Arr = (_GUID**)((byte*)Arr + 4);
		*(int*)Arr = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_CtlGuidZuneService);
		Arr = (_GUID**)((byte*)Arr + 4);
		*(int*)Arr = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_CtlGuidZuneWindowsMobile);
		Arr = (_GUID**)((byte*)Arr + 4);
		*(int*)Arr = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_CtlGuidZuneWmdu);
		((int*)Arr)[1] = (int)Unsafe.AsPointer(ref WPP_ThisDir_CTLGUID_SqmClientTracingGuid);
	}

	internal unsafe static uint WppControlCallback(WMIDPREQUESTCODE RequestCode, void* Context, uint* InOutBufferSize, void* Buffer)
	{
		_WPP_TRACE_CONTROL_BLOCK* ptr = (_WPP_TRACE_CONTROL_BLOCK*)Context;
		*InOutBufferSize = 0u;
		ulong num;
		uint num2;
		byte b;
		switch (RequestCode)
		{
		default:
			return 87u;
		case (WMIDPREQUESTCODE)5:
			num = 0uL;
			num2 = 0u;
			b = 0;
			break;
		case (WMIDPREQUESTCODE)4:
			num = GetTraceLoggerHandle(Buffer);
			b = GetTraceEnableLevel(num);
			num2 = GetTraceEnableFlags(num);
			break;
		}
		ushort num3 = ((ushort*)Context)[13];
		if ((ushort)(num3 & 1) != 0 && ((int*)Context)[4] != 0)
		{
			*(ulong*)(int)((uint*)Context)[4] = num;
			*(int*)(((int*)Context)[4] + 12) = b;
			*(uint*)(((int*)Context)[4] + 8) = num2;
		}
		else
		{
			if ((ushort)(num3 & 2) != 0)
			{
				uint num4 = ((uint*)Context)[4];
				if (num4 != 0)
				{
					ptr = (_WPP_TRACE_CONTROL_BLOCK*)(int)num4;
				}
			}
			((long*)ptr)[2] = (long)num;
			((sbyte*)ptr)[25] = (sbyte)b;
			((int*)ptr)[7] = (int)num2;
		}
		return 0u;
	}

	internal unsafe static void WppInitUm(ushort* AppName)
	{
		_WPP_TRACE_CONTROL_BLOCK* ptr = (_WPP_TRACE_CONTROL_BLOCK*)WPP_GLOBAL_Control;
		_GUID** ptr2 = (_GUID**)Unsafe.AsPointer(ref WPP_REGISTRATION_GUIDS);
		if (ptr != null)
		{
			int num = (int)__unep_0040_003FWppControlCallback_0040_0040_0024_0024J216YGKW4WMIDPREQUESTCODE_0040_0040PAXPAK1_0040Z;
			Unsafe.SkipInit(out _TRACE_GUID_REGISTRATION tRACE_GUID_REGISTRATION);
			do
			{
				_GUID* ptr3 = (_GUID*)(int)(*(uint*)ptr2);
				ptr2 = (_GUID**)((byte*)ptr2 + 4);
				*(int*)(&tRACE_GUID_REGISTRATION) = (int)ptr3;
				Unsafe.As<_TRACE_GUID_REGISTRATION, int>(ref Unsafe.AddByteOffset(ref tRACE_GUID_REGISTRATION, 4)) = 0;
				RegisterTraceGuidsW((delegate* unmanaged[Stdcall, Stdcall]<WMIDPREQUESTCODE, void*, uint*, void*, uint>)num, ptr, ptr3, 1u, &tRACE_GUID_REGISTRATION, null, null, (ulong*)ptr + 1);
				ptr = (_WPP_TRACE_CONTROL_BLOCK*)(int)(*(uint*)ptr);
			}
			while (ptr != null);
		}
	}

	internal unsafe static void WppCleanupUm()
	{
		if (WPP_GLOBAL_Control == Unsafe.AsPointer(ref WPP_GLOBAL_Control))
		{
			return;
		}
		_WPP_TRACE_CONTROL_BLOCK* ptr = (_WPP_TRACE_CONTROL_BLOCK*)WPP_GLOBAL_Control;
		if (ptr != null)
		{
			do
			{
				ulong num = ((ulong*)ptr)[1];
				if (num != 0)
				{
					UnregisterTraceGuids(num);
					((long*)ptr)[1] = 0L;
				}
				ptr = (_WPP_TRACE_CONTROL_BLOCK*)(int)(*(uint*)ptr);
			}
			while (ptr != null);
		}
		WPP_GLOBAL_Control = (WPP_PROJECT_CONTROL_BLOCK*)Unsafe.AsPointer(ref WPP_GLOBAL_Control);
	}

	internal unsafe static void* DBPropertyRequestStruct_002E__vecDelDtor(DBPropertyRequestStruct* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			DBPropertyRequestStruct* ptr = (DBPropertyRequestStruct*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 24u, *(int*)ptr, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&DBPropertyRequestStruct_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		DBPropertyRequestStruct_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static void DBPropertyRequestStruct_002E__dflt_ctor_closure(DBPropertyRequestStruct* P_0)
	{
		DBPropertyRequestStruct_002E_007Bctor_007D(P_0, uint.MaxValue);
	}

	internal unsafe static void* DBPropertySubmitStruct_002E__vecDelDtor(DBPropertySubmitStruct* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			DBPropertySubmitStruct* ptr = (DBPropertySubmitStruct*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 8u, *(int*)ptr, (delegate*<void*, void>)(delegate*<DBPropertySubmitStruct*, void>)(&DBPropertySubmitStruct_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static void DBPropertySubmitStruct_002E__dflt_ctor_closure(DBPropertySubmitStruct* P_0)
	{
		*(int*)P_0 = -1;
		((int*)P_0)[1] = 0;
	}

	internal unsafe static void* CComPropVariant_002E__vecDelDtor(CComPropVariant* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			CComPropVariant* ptr = (CComPropVariant*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 16u, *(int*)ptr, (delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&CComPropVariant_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		CComPropVariant_002EClear(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static CComPtrNtv_003CIQueryPropertyBag_003E* CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bctor_007D(CComPtrNtv_003CIQueryPropertyBag_003E* P_0, IQueryPropertyBag* lp)
	{
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return P_0;
	}

	internal unsafe static void CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D(CComPtrNtv_003CIQueryPropertyBag_003E* P_0)
	{
		CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(P_0);
	}

	internal unsafe static IQueryPropertyBag* CComPtrNtv_003CIQueryPropertyBag_003E_002E_003D(CComPtrNtv_003CIQueryPropertyBag_003E* P_0, IQueryPropertyBag* lp)
	{
		CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(P_0);
		*(int*)P_0 = (int)lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return (IQueryPropertyBag*)(int)(*(uint*)P_0);
	}

	internal unsafe static void CComPtrNtv_003CIMSMediaSchemaPropertySet_003E_002E_007Bdtor_007D(CComPtrNtv_003CIMSMediaSchemaPropertySet_003E* P_0)
	{
		CComPtrNtv_003CIMSMediaSchemaPropertySet_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(CComPtrNtv_003CIQueryPropertyBag_003E* P_0)
	{
		int num = *(int*)P_0;
		IQueryPropertyBag* ptr = (IQueryPropertyBag*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static void CComPtrNtv_003CIMSMediaSchemaPropertySet_003E_002ERelease(CComPtrNtv_003CIMSMediaSchemaPropertySet_003E* P_0)
	{
		int num = *(int*)P_0;
		IMSMediaSchemaPropertySet* ptr = (IMSMediaSchemaPropertySet*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	internal unsafe static ResultSetEventRelay* MicrosoftZuneLibrary_002EResultSetEventRelay_002E_007Bctor_007D(ResultSetEventRelay* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7ResultSetEventRelay_0040MicrosoftZuneLibrary_0040_00406B_0040);
		gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)((byte*)P_0 + 4));
		return P_0;
	}

	internal unsafe static void MicrosoftZuneLibrary_002EResultSetEventRelay_002E_007Bdtor_007D(ResultSetEventRelay* P_0)
	{
		gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)((byte*)P_0 + 4));
	}

	internal unsafe static void MicrosoftZuneLibrary_002EResultSetEventRelay_002EAdvise(ResultSetEventRelay* P_0, IQueryListEvents pQueryListEvents, IDatabaseQueryResults* pResults)
	{
		if (pResults == null || pQueryListEvents == null)
		{
			return;
		}
		ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
		if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr) == null)
		{
			InitializeCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr, new List<IQueryListEvents>());
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IResultSetEvents*, int>)(int)(*(uint*)(*(int*)pResults + 52)))((nint)pResults, (IResultSetEvents*)P_0) < 0)
			{
				return;
			}
		}
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Add(pQueryListEvents);
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
	}

	internal unsafe static void MicrosoftZuneLibrary_002EResultSetEventRelay_002EUnAdvise(ResultSetEventRelay* P_0, IQueryListEvents pQueryListEvents, IDatabaseQueryResults* pResults)
	{
		if (pResults == null || pQueryListEvents == null)
		{
			return;
		}
		ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
		if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count == 1)
		{
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IResultSetEvents*, int>)(int)(*(uint*)(*(int*)pResults + 56)))((nint)pResults, (IResultSetEvents*)P_0) >= 0)
			{
				try
				{
					EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Remove(pQueryListEvents);
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr, null);
					return;
				}
				finally
				{
					ResultSetEventRelay* ptr2 = (ResultSetEventRelay*)((byte*)P_0 + 8);
					LeaveCriticalSection((_RTL_CRITICAL_SECTION*)ptr2);
					DeleteCriticalSection((_RTL_CRITICAL_SECTION*)ptr2);
				}
			}
			return;
		}
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Remove(pQueryListEvents);
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002EBeginBulkEvents(ResultSetEventRelay* P_0)
	{
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
			if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr) != null && gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count > 0)
			{
				for (int i = 0; i < gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count; i++)
				{
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr)[i].ListBeginBulkEvents();
				}
			}
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002EEndBulkEvents(ResultSetEventRelay* P_0)
	{
		int result = 0;
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
			if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr) != null && gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count > 0)
			{
				for (int i = 0; i < gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count; i++)
				{
					result = -2147483638;
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr)[i].ListEndBulkEvents();
				}
			}
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
		return result;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002EInsert(ResultSetEventRelay* P_0, uint dwIndex)
	{
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
			if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr) != null && gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count > 0)
			{
				for (int i = 0; i < gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count; i++)
				{
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr)[i].ListInsert((int)dwIndex);
				}
			}
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002ERemove(ResultSetEventRelay* P_0, uint dwIndex)
	{
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
			if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr) != null && gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count > 0)
			{
				for (int i = 0; i < gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count; i++)
				{
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr)[i].ListRemoveAt((int)dwIndex);
				}
			}
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002EMove(ResultSetEventRelay* P_0, uint dwFrom, uint dwTo)
	{
		return -2147467263;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002EChange(ResultSetEventRelay* P_0, uint dwItem)
	{
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
			if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr) != null && gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count > 0)
			{
				for (int i = 0; i < gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count; i++)
				{
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr)[i].ListModified((int)dwItem);
				}
			}
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
		return 0;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002EClear(ResultSetEventRelay* P_0)
	{
		return -2147467263;
	}

	internal unsafe static int MicrosoftZuneLibrary_002EResultSetEventRelay_002ENotifyCount(ResultSetEventRelay* P_0, uint Count)
	{
		try
		{
			EnterCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
			ResultSetEventRelay* ptr = (ResultSetEventRelay*)((byte*)P_0 + 4);
			if (gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr) != null && gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count > 0)
			{
				for (int i = 0; i < gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr).Count; i++)
				{
					gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040((gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E*)ptr)[i].ListNotifyCount(Count);
				}
			}
		}
		finally
		{
			LeaveCriticalSection((_RTL_CRITICAL_SECTION*)((byte*)P_0 + 8));
		}
		return 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E* gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_007Bctor_007D(gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E* gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_003D(gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E* P_0, List<IQueryListEvents> t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static List<IQueryListEvents> gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002EP_0024AAV_003F_0024List_0040P_0024AAUIQueryListEvents_0040MicrosoftZuneLibrary_0040_0040_0040Generic_0040Collections_0040System_0040_0040(gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (List<IQueryListEvents>)((GCHandle)intPtr).Target;
	}

	internal unsafe static List<IQueryListEvents> gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E_002E_002D_003E(gcroot_003CSystem_003A_003ACollections_003A_003AGeneric_003A_003AList_003CMicrosoftZuneLibrary_003A_003AIQueryListEvents_0020_005E_003E_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (List<IQueryListEvents>)((GCHandle)intPtr).Target;
	}

	internal unsafe static void WPP_SF_sD(ulong Logger, ushort id, _GUID* TraceGuid, sbyte* _a1, uint _a2)
	{
		uint num;
		if (_a1 != null)
		{
			sbyte* ptr = _a1;
			if (*_a1 != 0)
			{
				do
				{
					ptr++;
				}
				while (*ptr != 0);
			}
			num = (uint)(ptr - (nuint)_a1 + 1);
		}
		else
		{
			num = 5u;
		}
		sbyte* ptr2 = (sbyte*)((_a1 == null) ? Unsafe.AsPointer(ref _003F_003F_C_0040_04HIBGFPH_0040NULL_003F_0024AA_0040) : _a1);
		TraceMessage(Logger, 43u, TraceGuid, id, __arglist(ptr2, num, &_a2, 4u, 0));
	}

	internal unsafe static ZuneWebHostEventSink* Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002E_007Bctor_007D(ZuneWebHostEventSink* P_0, ZuneWebHost owner, IZuneWebHost* nativeZuneWebHost)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7ZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		ZuneWebHostEventSink* ptr = (ZuneWebHostEventSink*)((byte*)P_0 + 8);
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_007Bctor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*)ptr);
		try
		{
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[25] >= 5u)
			{
				WPP_SF_(((ulong*)WPP_GLOBAL_Control)[2], 22, (_GUID*)Unsafe.AsPointer(ref _003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
			}
			((int*)P_0)[1] = 0;
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*)ptr, owner);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IZuneWebHostEventSink*, int>)(int)(*(uint*)(*(int*)nativeZuneWebHost + 16)))((nint)nativeZuneWebHost, (IZuneWebHostEventSink*)P_0);
			if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[25] >= 5u)
			{
				WPP_SF_(((ulong*)WPP_GLOBAL_Control)[2], 23, (_GUID*)Unsafe.AsPointer(ref _003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
			}
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		return P_0;
	}

	internal unsafe static void* Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002E__vecDelDtor(ZuneWebHostEventSink* P_0, uint P_1)
	{
		if ((P_1 & 2) != 0)
		{
			ZuneWebHostEventSink* ptr = (ZuneWebHostEventSink*)((byte*)P_0 - 4);
			__ehvec_dtor(P_0, 12u, *(int*)ptr, (delegate*<void*, void>)(delegate*<ZuneWebHostEventSink*, void>)(&Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002E_007Bdtor_007D));
			if ((P_1 & 1) != 0)
			{
				delete_005B_005D(ptr);
			}
			return ptr;
		}
		Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002E_007Bdtor_007D(P_0);
		if ((P_1 & 1) != 0)
		{
			delete(P_0);
		}
		return P_0;
	}

	internal unsafe static void Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002E_007Bdtor_007D(ZuneWebHostEventSink* P_0)
	{
		*(int*)P_0 = (int)Unsafe.AsPointer(ref _003F_003F_7ZuneWebHostEventSink_0040Util_0040Zune_0040Microsoft_0040_00406B_0040);
		ZuneWebHostEventSink* ptr;
		try
		{
			ptr = (ZuneWebHostEventSink*)((byte*)P_0 + 8);
			gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*)ptr, null);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*, void>)(&gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_007Bdtor_007D), (byte*)P_0 + 8);
			throw;
		}
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*)ptr);
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002EOnNavigateComplete(ZuneWebHostEventSink* P_0, WString* wsDestinationUrl)
	{
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[25] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[2], 24, (_GUID*)Unsafe.AsPointer(ref _003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		string data = new string((char*)(int)(*(uint*)wsDestinationUrl));
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*)((byte*)P_0 + 8)).OnNavigationComplete(data);
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[25] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[2], 25, (_GUID*)Unsafe.AsPointer(ref _003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002EOnNavigateError(ZuneWebHostEventSink* P_0, WString* wsErrorUrl, uint dwErrorCode)
	{
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[25] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[2], 26, (_GUID*)Unsafe.AsPointer(ref _003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		string navUrl = new string((char*)(int)(*(uint*)wsErrorUrl));
		gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_002D_003E((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E*)((byte*)P_0 + 8)).OnNavigationError(navUrl, (int)dwErrorCode);
		if (WPP_GLOBAL_Control != Unsafe.AsPointer(ref WPP_GLOBAL_Control) && (((int*)WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)WPP_GLOBAL_Control)[25] >= 5u)
		{
			WPP_SF_(((ulong*)WPP_GLOBAL_Control)[2], 27, (_GUID*)Unsafe.AsPointer(ref _003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		return 0;
	}

	internal unsafe static int Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002EQueryInterface(ZuneWebHostEventSink* P_0, _GUID* iid, void** ppInterface)
	{
		int result = 0;
		if (IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref IID_IUnknown)) == 0 && IsEqualGUID(iid, (_GUID*)Unsafe.AsPointer(ref _GUID_2da5365a_229c_4dc9_a33e_47960794e4f9)) == 0)
		{
			*(int*)ppInterface = 0;
			result = -2147467262;
		}
		else
		{
			*(int*)ppInterface = (int)P_0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)P_0 + 4)))((nint)P_0);
		}
		return result;
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002EAddRef(ZuneWebHostEventSink* P_0)
	{
		return (uint)InterlockedIncrement((int*)P_0 + 1);
	}

	internal unsafe static uint Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002ERelease(ZuneWebHostEventSink* P_0)
	{
		int num = InterlockedDecrement((int*)P_0 + 1);
		if (num == 0 && P_0 != null)
		{
			((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, uint, void*>)(int)(*(uint*)(*(int*)P_0 + 20)))((nint)P_0, 1u);
		}
		return (uint)num;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_007Bctor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E* gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_003D(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E* P_0, ZuneWebHost t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static ZuneWebHost gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E_002E_002D_003E(gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AZuneWebHost_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (ZuneWebHost)((GCHandle)intPtr).Target;
	}

	internal unsafe static void CComPtrNtv_003CIZuneWebHost_003E_002E_007Bdtor_007D(CComPtrNtv_003CIZuneWebHost_003E* P_0)
	{
		CComPtrNtv_003CIZuneWebHost_003E_002ERelease(P_0);
	}

	internal unsafe static void CComPtrNtv_003CIZuneWebHost_003E_002ERelease(CComPtrNtv_003CIZuneWebHost_003E* P_0)
	{
		int num = *(int*)P_0;
		IZuneWebHost* ptr = (IZuneWebHost*)num;
		if (num != 0)
		{
			*(int*)P_0 = 0;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool _003CCrtImplementationDetails_003E_002ENativeDll_002EIsSafeForManagedCode()
	{
		if (!(__native_dllmain_reason != uint.MaxValue))
		{
			return true;
		}
		if (__native_vcclrit_reason != uint.MaxValue)
		{
			return true;
		}
		int num = ((__native_dllmain_reason != 1 && __native_dllmain_reason != 0) ? 1 : 0);
		return (byte)num != 0;
	}

	internal unsafe static int _003CCrtImplementationDetails_003E_002EDefaultDomain_002EDoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool _003CCrtImplementationDetails_003E_002EDefaultDomain_002EHasPerProcess()
	{
		if (_003FhasPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A == (TriBool.State)2)
		{
			void** ptr = (void**)Unsafe.AsPointer(ref _003FA0x250abb45_002E__xc_mp_a);
			if (Unsafe.IsAddressLessThan(ref _003FA0x250abb45_002E__xc_mp_a, ref _003FA0x250abb45_002E__xc_mp_z))
			{
				do
				{
					if (*(int*)ptr == 0)
					{
						ptr = (void**)((byte*)ptr + 4);
						continue;
					}
					_003FhasPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A = (TriBool.State)(-1);
					return true;
				}
				while (ptr < Unsafe.AsPointer(ref _003FA0x250abb45_002E__xc_mp_z));
			}
			_003FhasPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A = (TriBool.State)0;
			return false;
		}
		return _003FhasPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A == (TriBool.State)(-1);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool _003CCrtImplementationDetails_003E_002EDefaultDomain_002EHasNative()
	{
		if (_003FhasNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A == (TriBool.State)2)
		{
			void** ptr = (void**)Unsafe.AsPointer(ref __xi_a);
			if (Unsafe.IsAddressLessThan(ref __xi_a, ref __xi_z))
			{
				do
				{
					if (*(int*)ptr == 0)
					{
						ptr = (void**)((byte*)ptr + 4);
						continue;
					}
					_003FhasNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A = (TriBool.State)(-1);
					return true;
				}
				while (ptr < Unsafe.AsPointer(ref __xi_z));
			}
			void** ptr2 = (void**)Unsafe.AsPointer(ref __xc_a);
			if (Unsafe.IsAddressLessThan(ref __xc_a, ref __xc_z))
			{
				do
				{
					if (*(int*)ptr2 == 0)
					{
						ptr2 = (void**)((byte*)ptr2 + 4);
						continue;
					}
					_003FhasNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A = (TriBool.State)(-1);
					return true;
				}
				while (ptr2 < Unsafe.AsPointer(ref __xc_z));
			}
			_003FhasNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A = (TriBool.State)0;
			return false;
		}
		return _003FhasNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00400W4State_0040TriBool_00402_0040A == (TriBool.State)(-1);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool _003CCrtImplementationDetails_003E_002EDefaultDomain_002ENeedsInitialization()
	{
		int num = (((_003CCrtImplementationDetails_003E_002EDefaultDomain_002EHasPerProcess() && !_003FInitializedPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA) || (_003CCrtImplementationDetails_003E_002EDefaultDomain_002EHasNative() && !_003FInitializedNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA && __native_startup_state == (__enative_startup_state)0)) ? 1 : 0);
		return (byte)num != 0;
	}

	internal unsafe static void _003CCrtImplementationDetails_003E_002EDefaultDomain_002EInitialize()
	{
		_003CCrtImplementationDetails_003E_002EDoCallBackInDefaultDomain((delegate* unmanaged[Stdcall, Stdcall]<void*, int>)__unep_0040_003FDoNothing_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024FCGJPAX_0040Z, null);
	}

	internal static void _003FA0x250abb45_002E_003F_003F__E_003FInitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA_0040_0040YMXXZ()
	{
		_003FInitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA = 0;
	}

	internal static void _003FA0x250abb45_002E_003F_003F__E_003FUninitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA_0040_0040YMXXZ()
	{
		_003FUninitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA = 0;
	}

	internal static void _003FA0x250abb45_002E_003F_003F__E_003FIsDefaultDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2_NA_0040_0040YMXXZ()
	{
		_003FIsDefaultDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2_NA = false;
	}

	internal static void _003FA0x250abb45_002E_003F_003F__E_003FInitializedVtables_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A_0040_0040YMXXZ()
	{
		_003FInitializedVtables_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)0;
	}

	internal static void _003FA0x250abb45_002E_003F_003F__E_003FInitializedNative_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A_0040_0040YMXXZ()
	{
		_003FInitializedNative_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)0;
	}

	internal static void _003FA0x250abb45_002E_003F_003F__E_003FInitializedPerProcess_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A_0040_0040YMXXZ()
	{
		_003FInitializedPerProcess_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)0;
	}

	internal static void _003FA0x250abb45_002E_003F_003F__E_003FInitializedPerAppDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A_0040_0040YMXXZ()
	{
		_003FInitializedPerAppDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)0;
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeVtables(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0, "The C++ module failed to load during vtable initialization.\n");
		_003FInitializedVtables_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)1;
		_initterm_m((delegate*<void*>*)Unsafe.AsPointer(ref _003FA0x250abb45_002E__xi_vt_a), (delegate*<void*>*)Unsafe.AsPointer(ref _003FA0x250abb45_002E__xi_vt_z));
		_003FInitializedVtables_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)2;
	}

	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeDefaultAppDomain(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		_003CCrtImplementationDetails_003E_002EDefaultDomain_002EInitialize();
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeNative(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0, "The C++ module failed to load during native initialization.\n");
		__security_init_cookie();
		_003FInitializedNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA = true;
		if (!_003CCrtImplementationDetails_003E_002ENativeDll_002EIsSafeForManagedCode())
		{
			_amsg_exit(33);
		}
		if (__native_startup_state == (__enative_startup_state)1)
		{
			_amsg_exit(33);
		}
		else if (__native_startup_state == (__enative_startup_state)0)
		{
			_003FInitializedNative_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)1;
			__native_startup_state = (__enative_startup_state)1;
			if (_initterm_e((delegate* unmanaged[Cdecl, Cdecl]<int>*)Unsafe.AsPointer(ref __xi_a), (delegate* unmanaged[Cdecl, Cdecl]<int>*)Unsafe.AsPointer(ref __xi_z)) != 0)
			{
				_003CCrtImplementationDetails_003E_002EThrowModuleLoadException(gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_002EP_0024AAVString_0040System_0040_0040((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0));
			}
			_initterm((delegate* unmanaged[Cdecl, Cdecl]<void>*)Unsafe.AsPointer(ref __xc_a), (delegate* unmanaged[Cdecl, Cdecl]<void>*)Unsafe.AsPointer(ref __xc_z));
			__native_startup_state = (__enative_startup_state)2;
			_003FInitializedNativeFromCCTOR_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA = true;
			_003FInitializedNative_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)2;
		}
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializePerProcess(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0, "The C++ module failed to load during process initialization.\n");
		_003FInitializedPerProcess_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)1;
		_initatexit_m();
		_initterm_m((delegate*<void*>*)Unsafe.AsPointer(ref _003FA0x250abb45_002E__xc_mp_a), (delegate*<void*>*)Unsafe.AsPointer(ref _003FA0x250abb45_002E__xc_mp_z));
		_003FInitializedPerProcess_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)2;
		_003FInitializedPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA = true;
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializePerAppDomain(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0, "The C++ module failed to load during appdomain initialization.\n");
		_003FInitializedPerAppDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)1;
		_initatexit_app_domain();
		_initterm_m((delegate*<void*>*)Unsafe.AsPointer(ref _003FA0x250abb45_002E__xc_ma_a), (delegate*<void*>*)Unsafe.AsPointer(ref _003FA0x250abb45_002E__xc_ma_z));
		_003FInitializedPerAppDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2W4State_0040Progress_00402_0040A = (Progress.State)2;
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeUninitializer(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0, "The C++ module failed to load during registration for the unload events.\n");
		_003CCrtImplementationDetails_003E_002ERegisterModuleUninitializer([PrePrepareMethod] [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] (object source, EventArgs arguments) =>
		{
			if (_003FInitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA != 0 && Interlocked.Exchange(ref _003FUninitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA, 1) == 0)
			{
				bool num = Interlocked.Decrement(ref _003FCount_0040AllDomains_0040_003CCrtImplementationDetails_003E_0040_00402HA) == 0;
				_app_exit_callback();
				if (num)
				{
					_003CCrtImplementationDetails_003E_002ELanguageSupport_002EUninitializeDefaultDomain();
				}
			}
		});
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002E_Initialize(LanguageSupport* P_0)
	{
		_003FIsDefaultDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		if (_003FIsDefaultDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2_NA)
		{
			_003FEntered_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA = true;
		}
		_003CCrtImplementationDetails_003E_002EDoDllLanguageSupportValidation();
		void* ptr = _getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					IntPtr comparand = (IntPtr)0;
					IntPtr value = (IntPtr)ptr;
					void* ptr2 = (void*)Interlocked.CompareExchange(ref Unsafe.As<void*, IntPtr>(ref __native_startup_lock), value, comparand);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					Sleep(1000u);
				}
			}
			_003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeVtables(P_0);
			if (_003FIsDefaultDomain_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2_NA)
			{
				_003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeNative(P_0);
				_003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializePerProcess(P_0);
			}
			else if (_003CCrtImplementationDetails_003E_002EDefaultDomain_002ENeedsInitialization())
			{
				num3 = 1;
			}
		}
		finally
		{
			if (num == 0)
			{
				IntPtr value2 = (IntPtr)0;
				Interlocked.Exchange(ref Unsafe.As<void*, IntPtr>(ref __native_startup_lock), value2);
			}
		}
		if (num3 != 0)
		{
			_003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeDefaultAppDomain(P_0);
		}
		_003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializePerAppDomain(P_0);
		_003FInitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA = 1;
		_003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitializeUninitializer(P_0);
	}

	internal static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EUninitializeAppDomain()
	{
		_app_exit_callback();
	}

	internal unsafe static int _003CCrtImplementationDetails_003E_002ELanguageSupport_002E_UninitializeDefaultDomain(void* cookie)
	{
		_exit_callback();
		_003FInitializedPerProcess_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA = false;
		if (_003FInitializedNativeFromCCTOR_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA)
		{
			_cexit();
			__native_startup_state = (__enative_startup_state)0;
			_003FInitializedNativeFromCCTOR_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA = false;
		}
		_003FInitializedNative_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA = false;
		return 0;
	}

	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EUninitializeDefaultDomain()
	{
		if (_003FEntered_0040DefaultDomain_0040_003CCrtImplementationDetails_003E_0040_00402_NA)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				_003CCrtImplementationDetails_003E_002ELanguageSupport_002E_UninitializeDefaultDomain(null);
			}
			else
			{
				_003CCrtImplementationDetails_003E_002EDoCallBackInDefaultDomain((delegate* unmanaged[Stdcall, Stdcall]<void*, int>)__unep_0040_003F_UninitializeDefaultDomain_0040LanguageSupport_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024FCGJPAX_0040Z, null);
			}
		}
	}

	[PrePrepareMethod]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EDomainUnload(object source, EventArgs arguments)
	{
		if (_003FInitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA != 0 && Interlocked.Exchange(ref _003FUninitialized_0040CurrentDomain_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q2HA, 1) == 0)
		{
			bool num = Interlocked.Decrement(ref _003FCount_0040AllDomains_0040_003CCrtImplementationDetails_003E_0040_00402HA) == 0;
			_app_exit_callback();
			if (num)
			{
				_003CCrtImplementationDetails_003E_002ELanguageSupport_002EUninitializeDefaultDomain();
			}
		}
	}

	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002ECleanup(LanguageSupport* P_0, Exception innerException)
	{
		try
		{
			bool flag = Interlocked.Decrement(ref _003FCount_0040AllDomains_0040_003CCrtImplementationDetails_003E_0040_00402HA) == 0;
			_003CCrtImplementationDetails_003E_002ELanguageSupport_002EUninitializeAppDomain();
			if (flag)
			{
				_003CCrtImplementationDetails_003E_002ELanguageSupport_002EUninitializeDefaultDomain();
			}
		}
		catch (Exception ex)
		{
			_003CCrtImplementationDetails_003E_002EThrowNestedModuleLoadException(innerException, ex);
		}
		catch
		{
			_003CCrtImplementationDetails_003E_002EThrowNestedModuleLoadException(innerException, null);
		}
	}

	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitialize(LanguageSupport* P_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref _003FCount_0040AllDomains_0040_003CCrtImplementationDetails_003E_0040_00402HA);
				flag = true;
			}
			_003CCrtImplementationDetails_003E_002ELanguageSupport_002E_Initialize(P_0);
		}
		catch (Exception ex)
		{
			if (flag)
			{
				_003CCrtImplementationDetails_003E_002ELanguageSupport_002ECleanup(P_0, ex);
			}
			_003CCrtImplementationDetails_003E_002EThrowModuleLoadException(gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_002EP_0024AAVString_0040System_0040_0040((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0), ex);
		}
		catch
		{
			if (flag)
			{
				_003CCrtImplementationDetails_003E_002ELanguageSupport_002ECleanup(P_0, null);
			}
			_003CCrtImplementationDetails_003E_002EThrowModuleLoadException(gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_002EP_0024AAVString_0040System_0040_0040((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0), null);
		}
	}

	[DebuggerStepThrough]
	static unsafe _003CModule_003E()
	{
		Unsafe.SkipInit(out LanguageSupport languageSupport);
		_003CCrtImplementationDetails_003E_002ELanguageSupport_002E_007Bctor_007D(&languageSupport);
		try
		{
			_003CCrtImplementationDetails_003E_002ELanguageSupport_002EInitialize(&languageSupport);
		}
		catch
		{
			//try-fault
			___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<LanguageSupport*, void>)(&_003CCrtImplementationDetails_003E_002ELanguageSupport_002E_007Bdtor_007D), &languageSupport);
			throw;
		}
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)(&languageSupport));
	}

	internal unsafe static LanguageSupport* _003CCrtImplementationDetails_003E_002ELanguageSupport_002E_007Bctor_007D(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_007Bctor_007D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0);
		return P_0;
	}

	internal unsafe static void _003CCrtImplementationDetails_003E_002ELanguageSupport_002E_007Bdtor_007D(LanguageSupport* P_0)
	{
		gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_007Bdtor_007D((gcroot_003CSystem_003A_003AString_0020_005E_003E*)P_0);
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AString_0020_005E_003E* gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_007Bctor_007D(gcroot_003CSystem_003A_003AString_0020_005E_003E* P_0)
	{
		*(int*)P_0 = (int)((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return P_0;
	}

	[DebuggerStepThrough]
	internal unsafe static void gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_007Bdtor_007D(gcroot_003CSystem_003A_003AString_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		((GCHandle)intPtr).Free();
		*(int*)P_0 = 0;
	}

	[DebuggerStepThrough]
	internal unsafe static gcroot_003CSystem_003A_003AString_0020_005E_003E* gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_003D(gcroot_003CSystem_003A_003AString_0020_005E_003E* P_0, string t)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		GCHandle gCHandle = (GCHandle)intPtr;
		gCHandle.Target = t;
		return P_0;
	}

	internal unsafe static string gcroot_003CSystem_003A_003AString_0020_005E_003E_002E_002EP_0024AAVString_0040System_0040_0040(gcroot_003CSystem_003A_003AString_0020_005E_003E* P_0)
	{
		IntPtr intPtr = new IntPtr((void*)(int)(*(uint*)P_0));
		return (string)((GCHandle)intPtr).Target;
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void ___CxxCallUnwindDtor(delegate*<void*, void> pDtor, void* pThis)
	{
		try
		{
			pDtor(pThis);
		}
		catch when (__FrameUnwindFilter((_EXCEPTION_POINTERS*)Marshal.GetExceptionPointers()) != 0)
		{
		}
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void ___CxxCallUnwindVecDtor(delegate*<void*, uint, int, delegate*<void*, void>, void> pVecDtor, void* ptr, uint size, int count, delegate*<void*, void> pDtor)
	{
		try
		{
			pVecDtor(ptr, size, count, pDtor);
		}
		catch when (__FrameUnwindFilter((_EXCEPTION_POINTERS*)Marshal.GetExceptionPointers()) != 0)
		{
		}
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void __ehvec_dtor(void* ptr, uint size, int count, delegate*<void*, void> pDtor)
	{
		int num = 0;
		ptr = (int)size * count + (byte*)ptr;
		try
		{
			while (true)
			{
				count--;
				if (count < 0)
				{
					break;
				}
				ptr = (byte*)ptr - (int)size;
				pDtor(ptr);
			}
			num = 1;
		}
		finally
		{
			if (num == 0)
			{
				__ArrayUnwind(ptr, size, count, pDtor);
			}
		}
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static int _003FA0x644ad69d_002EArrayUnwindFilter(_EXCEPTION_POINTERS* pExPtrs)
	{
		if (*(int*)(int)(*(uint*)pExPtrs) != -529697949)
		{
			return 0;
		}
		terminate();
		return 0;
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void __ArrayUnwind(void* ptr, uint size, int count, delegate*<void*, void> pDtor)
	{
		try
		{
			while (true)
			{
				count--;
				if (count >= 0)
				{
					ptr = (byte*)ptr - (int)size;
					pDtor(ptr);
					continue;
				}
				break;
			}
		}
		catch when (_003FA0x644ad69d_002EArrayUnwindFilter((_EXCEPTION_POINTERS*)Marshal.GetExceptionPointers()) != 0)
		{
		}
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void __ehvec_ctor(void* ptr, uint size, int count, delegate*<void*, void> pCtor, delegate*<void*, void> pDtor)
	{
		int num = 0;
		Unsafe.SkipInit(out int i);
		try
		{
			for (i = 0; i < count; i++)
			{
				pCtor(ptr);
				ptr = (int)size + (byte*)ptr;
			}
			num = 1;
		}
		finally
		{
			if (num == 0)
			{
				__ArrayUnwind(ptr, size, i, pDtor);
			}
		}
	}

	[DebuggerStepThrough]
	internal unsafe static ValueType _003CCrtImplementationDetails_003E_002EAtExitLock_002E_handle()
	{
		if (_003F_lock_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0PAXA != null)
		{
			IntPtr value = new IntPtr(_003F_lock_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0PAXA);
			return GCHandle.FromIntPtr(value);
		}
		return null;
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Set(object value)
	{
		ValueType valueType = _003CCrtImplementationDetails_003E_002EAtExitLock_002E_handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			_003F_lock_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0PAXA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	[DebuggerStepThrough]
	internal static object _003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Get()
	{
		ValueType valueType = _003CCrtImplementationDetails_003E_002EAtExitLock_002E_handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Destruct()
	{
		ValueType valueType = _003CCrtImplementationDetails_003E_002EAtExitLock_002E_handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			_003F_lock_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0PAXA = null;
		}
	}

	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool _003CCrtImplementationDetails_003E_002EAtExitLock_002EIsInitialized()
	{
		return _003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Get() != null;
	}

	[DebuggerStepThrough]
	internal unsafe static void _003CCrtImplementationDetails_003E_002EAtExitLock_002EAddRef()
	{
		if (!_003CCrtImplementationDetails_003E_002EAtExitLock_002EIsInitialized())
		{
			object value = new object();
			_003F_lock_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0PAXA = null;
			_003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Set(value);
			_003F_ref_count_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0HA = 0;
		}
		_003F_ref_count_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0HA++;
	}

	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool _003FA0x11773762_002E__global_lock()
	{
		bool result = false;
		if (_003CCrtImplementationDetails_003E_002EAtExitLock_002EIsInitialized())
		{
			Monitor.Enter(_003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Get());
			result = true;
		}
		return result;
	}

	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool _003FA0x11773762_002E__global_unlock()
	{
		bool result = false;
		if (_003CCrtImplementationDetails_003E_002EAtExitLock_002EIsInitialized())
		{
			Monitor.Exit(_003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Get());
			result = true;
		}
		return result;
	}

	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool _003FA0x11773762_002E__alloc_global_lock()
	{
		_003CCrtImplementationDetails_003E_002EAtExitLock_002EAddRef();
		return _003CCrtImplementationDetails_003E_002EAtExitLock_002EIsInitialized();
	}

	[DebuggerStepThrough]
	internal static void _003FA0x11773762_002E__dealloc_global_lock()
	{
		_003F_ref_count_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0HA--;
		if (_003F_ref_count_0040AtExitLock_0040_003CCrtImplementationDetails_003E_0040_0040_0024_0024Q0HA == 0)
		{
			_003CCrtImplementationDetails_003E_002EAtExitLock_002E_lock_Destruct();
		}
	}

	internal unsafe static int _atexit_helper(delegate*<void> func, uint* __pexit_list_size, delegate*<void>** __ponexitend_e, delegate*<void>** __ponexitbegin_e)
	{
		delegate*<void> delegate_002A = null;
		if (func == (delegate*<void>)null)
		{
			return -1;
		}
		if (_003FA0x11773762_002E__global_lock())
		{
			try
			{
				delegate*<void>* ptr = (delegate*<void>*)_decode_pointer((void*)(int)(*(uint*)__ponexitbegin_e));
				delegate*<void>* ptr2 = (delegate*<void>*)_decode_pointer((void*)(int)(*(uint*)__ponexitend_e));
				delegate*<void>* ptr3 = (delegate*<void>*)((byte*)ptr2 - (nuint)ptr);
				if ((nuint)(int)(*__pexit_list_size - 1) < (nuint)ptr3 >> 2)
				{
					try
					{
						uint num = *__pexit_list_size * 4;
						uint num2 = ((num >= 2048) ? 2048u : num);
						IntPtr cb = new IntPtr((int)(num + num2));
						IntPtr pv = new IntPtr(ptr);
						IntPtr intPtr = Marshal.ReAllocHGlobal(pv, cb);
						ptr2 = (delegate*<void>*)((byte*)intPtr.ToPointer() + (nuint)ptr3);
						ptr = (delegate*<void>*)intPtr.ToPointer();
						uint num3 = *__pexit_list_size;
						uint num4 = ((512 >= num3) ? num3 : 512u);
						*__pexit_list_size = num3 + num4;
					}
					catch (OutOfMemoryException)
					{
						IntPtr cb2 = new IntPtr((int)(*__pexit_list_size * 4 + 8));
						IntPtr pv2 = new IntPtr(ptr);
						IntPtr intPtr2 = Marshal.ReAllocHGlobal(pv2, cb2);
						ptr2 = (delegate*<void>*)((byte*)intPtr2.ToPointer() - (nuint)ptr + (nuint)ptr2);
						ptr = (delegate*<void>*)intPtr2.ToPointer();
						*__pexit_list_size += 4u;
					}
				}
				*(int*)ptr2 = (int)func;
				ptr2 = (delegate*<void>*)((byte*)ptr2 + 4);
				delegate_002A = func;
				*(int*)__ponexitbegin_e = (int)_encode_pointer(ptr);
				*(int*)__ponexitend_e = (int)_encode_pointer(ptr2);
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				_003FA0x11773762_002E__global_unlock();
			}
			if (delegate_002A != (delegate*<void>)null)
			{
				return 0;
			}
		}
		return -1;
	}

	internal unsafe static void _exit_callback()
	{
		if (_003FA0x11773762_002E__exit_list_size == 0)
		{
			return;
		}
		delegate*<void>* ptr = (delegate*<void>*)_decode_pointer(_003FA0x11773762_002E__onexitbegin_m);
		delegate*<void>* ptr2 = (delegate*<void>*)_decode_pointer(_003FA0x11773762_002E__onexitend_m);
		if (ptr != (delegate*<void>*)(-1) && ptr != null && ptr2 != null)
		{
			delegate*<void>* ptr3 = ptr;
			delegate*<void>* ptr4 = ptr2;
			while (true)
			{
				ptr2 = (delegate*<void>*)((byte*)ptr2 - 4);
				if (ptr2 < ptr)
				{
					break;
				}
				if ((void*)(*(int*)ptr2) != _encoded_null())
				{
					void* intPtr = _decode_pointer((void*)(int)(*(uint*)ptr2));
					*(int*)ptr2 = (int)_encoded_null();
					((delegate*<void>)intPtr)();
					delegate*<void>* ptr5 = (delegate*<void>*)_decode_pointer(_003FA0x11773762_002E__onexitbegin_m);
					delegate*<void>* ptr6 = (delegate*<void>*)_decode_pointer(_003FA0x11773762_002E__onexitend_m);
					if (ptr3 != ptr5 || ptr4 != ptr6)
					{
						ptr3 = ptr5;
						ptr = ptr5;
						ptr4 = ptr6;
						ptr2 = ptr6;
					}
				}
			}
			IntPtr hglobal = new IntPtr(ptr);
			Marshal.FreeHGlobal(hglobal);
		}
		_003FA0x11773762_002E__dealloc_global_lock();
	}

	[DebuggerStepThrough]
	internal unsafe static int _initatexit_m()
	{
		int result = 0;
		if (_003FA0x11773762_002E__alloc_global_lock())
		{
			_003FA0x11773762_002E__onexitbegin_m = (delegate*<void>*)_encode_pointer(Marshal.AllocHGlobal(128).ToPointer());
			_003FA0x11773762_002E__onexitend_m = _003FA0x11773762_002E__onexitbegin_m;
			_003FA0x11773762_002E__exit_list_size = 32u;
			result = 1;
		}
		return result;
	}

	internal unsafe static int _atexit_m(delegate*<void> func)
	{
		return _atexit_helper((delegate*<void>)_encode_pointer(func), (uint*)Unsafe.AsPointer(ref _003FA0x11773762_002E__exit_list_size), (delegate*<void>**)Unsafe.AsPointer(ref _003FA0x11773762_002E__onexitend_m), (delegate*<void>**)Unsafe.AsPointer(ref _003FA0x11773762_002E__onexitbegin_m));
	}

	[DebuggerStepThrough]
	internal unsafe static int _initatexit_app_domain()
	{
		if (_003FA0x11773762_002E__alloc_global_lock())
		{
			__onexitbegin_app_domain = (delegate*<void>*)_encode_pointer(Marshal.AllocHGlobal(128).ToPointer());
			__onexitend_app_domain = __onexitbegin_app_domain;
			__exit_list_size_app_domain = 32u;
		}
		return 1;
	}

	internal unsafe static void _app_exit_callback()
	{
		if (__exit_list_size_app_domain == 0)
		{
			return;
		}
		delegate*<void>* ptr = (delegate*<void>*)_decode_pointer(__onexitbegin_app_domain);
		delegate*<void>* ptr2 = (delegate*<void>*)_decode_pointer(__onexitend_app_domain);
		try
		{
			if (ptr == (delegate*<void>*)(-1) || ptr == null || ptr2 == null)
			{
				return;
			}
			delegate*<void> delegate_002A = null;
			delegate*<void>* ptr3 = ptr;
			delegate*<void>* ptr4 = ptr2;
			while (true)
			{
				delegate*<void>* ptr5 = null;
				delegate*<void>* ptr6 = null;
				do
				{
					ptr2 = (delegate*<void>*)((byte*)ptr2 - 4);
				}
				while (ptr2 >= ptr && (void*)(*(int*)ptr2) == _encoded_null());
				if (ptr2 >= ptr)
				{
					delegate_002A = (delegate*<void>)_decode_pointer((void*)(int)(*(uint*)ptr2));
					*(int*)ptr2 = (int)_encoded_null();
					delegate_002A();
					delegate*<void>* ptr7 = (delegate*<void>*)_decode_pointer(__onexitbegin_app_domain);
					delegate*<void>* ptr8 = (delegate*<void>*)_decode_pointer(__onexitend_app_domain);
					if (ptr3 != ptr7 || ptr4 != ptr8)
					{
						ptr3 = ptr7;
						ptr = ptr7;
						ptr4 = ptr8;
						ptr2 = ptr8;
					}
					continue;
				}
				break;
			}
		}
		finally
		{
			IntPtr hglobal = new IntPtr(ptr);
			Marshal.FreeHGlobal(hglobal);
			_003FA0x11773762_002E__dealloc_global_lock();
		}
	}

	[DebuggerStepThrough]
	internal unsafe static int _initterm_e(delegate* unmanaged[Cdecl, Cdecl]<int>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<int>* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				uint num2 = *(uint*)pfbegin;
				if (num2 != 0)
				{
					num = ((delegate* unmanaged[Cdecl, Cdecl]<int>)(int)num2)();
				}
				pfbegin = (delegate* unmanaged[Cdecl, Cdecl]<int>*)((byte*)pfbegin + 4);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	[DebuggerStepThrough]
	internal unsafe static void _initterm(delegate* unmanaged[Cdecl, Cdecl]<void>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<void>* pfend)
	{
		if (pfbegin >= pfend)
		{
			return;
		}
		do
		{
			uint num = *(uint*)pfbegin;
			if (num != 0)
			{
				((delegate* unmanaged[Cdecl, Cdecl]<void>)(int)num)();
			}
			pfbegin = (delegate* unmanaged[Cdecl, Cdecl]<void>*)((byte*)pfbegin + 4);
		}
		while (pfbegin < pfend);
	}

	[DebuggerStepThrough]
	internal static ModuleHandle _003CCrtImplementationDetails_003E_002EThisModule_002EHandle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	[DebuggerStepThrough]
	internal unsafe static void _initterm_m(delegate*<void*>* pfbegin, delegate*<void*>* pfend)
	{
		if (pfbegin >= pfend)
		{
			return;
		}
		do
		{
			uint num = *(uint*)pfbegin;
			if (num != 0)
			{
				_003CCrtImplementationDetails_003E_002EThisModule_002EResolveMethod_003Cvoid_0020const_0020_002A_0020__clrcall_0028void_0029_003E((delegate*<void*>)(int)num)();
			}
			pfbegin = (delegate*<void*>*)((byte*)pfbegin + 4);
		}
		while (pfbegin < pfend);
	}

	[DebuggerStepThrough]
	internal unsafe static delegate*<void*> _003CCrtImplementationDetails_003E_002EThisModule_002EResolveMethod_003Cvoid_0020const_0020_002A_0020__clrcall_0028void_0029_003E(delegate*<void*> methodToken)
	{
		return (delegate*<void*>)_003CCrtImplementationDetails_003E_002EThisModule_002EHandle().ResolveMethodHandle((int)methodToken).GetFunctionPointer().ToPointer();
	}

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _ZuneShipAssert(uint P_0, uint P_1);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern WString* WString_002E_007Bctor_007D(WString* P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void WString_002E_007Bdtor_007D(WString* P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern WBSTRString* WBSTRString_002E_007Bctor_007D(WBSTRString* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void ZuneLibraryExports_002EShipAssert(uint P_0, uint P_1, ushort* P_2);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetSingleton(_GUID P_0, void** P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SafeArrayDestroy(tagSAFEARRAY* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SafeArrayGetElement(tagSAFEARRAY* P_0, int* P_1, void* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SafeArrayGetLBound(tagSAFEARRAY* P_0, uint P_1, int* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SafeArrayGetUBound(tagSAFEARRAY* P_0, uint P_1, int* P_2);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* @new(uint P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void delete(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int InterlockedDecrement(int* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int InterlockedIncrement(int* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern ushort* SysAllocString(ushort* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void SysFreeString(ushort* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EPhase3Initialization(IAsyncCallback* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EAddItemToPlaylist(int P_0, int P_1, IPlaylist* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int PropVariantClear(tagPROPVARIANT* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateEmptyPlaylist(IPlaylist** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EQueryDatabase(EQueryType P_0, IQueryPropertyBag* P_1, IDatabaseQueryResults** P_2, ushort** P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CoCreateInstance(_GUID* P_0, IUnknown* P_1, uint P_2, _GUID* P_3, void** P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* GetClipboardData(uint P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int CloseClipboard();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int IsClipboardFormatAvailable(uint P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int OpenClipboard(HWND__* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint DragQueryFileW(HDROP__* P_0, uint P_1, ushort* P_2, uint P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* GlobalLock(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern uint GetLastError();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* CopyImage(void* P_0, uint P_1, int P_2, int P_3, uint P_4);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint TraceMessage(ulong P_0, uint P_1, _GUID* P_2, ushort P_3, __arglist);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int memcpy_s(void* P_0, uint P_1, void* P_2, uint P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int lstrlenW(ushort* P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void delete_005B_005D(void* P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* new_005B_005D(uint P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetConfigurationManagerInstance(IConfigurationManager** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateContentRefreshTask(IAsyncCallback* P_0, IContentRefreshTask** P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EDestroyDataObjectEnum(IDataObjectEnumerator* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateDataObjectEnum(IDataObjectEnumerator** P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void WString_002ESysFreeString(ushort** P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _ZuneShipAssertForHr(int P_0, uint P_1);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int DataStructs_002EIntSet_002EGetNextMember(IntSet* P_0, int P_1);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int DataStructs_002EIntSet_002EMemberCount(IntSet* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint SysStringLen(ushort* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void LeaveCriticalSection(_RTL_CRITICAL_SECTION* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void EnterCriticalSection(_RTL_CRITICAL_SECTION* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void DeleteCriticalSection(_RTL_CRITICAL_SECTION* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void InitializeCriticalSection(_RTL_CRITICAL_SECTION* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateDRMQuery(IDRMQuery** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetMappedErrorDescriptionAndUrl(int P_0, eErrorCondition P_1, int* P_2, ushort** P_3, ushort** P_4);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void WString_002EAttachBSTR(WString* P_0, ushort* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateNativeFileAssociationHandler(void** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern uint SetThreadExecutionState(uint P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int WString_002EAllocateBSTR(WString* P_0, ushort** P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetFieldValues(int P_0, EListType P_1, int P_2, DBPropertyRequestStruct* P_3, IQueryPropertyBag* P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EUserCardsForMedia(_GUID P_0, EMediaTypes P_1, int P_2, int P_3, int P_4, int* P_5, int** P_6, int** P_7);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CloseHandle(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint WaitForSingleObject(void* P_0, uint P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* CreateEventW(_SECURITY_ATTRIBUTES* P_0, int P_1, int P_2, ushort* P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SetEvent(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CreateStreamOnHGlobal(void* P_0, int P_1, IStream** P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int VariantClear(tagVARIANT* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void VariantInit(tagVARIANT* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CreateNSSManager(INSSManager** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CreateHMESettings(IHMESettings** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EInteropNotifyUnAdvise(uint P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EInteropNotifyAdvise(IInteropNotify* P_0, uint* P_1);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern CSchemaMap._SCHEMAMAPENTRY* CSchemaMap_002EGetEntry(ushort* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ELocateArt(int P_0, EMediaTypes P_1, [MarshalAs(UnmanagedType.U1)] bool P_2, ushort** P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ESetAlbumArt(int P_0, HBITMAP__* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ESetAlbumArt(int P_0, ushort* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint TraceEvent(ulong P_0, _EVENT_TRACE_HEADER* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EDeleteMedia(EMediaTypes P_0, int* P_1, int P_2, int P_3, int P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int MBRBandwidthTest_CreateInstance(IZuneMBRBandwidthTest** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreatePropertySet(_GUID* P_0, uint P_1, IMSMediaSchemaPropertySet** P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreatePropertySetList(_GUID* P_0, uint P_1, IMSMediaSchemaPropertyList** P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateTrackPropSet(_GUID P_0, _GUID P_1, int P_2, ushort* P_3, int P_4, ushort* P_5, ushort* P_6, ushort* P_7, IMSMediaSchemaPropertySet** P_8);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EMetadataChangeUnAdvise(uint P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EMetadataChangeAdvise(IMetadataChangeNotify* P_0, uint* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void WmpCoreDeinitialize();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CWmpPlayer_GetInstance(IMCPlayer** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int WmpCoreInitialize();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int QueueUserWorkItem(delegate* unmanaged[Stdcall, Stdcall]<void*, uint> P_0, void* P_1, uint P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateMultiSortAttributes(int P_0, IMultiSortAttributes** P_1);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int _wcsicmp(ushort* P_0, ushort* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreatePropertyBag(IQueryPropertyBag** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CompareStringW(uint P_0, uint P_1, ushort* P_2, int P_3, ushort* P_4, int P_5);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int DeleteObject(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetThumbnailBitmapData(ushort* P_0, int* P_1, int* P_2, void** P_3, HBITMAP__** P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECopyThumbnailBitmapData(HBITMAP__* P_0, int P_1, int P_2, int P_3, int P_4, int P_5, int P_6, HBITMAP__** P_7, void** P_8);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetObjectW(void* P_0, int P_1, void* P_2);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int WString_002EInit(WString* P_0, ushort* P_1, uint P_2);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void WString_002EDeleteString(WString* P_0);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int _i64tow_s(long P_0, ushort* P_1, uint P_2, int P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetServiceEndPointUri(EServiceEndpointId P_0, ushort** P_1);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int _wtoi(ushort* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ESetFieldValues(int P_0, EListType P_1, int P_2, DBPropertySubmitStruct* P_3, IQueryPropertyBag* P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int PropVariantCopy(tagPROPVARIANT* P_0, tagPROPVARIANT* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateNativeSubscriptionViewer(void** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetSyncRuleForMedia(int P_0, EMediaTypes P_1, int P_2, EDeviceSyncRuleType* P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EDeleteDeviceSyncRules(EDeviceSyncRuleType P_0, int P_1, EMediaTypes P_2, int* P_3, int P_4, [MarshalAs(UnmanagedType.U1)] bool P_5);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EAddDeviceSyncRule(EDeviceSyncRuleType P_0, [MarshalAs(UnmanagedType.U1)] bool P_1, int P_2, EMediaTypes P_3, int P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetSyncRuleValueForMedia(EDeviceSyncRuleType P_0, int P_1, EMediaTypes P_2, int P_3, int* P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EAddDeviceSyncRuleWithValue(EDeviceSyncRuleType P_0, int P_1, EMediaTypes P_2, int P_3, int P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int KillTimer(HWND__* P_0, uint P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint SetTimer(HWND__* P_0, uint P_1, uint P_2, delegate* unmanaged[Stdcall, Stdcall]<HWND__*, uint, uint, uint, void> P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SetForegroundWindow(HWND__* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint SendInput(uint P_0, tagINPUT* P_1, int P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HWND__* GetForegroundWindow();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HMONITOR__* MonitorFromPoint(tagPOINT P_0, uint P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int PtInRect(tagRECT* P_0, tagPOINT P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetClientRect(HWND__* P_0, tagRECT* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ScreenToClient(HWND__* P_0, tagPOINT* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetCursorPos(tagPOINT* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int InflateRect(tagRECT* P_0, int P_1, int P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CopyRect(tagRECT* P_0, tagRECT* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int IntersectRect(tagRECT* P_0, tagRECT* P_1, tagRECT* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int OffsetRect(tagRECT* P_0, int P_1, int P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetWindowPlacement(HWND__* P_0, tagWINDOWPLACEMENT* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetMonitorInfoW(HMONITOR__* P_0, tagMONITORINFO* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HMONITOR__* MonitorFromWindow(HWND__* P_0, uint P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HWND__* CreateWindowExW(uint P_0, ushort* P_1, ushort* P_2, uint P_3, int P_4, int P_5, int P_6, int P_7, HWND__* P_8, HMENU__* P_9, HINSTANCE__* P_10, void* P_11);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern ushort RegisterClassW(tagWNDCLASSW* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HICON__* LoadCursorW(HINSTANCE__* P_0, ushort* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetClassInfoW(HINSTANCE__* P_0, ushort* P_1, tagWNDCLASSW* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetWindowLongW(HWND__* P_0, int P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint RegisterWindowMessageW(ushort* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int DefWindowProcW(HWND__* P_0, uint P_1, uint P_2, int P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int PostMessageW(HWND__* P_0, uint P_1, uint P_2, int P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int IsWindow(HWND__* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SafeArrayPutElement(tagSAFEARRAY* P_0, int* P_1, void* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern tagSAFEARRAY* SafeArrayCreateVector(ushort P_0, int P_1, uint P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int CallNtPowerInformation(POWER_INFORMATION_LEVEL P_0, void* P_1, uint P_2, void* P_3, uint P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SendMessageTimeoutW(HWND__* P_0, uint P_1, uint P_2, int P_3, uint P_4, uint P_5, uint* P_6);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HWND__* FindWindowExW(HWND__* P_0, HWND__* P_1, ushort* P_2, ushort* P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SystemTimeToFileTime(_SYSTEMTIME* P_0, _FILETIME* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int FileTimeToSystemTime(_FILETIME* P_0, _SYSTEMTIME* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* LocalFree(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint FormatMessageW(uint P_0, void* P_1, uint P_2, uint P_3, ushort* P_4, uint P_5, sbyte** P_6);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int GetVersionExW(_OSVERSIONINFOW* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECopyThumbnailBitmapData(HBITMAP__* P_0, HBITMAP__** P_1, void** P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EDoesFileExist(ushort* P_0, int* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECompareWithoutArticles(ushort* P_0, ushort* P_1, int* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002ESplitAudioTrack(int P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetAlbumMetadataForAlbumId(long P_0, int P_1, IAlbumInfo* P_2, IWMISGetAlbumForAlbumIdCallback* P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EUpdateAlbumMetadata(int P_0, IAlbumInfo* P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetAlbumMetadata(int P_0, [MarshalAs(UnmanagedType.U1)] bool P_1, IAlbumInfo** P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetLocalizedPathOfFolder(ushort* P_0, [MarshalAs(UnmanagedType.U1)] bool P_1, ushort** P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetKnownFolders(DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_0, DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_1, DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_2, DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_3, DynamicArray_003Cunsigned_0020short_0020_002A_003E* P_4, ushort** P_5, ushort** P_6, ushort** P_7, ushort** P_8, ushort** P_9);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EExportUserRatings(int P_0, EMediaTypes P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EImportSharedRatingsForUser(int P_0, EMediaTypes P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EScanAndClearDeletedMedia();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EDeleteFSFolder(int P_0, EMediaTypes P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EDeleteRootFolder(ushort* P_0, EMediaTypes P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EMarkAllDRMFilesAsNeedingLicenseRefresh();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002ECleanupTransientMedia();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EAddTransientMedia(ushort* P_0, EMediaTypes P_1, int* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateAlbumPropSet(_GUID P_0, ushort* P_1, ushort* P_2, IMSMediaSchemaPropertySet** P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECreateVideoPropSet(_GUID P_0, ushort* P_1, int P_2, IMSMediaSchemaPropertySet** P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EAddMedia(IMSMediaSchemaPropertySet* P_0, EMediaTypes P_1, int* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EAddMedia(ushort* P_0, EMediaTypes P_1, uint P_2, bool* P_3, int* P_4);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EAddGrovelerScanDirectory(ushort* P_0, EMediaTypes P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECanAddMedia(ushort* P_0, EMediaTypes P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002ECanAddFromFolder(ushort* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EGetCDDeviceList(IWMPCDDeviceList** P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int LoadStringW(HINSTANCE__* P_0, uint P_1, ushort* P_2, int P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HINSTANCE__* ZuneLibraryExports_002EGetLocResourceInstance();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EPhase2Initialization();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EStartupZuneNativeLib(ushort* P_0, int* P_1);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneEtwInit();

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneEtwShutdown();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EShutdownZuneNativeLib();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern int ZuneLibraryExports_002EStopGroveler([MarshalAs(UnmanagedType.U1)] bool P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern uint UnregisterTraceGuids(ulong P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint RegisterTraceGuidsW(delegate* unmanaged[Stdcall, Stdcall]<WMIDPREQUESTCODE, void*, uint*, void*, uint> P_0, void* P_1, _GUID* P_2, uint P_3, _TRACE_GUID_REGISTRATION* P_4, ushort* P_5, ushort* P_6, ulong* P_7);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern uint GetTraceEnableFlags(ulong P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern byte GetTraceEnableLevel(ulong P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern ulong GetTraceLoggerHandle(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern tagSAFEARRAY* SafeArrayCreate(ushort P_0, uint P_1, tagSAFEARRAYBOUND* P_2);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SafeArrayUnaccessData(tagSAFEARRAY* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int SafeArrayAccessData(tagSAFEARRAY* P_0, void** P_1);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern uint SafeArrayGetDim(tagSAFEARRAY* P_0);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int ZuneLibraryExports_002EZunePropVariantChangeType(tagPROPVARIANT* P_0, tagPROPVARIANT* P_1, ushort P_2, ushort P_3);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int MoveWindow(HWND__* P_0, int P_1, int P_2, int P_3, int P_4, int P_5);

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern HWND__* GetWindow(HWND__* P_0, uint P_1);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* _getFiberPtrId();

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _amsg_exit(int P_0);

	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void __security_init_cookie();

	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void Sleep(uint P_0);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _003CCrtImplementationDetails_003E_002EThrowModuleLoadException(string P_0, Exception P_1);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _003CCrtImplementationDetails_003E_002EThrowModuleLoadException(string P_0);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _003CCrtImplementationDetails_003E_002EDoDllLanguageSupportValidation();

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _003CCrtImplementationDetails_003E_002EThrowNestedModuleLoadException(Exception P_0, Exception P_1);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _003CCrtImplementationDetails_003E_002ERegisterModuleUninitializer(EventHandler P_0);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void _003CCrtImplementationDetails_003E_002EDoCallBackInDefaultDomain(delegate* unmanaged[Stdcall, Stdcall]<void*, int> P_0, void* P_1);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void _cexit();

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS* P_0);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public static extern void terminate();

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* _encode_pointer(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* _decode_pointer(void* P_0);

	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[SuppressUnmanagedCodeSecurity]
	public unsafe static extern void* _encoded_null();
}
