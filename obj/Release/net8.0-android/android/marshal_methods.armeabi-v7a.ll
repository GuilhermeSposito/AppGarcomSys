; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [141 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [282 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 113
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 112
	i32 28873261, ; 2: Npgsql.dll => 0x1b8922d => 53
	i32 38948123, ; 3: ar\Microsoft.Maui.Controls.resources.dll => 0x2524d1b => 0
	i32 42244203, ; 4: he\Microsoft.Maui.Controls.resources.dll => 0x284986b => 9
	i32 42639949, ; 5: System.Threading.Thread => 0x28aa24d => 132
	i32 67008169, ; 6: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 33
	i32 72070932, ; 7: Microsoft.Maui.Graphics.dll => 0x44bb714 => 52
	i32 83839681, ; 8: ms\Microsoft.Maui.Controls.resources.dll => 0x4ff4ac1 => 17
	i32 117431740, ; 9: System.Runtime.InteropServices => 0x6ffddbc => 122
	i32 122350210, ; 10: System.Threading.Channels.dll => 0x74aea82 => 131
	i32 136584136, ; 11: zh-Hans\Microsoft.Maui.Controls.resources.dll => 0x8241bc8 => 32
	i32 140062828, ; 12: sk\Microsoft.Maui.Controls.resources.dll => 0x859306c => 25
	i32 159306688, ; 13: System.ComponentModel.Annotations => 0x97ed3c0 => 91
	i32 165246403, ; 14: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 59
	i32 182336117, ; 15: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 78
	i32 205061960, ; 16: System.ComponentModel => 0xc38ff48 => 94
	i32 317674968, ; 17: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 30
	i32 318968648, ; 18: Xamarin.AndroidX.Activity.dll => 0x13031348 => 55
	i32 321963286, ; 19: fr\Microsoft.Maui.Controls.resources.dll => 0x1330c516 => 8
	i32 342366114, ; 20: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 66
	i32 367780167, ; 21: System.IO.Pipes => 0x15ebe147 => 106
	i32 374914964, ; 22: System.Transactions.Local => 0x1658bf94 => 134
	i32 379916513, ; 23: System.Threading.Thread.dll => 0x16a510e1 => 132
	i32 385762202, ; 24: System.Memory.dll => 0x16fe439a => 110
	i32 395744057, ; 25: _Microsoft.Android.Resource.Designer => 0x17969339 => 34
	i32 409257351, ; 26: tr\Microsoft.Maui.Controls.resources.dll => 0x1864c587 => 28
	i32 424102203, ; 27: AppGarcomSys.dll => 0x1947493b => 85
	i32 442565967, ; 28: System.Collections => 0x1a61054f => 90
	i32 450948140, ; 29: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 65
	i32 469710990, ; 30: System.dll => 0x1bff388e => 136
	i32 489220957, ; 31: es\Microsoft.Maui.Controls.resources.dll => 0x1d28eb5d => 6
	i32 498788369, ; 32: System.ObjectModel => 0x1dbae811 => 119
	i32 513247710, ; 33: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 47
	i32 530272170, ; 34: System.Linq.Queryable => 0x1f9b4faa => 108
	i32 538707440, ; 35: th\Microsoft.Maui.Controls.resources.dll => 0x201c05f0 => 27
	i32 539058512, ; 36: Microsoft.Extensions.Logging => 0x20216150 => 44
	i32 627609679, ; 37: Xamarin.AndroidX.CustomView => 0x2568904f => 63
	i32 627931235, ; 38: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 19
	i32 662205335, ; 39: System.Text.Encodings.Web.dll => 0x27787397 => 128
	i32 672442732, ; 40: System.Collections.Concurrent => 0x2814a96c => 86
	i32 683518922, ; 41: System.Net.Security => 0x28bdabca => 116
	i32 722857257, ; 42: System.Runtime.Loader.dll => 0x2b15ed29 => 123
	i32 759454413, ; 43: System.Net.Requests => 0x2d445acd => 115
	i32 762598435, ; 44: System.IO.Pipes.dll => 0x2d745423 => 106
	i32 775507847, ; 45: System.IO.Compression => 0x2e394f87 => 105
	i32 777317022, ; 46: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 25
	i32 789151979, ; 47: Microsoft.Extensions.Options => 0x2f0980eb => 46
	i32 804715423, ; 48: System.Data.Common => 0x2ff6fb9f => 96
	i32 823281589, ; 49: System.Private.Uri.dll => 0x311247b5 => 120
	i32 830298997, ; 50: System.IO.Compression.Brotli => 0x317d5b75 => 104
	i32 869139383, ; 51: hi\Microsoft.Maui.Controls.resources.dll => 0x33ce03b7 => 10
	i32 880668424, ; 52: ru\Microsoft.Maui.Controls.resources.dll => 0x347def08 => 24
	i32 904024072, ; 53: System.ComponentModel.Primitives.dll => 0x35e25008 => 92
	i32 918734561, ; 54: pt-BR\Microsoft.Maui.Controls.resources.dll => 0x36c2c6e1 => 21
	i32 961460050, ; 55: it\Microsoft.Maui.Controls.resources.dll => 0x394eb752 => 14
	i32 966729478, ; 56: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 82
	i32 967690846, ; 57: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 66
	i32 975236339, ; 58: System.Diagnostics.Tracing => 0x3a20ecf3 => 100
	i32 990727110, ; 59: ro\Microsoft.Maui.Controls.resources.dll => 0x3b0d4bc6 => 23
	i32 992768348, ; 60: System.Collections.dll => 0x3b2c715c => 90
	i32 1012816738, ; 61: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 76
	i32 1019214401, ; 62: System.Drawing => 0x3cbffa41 => 102
	i32 1028951442, ; 63: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 43
	i32 1035644815, ; 64: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 56
	i32 1036536393, ; 65: System.Drawing.Primitives.dll => 0x3dc84a49 => 101
	i32 1043950537, ; 66: de\Microsoft.Maui.Controls.resources.dll => 0x3e396bc9 => 4
	i32 1044663988, ; 67: System.Linq.Expressions.dll => 0x3e444eb4 => 107
	i32 1052210849, ; 68: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 68
	i32 1082857460, ; 69: System.ComponentModel.TypeConverter => 0x408b17f4 => 93
	i32 1084122840, ; 70: Xamarin.Kotlin.StdLib => 0x409e66d8 => 83
	i32 1098259244, ; 71: System => 0x41761b2c => 136
	i32 1108272742, ; 72: sv\Microsoft.Maui.Controls.resources.dll => 0x420ee666 => 26
	i32 1117529484, ; 73: pl\Microsoft.Maui.Controls.resources.dll => 0x429c258c => 20
	i32 1118262833, ; 74: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 16
	i32 1157931901, ; 75: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 36
	i32 1168523401, ; 76: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 22
	i32 1178241025, ; 77: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 73
	i32 1202000627, ; 78: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 36
	i32 1204575371, ; 79: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 39
	i32 1208641965, ; 80: System.Diagnostics.Process => 0x480a69ad => 98
	i32 1260983243, ; 81: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 2
	i32 1293217323, ; 82: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 64
	i32 1308624726, ; 83: hr\Microsoft.Maui.Controls.resources.dll => 0x4e000756 => 11
	i32 1324164729, ; 84: System.Linq => 0x4eed2679 => 109
	i32 1336711579, ; 85: zh-HK\Microsoft.Maui.Controls.resources.dll => 0x4fac999b => 31
	i32 1373134921, ; 86: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 32
	i32 1376866003, ; 87: Xamarin.AndroidX.SavedState => 0x52114ed3 => 76
	i32 1406073936, ; 88: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 60
	i32 1408764838, ; 89: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 125
	i32 1430672901, ; 90: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 0
	i32 1435222561, ; 91: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 82
	i32 1452070440, ; 92: System.Formats.Asn1.dll => 0x568cd628 => 103
	i32 1458022317, ; 93: System.Net.Security.dll => 0x56e7a7ad => 116
	i32 1461004990, ; 94: es\Microsoft.Maui.Controls.resources => 0x57152abe => 6
	i32 1461234159, ; 95: System.Collections.Immutable.dll => 0x5718a9ef => 87
	i32 1462112819, ; 96: System.IO.Compression.dll => 0x57261233 => 105
	i32 1469204771, ; 97: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 57
	i32 1470490898, ; 98: Microsoft.Extensions.Primitives => 0x57a5e912 => 47
	i32 1479771757, ; 99: System.Collections.Immutable => 0x5833866d => 87
	i32 1480492111, ; 100: System.IO.Compression.Brotli.dll => 0x583e844f => 104
	i32 1526286932, ; 101: vi\Microsoft.Maui.Controls.resources.dll => 0x5af94a54 => 30
	i32 1543031311, ; 102: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 130
	i32 1622152042, ; 103: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 70
	i32 1624863272, ; 104: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 80
	i32 1636350590, ; 105: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 62
	i32 1639515021, ; 106: System.Net.Http.dll => 0x61b9038d => 111
	i32 1639986890, ; 107: System.Text.RegularExpressions => 0x61c036ca => 130
	i32 1657153582, ; 108: System.Runtime => 0x62c6282e => 126
	i32 1658251792, ; 109: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 81
	i32 1677501392, ; 110: System.Net.Primitives.dll => 0x63fca3d0 => 114
	i32 1679769178, ; 111: System.Security.Cryptography => 0x641f3e5a => 127
	i32 1689493916, ; 112: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 35
	i32 1729485958, ; 113: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 58
	i32 1743415430, ; 114: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 1
	i32 1744735666, ; 115: System.Transactions.Local.dll => 0x67fe8db2 => 134
	i32 1763938596, ; 116: System.Diagnostics.TraceSource.dll => 0x69239124 => 99
	i32 1766324549, ; 117: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 78
	i32 1770582343, ; 118: Microsoft.Extensions.Logging.dll => 0x6988f147 => 44
	i32 1780572499, ; 119: Mono.Android.Runtime.dll => 0x6a216153 => 139
	i32 1782862114, ; 120: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 17
	i32 1788241197, ; 121: Xamarin.AndroidX.Fragment => 0x6a96652d => 65
	i32 1793755602, ; 122: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 9
	i32 1808609942, ; 123: Xamarin.AndroidX.Loader => 0x6bcd3296 => 70
	i32 1813058853, ; 124: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 83
	i32 1813201214, ; 125: Xamarin.Google.Android.Material => 0x6c13413e => 81
	i32 1818569960, ; 126: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 74
	i32 1824722060, ; 127: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 125
	i32 1828688058, ; 128: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 45
	i32 1853025655, ; 129: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 26
	i32 1858542181, ; 130: System.Linq.Expressions => 0x6ec71a65 => 107
	i32 1875935024, ; 131: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 8
	i32 1893218855, ; 132: cs\Microsoft.Maui.Controls.resources.dll => 0x70d83a27 => 2
	i32 1910275211, ; 133: System.Collections.NonGeneric.dll => 0x71dc7c8b => 88
	i32 1953182387, ; 134: id\Microsoft.Maui.Controls.resources.dll => 0x746b32b3 => 13
	i32 1961813231, ; 135: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 77
	i32 1968388702, ; 136: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 40
	i32 2003115576, ; 137: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 5
	i32 2019465201, ; 138: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 68
	i32 2045470958, ; 139: System.Private.Xml => 0x79eb68ee => 121
	i32 2055257422, ; 140: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 67
	i32 2066184531, ; 141: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 4
	i32 2070888862, ; 142: System.Diagnostics.TraceSource => 0x7b6f419e => 99
	i32 2079903147, ; 143: System.Runtime.dll => 0x7bf8cdab => 126
	i32 2090596640, ; 144: System.Numerics.Vectors => 0x7c9bf920 => 118
	i32 2127167465, ; 145: System.Console => 0x7ec9ffe9 => 95
	i32 2142473426, ; 146: System.Collections.Specialized => 0x7fb38cd2 => 89
	i32 2159891885, ; 147: Microsoft.Maui => 0x80bd55ad => 50
	i32 2169148018, ; 148: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 12
	i32 2181898931, ; 149: Microsoft.Extensions.Options.dll => 0x820d22b3 => 46
	i32 2192057212, ; 150: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 45
	i32 2193016926, ; 151: System.ObjectModel.dll => 0x82b6c85e => 119
	i32 2201107256, ; 152: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 84
	i32 2201231467, ; 153: System.Net.Http => 0x8334206b => 111
	i32 2207618523, ; 154: it\Microsoft.Maui.Controls.resources => 0x839595db => 14
	i32 2252897993, ; 155: Microsoft.EntityFrameworkCore => 0x86487ec9 => 35
	i32 2266799131, ; 156: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 41
	i32 2279755925, ; 157: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 75
	i32 2294913272, ; 158: Npgsql => 0x88c998f8 => 53
	i32 2295906218, ; 159: System.Net.Sockets => 0x88d8bfaa => 117
	i32 2303942373, ; 160: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 18
	i32 2305521784, ; 161: System.Private.CoreLib.dll => 0x896b7878 => 137
	i32 2334995809, ; 162: Npgsql.EntityFrameworkCore.PostgreSQL.dll => 0x8b2d3561 => 54
	i32 2353062107, ; 163: System.Net.Primitives => 0x8c40e0db => 114
	i32 2366048013, ; 164: hu\Microsoft.Maui.Controls.resources.dll => 0x8d07070d => 12
	i32 2368005991, ; 165: System.Xml.ReaderWriter.dll => 0x8d24e767 => 135
	i32 2371007202, ; 166: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 40
	i32 2395872292, ; 167: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 13
	i32 2427813419, ; 168: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 10
	i32 2435356389, ; 169: System.Console.dll => 0x912896e5 => 95
	i32 2458678730, ; 170: System.Net.Sockets.dll => 0x928c75ca => 117
	i32 2475788418, ; 171: Java.Interop.dll => 0x93918882 => 138
	i32 2480646305, ; 172: Microsoft.Maui.Controls => 0x93dba8a1 => 48
	i32 2503351294, ; 173: ko\Microsoft.Maui.Controls.resources.dll => 0x95361bfe => 16
	i32 2550873716, ; 174: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 11
	i32 2570120770, ; 175: System.Text.Encodings.Web => 0x9930ee42 => 128
	i32 2576534780, ; 176: ja\Microsoft.Maui.Controls.resources.dll => 0x9992ccfc => 15
	i32 2593496499, ; 177: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 20
	i32 2605712449, ; 178: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 84
	i32 2617129537, ; 179: System.Private.Xml.dll => 0x9bfe3a41 => 121
	i32 2620871830, ; 180: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 62
	i32 2626831493, ; 181: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 15
	i32 2634653062, ; 182: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 37
	i32 2663698177, ; 183: System.Runtime.Loader => 0x9ec4cf01 => 123
	i32 2665622720, ; 184: System.Drawing.Primitives => 0x9ee22cc0 => 101
	i32 2676780864, ; 185: System.Data.Common.dll => 0x9f8c6f40 => 96
	i32 2724373263, ; 186: System.Runtime.Numerics.dll => 0xa262a30f => 124
	i32 2732626843, ; 187: Xamarin.AndroidX.Activity => 0xa2e0939b => 55
	i32 2735172069, ; 188: System.Threading.Channels => 0xa30769e5 => 131
	i32 2737747696, ; 189: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 57
	i32 2740698338, ; 190: ca\Microsoft.Maui.Controls.resources.dll => 0xa35bbce2 => 1
	i32 2752995522, ; 191: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 21
	i32 2758225723, ; 192: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 49
	i32 2764765095, ; 193: Microsoft.Maui.dll => 0xa4caf7a7 => 50
	i32 2778768386, ; 194: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 79
	i32 2785988530, ; 195: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 27
	i32 2801831435, ; 196: Microsoft.Maui.Graphics => 0xa7008e0b => 52
	i32 2810250172, ; 197: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 60
	i32 2847789619, ; 198: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 37
	i32 2853208004, ; 199: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 79
	i32 2861189240, ; 200: Microsoft.Maui.Essentials => 0xaa8a4878 => 51
	i32 2909740682, ; 201: System.Private.CoreLib => 0xad6f1e8a => 137
	i32 2916838712, ; 202: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 80
	i32 2919462931, ; 203: System.Numerics.Vectors.dll => 0xae037813 => 118
	i32 2959614098, ; 204: System.ComponentModel.dll => 0xb0682092 => 94
	i32 2978675010, ; 205: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 64
	i32 2987532451, ; 206: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 77
	i32 3038032645, ; 207: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 34
	i32 3053864966, ; 208: fi\Microsoft.Maui.Controls.resources.dll => 0xb6064806 => 7
	i32 3057625584, ; 209: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 71
	i32 3059408633, ; 210: Mono.Android.Runtime => 0xb65adef9 => 139
	i32 3059793426, ; 211: System.ComponentModel.Primitives => 0xb660be12 => 92
	i32 3069363400, ; 212: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 38
	i32 3103600923, ; 213: System.Formats.Asn1 => 0xb8fd311b => 103
	i32 3147165239, ; 214: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 100
	i32 3178803400, ; 215: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 72
	i32 3195844289, ; 216: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 38
	i32 3220365878, ; 217: System.Threading => 0xbff2e236 => 133
	i32 3258312781, ; 218: Xamarin.AndroidX.CardView => 0xc235e84d => 58
	i32 3265493905, ; 219: System.Linq.Queryable.dll => 0xc2a37b91 => 108
	i32 3280506390, ; 220: System.ComponentModel.Annotations.dll => 0xc3888e16 => 91
	i32 3305363605, ; 221: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 7
	i32 3316684772, ; 222: System.Net.Requests.dll => 0xc5b097e4 => 115
	i32 3317135071, ; 223: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 63
	i32 3346324047, ; 224: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 73
	i32 3357674450, ; 225: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 24
	i32 3358260929, ; 226: System.Text.Json => 0xc82afec1 => 129
	i32 3362522851, ; 227: Xamarin.AndroidX.Core => 0xc86c06e3 => 61
	i32 3366347497, ; 228: Java.Interop => 0xc8a662e9 => 138
	i32 3374999561, ; 229: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 75
	i32 3381016424, ; 230: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 3
	i32 3428513518, ; 231: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 42
	i32 3458724246, ; 232: pt\Microsoft.Maui.Controls.resources.dll => 0xce27f196 => 22
	i32 3471940407, ; 233: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 93
	i32 3476120550, ; 234: Mono.Android => 0xcf3163e6 => 140
	i32 3484440000, ; 235: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 23
	i32 3485117614, ; 236: System.Text.Json.dll => 0xcfbaacae => 129
	i32 3580758918, ; 237: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 31
	i32 3592228221, ; 238: zh-Hant\Microsoft.Maui.Controls.resources.dll => 0xd61d0d7d => 33
	i32 3608519521, ; 239: System.Linq.dll => 0xd715a361 => 109
	i32 3641597786, ; 240: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 67
	i32 3643446276, ; 241: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 28
	i32 3643854240, ; 242: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 72
	i32 3657292374, ; 243: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 41
	i32 3660523487, ; 244: System.Net.NetworkInformation => 0xda2f27df => 113
	i32 3672681054, ; 245: Mono.Android.dll => 0xdae8aa5e => 140
	i32 3724971120, ; 246: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 71
	i32 3732100267, ; 247: System.Net.NameResolution => 0xde7354ab => 112
	i32 3748608112, ; 248: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 97
	i32 3751619990, ; 249: da\Microsoft.Maui.Controls.resources.dll => 0xdf9d2d96 => 3
	i32 3786282454, ; 250: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 59
	i32 3792276235, ; 251: System.Collections.NonGeneric => 0xe2098b0b => 88
	i32 3802395368, ; 252: System.Collections.Specialized.dll => 0xe2a3f2e8 => 89
	i32 3823082795, ; 253: System.Security.Cryptography.dll => 0xe3df9d2b => 127
	i32 3841636137, ; 254: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 43
	i32 3849253459, ; 255: System.Runtime.InteropServices.dll => 0xe56ef253 => 122
	i32 3896106733, ; 256: System.Collections.Concurrent.dll => 0xe839deed => 86
	i32 3896760992, ; 257: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 61
	i32 3920221145, ; 258: nl\Microsoft.Maui.Controls.resources.dll => 0xe9a9d3d9 => 19
	i32 3928044579, ; 259: System.Xml.ReaderWriter => 0xea213423 => 135
	i32 3931092270, ; 260: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 74
	i32 3955647286, ; 261: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 56
	i32 4003436829, ; 262: System.Diagnostics.Process.dll => 0xee9f991d => 98
	i32 4025784931, ; 263: System.Memory => 0xeff49a63 => 110
	i32 4046471985, ; 264: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 49
	i32 4073602200, ; 265: System.Threading.dll => 0xf2ce3c98 => 133
	i32 4091086043, ; 266: el\Microsoft.Maui.Controls.resources.dll => 0xf3d904db => 5
	i32 4094352644, ; 267: Microsoft.Maui.Essentials.dll => 0xf40add04 => 51
	i32 4099507663, ; 268: System.Drawing.dll => 0xf45985cf => 102
	i32 4100113165, ; 269: System.Private.Uri => 0xf462c30d => 120
	i32 4101236366, ; 270: Npgsql.EntityFrameworkCore.PostgreSQL => 0xf473e68e => 54
	i32 4101842092, ; 271: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 39
	i32 4103439459, ; 272: uk\Microsoft.Maui.Controls.resources.dll => 0xf4958463 => 29
	i32 4116429668, ; 273: AppGarcomSys => 0xf55bbb64 => 85
	i32 4126470640, ; 274: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 42
	i32 4150914736, ; 275: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 29
	i32 4182413190, ; 276: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 69
	i32 4213026141, ; 277: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 97
	i32 4249188766, ; 278: nb\Microsoft.Maui.Controls.resources.dll => 0xfd45799e => 18
	i32 4271975918, ; 279: Microsoft.Maui.Controls.dll => 0xfea12dee => 48
	i32 4274976490, ; 280: System.Runtime.Numerics => 0xfecef6ea => 124
	i32 4292120959 ; 281: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 69
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [282 x i32] [
	i32 113, ; 0
	i32 112, ; 1
	i32 53, ; 2
	i32 0, ; 3
	i32 9, ; 4
	i32 132, ; 5
	i32 33, ; 6
	i32 52, ; 7
	i32 17, ; 8
	i32 122, ; 9
	i32 131, ; 10
	i32 32, ; 11
	i32 25, ; 12
	i32 91, ; 13
	i32 59, ; 14
	i32 78, ; 15
	i32 94, ; 16
	i32 30, ; 17
	i32 55, ; 18
	i32 8, ; 19
	i32 66, ; 20
	i32 106, ; 21
	i32 134, ; 22
	i32 132, ; 23
	i32 110, ; 24
	i32 34, ; 25
	i32 28, ; 26
	i32 85, ; 27
	i32 90, ; 28
	i32 65, ; 29
	i32 136, ; 30
	i32 6, ; 31
	i32 119, ; 32
	i32 47, ; 33
	i32 108, ; 34
	i32 27, ; 35
	i32 44, ; 36
	i32 63, ; 37
	i32 19, ; 38
	i32 128, ; 39
	i32 86, ; 40
	i32 116, ; 41
	i32 123, ; 42
	i32 115, ; 43
	i32 106, ; 44
	i32 105, ; 45
	i32 25, ; 46
	i32 46, ; 47
	i32 96, ; 48
	i32 120, ; 49
	i32 104, ; 50
	i32 10, ; 51
	i32 24, ; 52
	i32 92, ; 53
	i32 21, ; 54
	i32 14, ; 55
	i32 82, ; 56
	i32 66, ; 57
	i32 100, ; 58
	i32 23, ; 59
	i32 90, ; 60
	i32 76, ; 61
	i32 102, ; 62
	i32 43, ; 63
	i32 56, ; 64
	i32 101, ; 65
	i32 4, ; 66
	i32 107, ; 67
	i32 68, ; 68
	i32 93, ; 69
	i32 83, ; 70
	i32 136, ; 71
	i32 26, ; 72
	i32 20, ; 73
	i32 16, ; 74
	i32 36, ; 75
	i32 22, ; 76
	i32 73, ; 77
	i32 36, ; 78
	i32 39, ; 79
	i32 98, ; 80
	i32 2, ; 81
	i32 64, ; 82
	i32 11, ; 83
	i32 109, ; 84
	i32 31, ; 85
	i32 32, ; 86
	i32 76, ; 87
	i32 60, ; 88
	i32 125, ; 89
	i32 0, ; 90
	i32 82, ; 91
	i32 103, ; 92
	i32 116, ; 93
	i32 6, ; 94
	i32 87, ; 95
	i32 105, ; 96
	i32 57, ; 97
	i32 47, ; 98
	i32 87, ; 99
	i32 104, ; 100
	i32 30, ; 101
	i32 130, ; 102
	i32 70, ; 103
	i32 80, ; 104
	i32 62, ; 105
	i32 111, ; 106
	i32 130, ; 107
	i32 126, ; 108
	i32 81, ; 109
	i32 114, ; 110
	i32 127, ; 111
	i32 35, ; 112
	i32 58, ; 113
	i32 1, ; 114
	i32 134, ; 115
	i32 99, ; 116
	i32 78, ; 117
	i32 44, ; 118
	i32 139, ; 119
	i32 17, ; 120
	i32 65, ; 121
	i32 9, ; 122
	i32 70, ; 123
	i32 83, ; 124
	i32 81, ; 125
	i32 74, ; 126
	i32 125, ; 127
	i32 45, ; 128
	i32 26, ; 129
	i32 107, ; 130
	i32 8, ; 131
	i32 2, ; 132
	i32 88, ; 133
	i32 13, ; 134
	i32 77, ; 135
	i32 40, ; 136
	i32 5, ; 137
	i32 68, ; 138
	i32 121, ; 139
	i32 67, ; 140
	i32 4, ; 141
	i32 99, ; 142
	i32 126, ; 143
	i32 118, ; 144
	i32 95, ; 145
	i32 89, ; 146
	i32 50, ; 147
	i32 12, ; 148
	i32 46, ; 149
	i32 45, ; 150
	i32 119, ; 151
	i32 84, ; 152
	i32 111, ; 153
	i32 14, ; 154
	i32 35, ; 155
	i32 41, ; 156
	i32 75, ; 157
	i32 53, ; 158
	i32 117, ; 159
	i32 18, ; 160
	i32 137, ; 161
	i32 54, ; 162
	i32 114, ; 163
	i32 12, ; 164
	i32 135, ; 165
	i32 40, ; 166
	i32 13, ; 167
	i32 10, ; 168
	i32 95, ; 169
	i32 117, ; 170
	i32 138, ; 171
	i32 48, ; 172
	i32 16, ; 173
	i32 11, ; 174
	i32 128, ; 175
	i32 15, ; 176
	i32 20, ; 177
	i32 84, ; 178
	i32 121, ; 179
	i32 62, ; 180
	i32 15, ; 181
	i32 37, ; 182
	i32 123, ; 183
	i32 101, ; 184
	i32 96, ; 185
	i32 124, ; 186
	i32 55, ; 187
	i32 131, ; 188
	i32 57, ; 189
	i32 1, ; 190
	i32 21, ; 191
	i32 49, ; 192
	i32 50, ; 193
	i32 79, ; 194
	i32 27, ; 195
	i32 52, ; 196
	i32 60, ; 197
	i32 37, ; 198
	i32 79, ; 199
	i32 51, ; 200
	i32 137, ; 201
	i32 80, ; 202
	i32 118, ; 203
	i32 94, ; 204
	i32 64, ; 205
	i32 77, ; 206
	i32 34, ; 207
	i32 7, ; 208
	i32 71, ; 209
	i32 139, ; 210
	i32 92, ; 211
	i32 38, ; 212
	i32 103, ; 213
	i32 100, ; 214
	i32 72, ; 215
	i32 38, ; 216
	i32 133, ; 217
	i32 58, ; 218
	i32 108, ; 219
	i32 91, ; 220
	i32 7, ; 221
	i32 115, ; 222
	i32 63, ; 223
	i32 73, ; 224
	i32 24, ; 225
	i32 129, ; 226
	i32 61, ; 227
	i32 138, ; 228
	i32 75, ; 229
	i32 3, ; 230
	i32 42, ; 231
	i32 22, ; 232
	i32 93, ; 233
	i32 140, ; 234
	i32 23, ; 235
	i32 129, ; 236
	i32 31, ; 237
	i32 33, ; 238
	i32 109, ; 239
	i32 67, ; 240
	i32 28, ; 241
	i32 72, ; 242
	i32 41, ; 243
	i32 113, ; 244
	i32 140, ; 245
	i32 71, ; 246
	i32 112, ; 247
	i32 97, ; 248
	i32 3, ; 249
	i32 59, ; 250
	i32 88, ; 251
	i32 89, ; 252
	i32 127, ; 253
	i32 43, ; 254
	i32 122, ; 255
	i32 86, ; 256
	i32 61, ; 257
	i32 19, ; 258
	i32 135, ; 259
	i32 74, ; 260
	i32 56, ; 261
	i32 98, ; 262
	i32 110, ; 263
	i32 49, ; 264
	i32 133, ; 265
	i32 5, ; 266
	i32 51, ; 267
	i32 102, ; 268
	i32 120, ; 269
	i32 54, ; 270
	i32 39, ; 271
	i32 29, ; 272
	i32 85, ; 273
	i32 42, ; 274
	i32 29, ; 275
	i32 69, ; 276
	i32 97, ; 277
	i32 18, ; 278
	i32 48, ; 279
	i32 124, ; 280
	i32 69 ; 281
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ af27162bee43b7fecdca59b4f67aa8c175cbc875"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
