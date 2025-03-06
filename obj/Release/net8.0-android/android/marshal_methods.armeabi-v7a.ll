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
	i32 42639949, ; 3: System.Threading.Thread => 0x28aa24d => 132
	i32 67008169, ; 4: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 33
	i32 72070932, ; 5: Microsoft.Maui.Graphics.dll => 0x44bb714 => 52
	i32 117431740, ; 6: System.Runtime.InteropServices => 0x6ffddbc => 122
	i32 122350210, ; 7: System.Threading.Channels.dll => 0x74aea82 => 131
	i32 159306688, ; 8: System.ComponentModel.Annotations => 0x97ed3c0 => 91
	i32 165246403, ; 9: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 59
	i32 182336117, ; 10: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 78
	i32 195452805, ; 11: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 30
	i32 199333315, ; 12: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 31
	i32 205061960, ; 13: System.ComponentModel => 0xc38ff48 => 94
	i32 280992041, ; 14: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 2
	i32 317674968, ; 15: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 30
	i32 318968648, ; 16: Xamarin.AndroidX.Activity.dll => 0x13031348 => 55
	i32 336156722, ; 17: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 15
	i32 342366114, ; 18: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 66
	i32 356389973, ; 19: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 14
	i32 367780167, ; 20: System.IO.Pipes => 0x15ebe147 => 106
	i32 374914964, ; 21: System.Transactions.Local => 0x1658bf94 => 134
	i32 379916513, ; 22: System.Threading.Thread.dll => 0x16a510e1 => 132
	i32 385762202, ; 23: System.Memory.dll => 0x16fe439a => 110
	i32 395744057, ; 24: _Microsoft.Android.Resource.Designer => 0x17969339 => 34
	i32 424102203, ; 25: AppGarcomSys.dll => 0x1947493b => 85
	i32 435591531, ; 26: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 26
	i32 442565967, ; 27: System.Collections => 0x1a61054f => 90
	i32 450948140, ; 28: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 65
	i32 469710990, ; 29: System.dll => 0x1bff388e => 136
	i32 498788369, ; 30: System.ObjectModel => 0x1dbae811 => 119
	i32 500358224, ; 31: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 13
	i32 503918385, ; 32: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 7
	i32 513247710, ; 33: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 47
	i32 530272170, ; 34: System.Linq.Queryable => 0x1f9b4faa => 108
	i32 539058512, ; 35: Microsoft.Extensions.Logging => 0x20216150 => 44
	i32 592146354, ; 36: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 21
	i32 627609679, ; 37: Xamarin.AndroidX.CustomView => 0x2568904f => 63
	i32 627931235, ; 38: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 19
	i32 662205335, ; 39: System.Text.Encodings.Web.dll => 0x27787397 => 128
	i32 672442732, ; 40: System.Collections.Concurrent => 0x2814a96c => 86
	i32 683518922, ; 41: System.Net.Security => 0x28bdabca => 116
	i32 688181140, ; 42: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 1
	i32 706645707, ; 43: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 16
	i32 709557578, ; 44: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 4
	i32 722857257, ; 45: System.Runtime.Loader.dll => 0x2b15ed29 => 123
	i32 759454413, ; 46: System.Net.Requests => 0x2d445acd => 115
	i32 762598435, ; 47: System.IO.Pipes.dll => 0x2d745423 => 106
	i32 775507847, ; 48: System.IO.Compression => 0x2e394f87 => 105
	i32 777317022, ; 49: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 25
	i32 789151979, ; 50: Microsoft.Extensions.Options => 0x2f0980eb => 46
	i32 804715423, ; 51: System.Data.Common => 0x2ff6fb9f => 96
	i32 823281589, ; 52: System.Private.Uri.dll => 0x311247b5 => 120
	i32 830298997, ; 53: System.IO.Compression.Brotli => 0x317d5b75 => 104
	i32 904024072, ; 54: System.ComponentModel.Primitives.dll => 0x35e25008 => 92
	i32 926902833, ; 55: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 28
	i32 966729478, ; 56: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 82
	i32 967690846, ; 57: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 66
	i32 975236339, ; 58: System.Diagnostics.Tracing => 0x3a20ecf3 => 100
	i32 992768348, ; 59: System.Collections.dll => 0x3b2c715c => 90
	i32 1012816738, ; 60: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 76
	i32 1019214401, ; 61: System.Drawing => 0x3cbffa41 => 102
	i32 1028951442, ; 62: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 43
	i32 1029334545, ; 63: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 3
	i32 1035644815, ; 64: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 56
	i32 1036536393, ; 65: System.Drawing.Primitives.dll => 0x3dc84a49 => 101
	i32 1044663988, ; 66: System.Linq.Expressions.dll => 0x3e444eb4 => 107
	i32 1052210849, ; 67: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 68
	i32 1082857460, ; 68: System.ComponentModel.TypeConverter => 0x408b17f4 => 93
	i32 1084122840, ; 69: Xamarin.Kotlin.StdLib => 0x409e66d8 => 83
	i32 1098259244, ; 70: System => 0x41761b2c => 136
	i32 1118262833, ; 71: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 16
	i32 1157931901, ; 72: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 36
	i32 1168523401, ; 73: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 22
	i32 1178241025, ; 74: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 73
	i32 1202000627, ; 75: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 36
	i32 1203215381, ; 76: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 20
	i32 1204575371, ; 77: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 39
	i32 1208641965, ; 78: System.Diagnostics.Process => 0x480a69ad => 98
	i32 1234928153, ; 79: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 18
	i32 1260983243, ; 80: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 2
	i32 1293217323, ; 81: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 64
	i32 1324164729, ; 82: System.Linq => 0x4eed2679 => 109
	i32 1373134921, ; 83: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 32
	i32 1376866003, ; 84: Xamarin.AndroidX.SavedState => 0x52114ed3 => 76
	i32 1406073936, ; 85: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 60
	i32 1408764838, ; 86: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 125
	i32 1430672901, ; 87: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 0
	i32 1435222561, ; 88: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 82
	i32 1452070440, ; 89: System.Formats.Asn1.dll => 0x568cd628 => 103
	i32 1458022317, ; 90: System.Net.Security.dll => 0x56e7a7ad => 116
	i32 1461004990, ; 91: es\Microsoft.Maui.Controls.resources => 0x57152abe => 6
	i32 1461234159, ; 92: System.Collections.Immutable.dll => 0x5718a9ef => 87
	i32 1462112819, ; 93: System.IO.Compression.dll => 0x57261233 => 105
	i32 1469204771, ; 94: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 57
	i32 1470490898, ; 95: Microsoft.Extensions.Primitives => 0x57a5e912 => 47
	i32 1479771757, ; 96: System.Collections.Immutable => 0x5833866d => 87
	i32 1480492111, ; 97: System.IO.Compression.Brotli.dll => 0x583e844f => 104
	i32 1493001747, ; 98: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 10
	i32 1514721132, ; 99: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 5
	i32 1543031311, ; 100: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 130
	i32 1551623176, ; 101: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 25
	i32 1622152042, ; 102: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 70
	i32 1624863272, ; 103: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 80
	i32 1636350590, ; 104: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 62
	i32 1639515021, ; 105: System.Net.Http.dll => 0x61b9038d => 111
	i32 1639986890, ; 106: System.Text.RegularExpressions => 0x61c036ca => 130
	i32 1657153582, ; 107: System.Runtime => 0x62c6282e => 126
	i32 1658251792, ; 108: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 81
	i32 1677501392, ; 109: System.Net.Primitives.dll => 0x63fca3d0 => 114
	i32 1679769178, ; 110: System.Security.Cryptography => 0x641f3e5a => 127
	i32 1689493916, ; 111: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 35
	i32 1729485958, ; 112: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 58
	i32 1736233607, ; 113: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 23
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
	i32 1842015223, ; 129: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 29
	i32 1853025655, ; 130: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 26
	i32 1858542181, ; 131: System.Linq.Expressions => 0x6ec71a65 => 107
	i32 1875935024, ; 132: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 8
	i32 1910275211, ; 133: System.Collections.NonGeneric.dll => 0x71dc7c8b => 88
	i32 1961813231, ; 134: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 77
	i32 1968388702, ; 135: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 40
	i32 2003115576, ; 136: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 5
	i32 2019465201, ; 137: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 68
	i32 2025202353, ; 138: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 0
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
	i32 2270573516, ; 157: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 8
	i32 2279755925, ; 158: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 75
	i32 2294913272, ; 159: Npgsql => 0x88c998f8 => 53
	i32 2295906218, ; 160: System.Net.Sockets => 0x88d8bfaa => 117
	i32 2303942373, ; 161: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 18
	i32 2305521784, ; 162: System.Private.CoreLib.dll => 0x896b7878 => 137
	i32 2334995809, ; 163: Npgsql.EntityFrameworkCore.PostgreSQL.dll => 0x8b2d3561 => 54
	i32 2353062107, ; 164: System.Net.Primitives => 0x8c40e0db => 114
	i32 2368005991, ; 165: System.Xml.ReaderWriter.dll => 0x8d24e767 => 135
	i32 2371007202, ; 166: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 40
	i32 2395872292, ; 167: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 13
	i32 2427813419, ; 168: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 10
	i32 2435356389, ; 169: System.Console.dll => 0x912896e5 => 95
	i32 2458678730, ; 170: System.Net.Sockets.dll => 0x928c75ca => 117
	i32 2475788418, ; 171: Java.Interop.dll => 0x93918882 => 138
	i32 2480646305, ; 172: Microsoft.Maui.Controls => 0x93dba8a1 => 48
	i32 2550873716, ; 173: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 11
	i32 2570120770, ; 174: System.Text.Encodings.Web => 0x9930ee42 => 128
	i32 2593496499, ; 175: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 20
	i32 2605712449, ; 176: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 84
	i32 2617129537, ; 177: System.Private.Xml.dll => 0x9bfe3a41 => 121
	i32 2620871830, ; 178: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 62
	i32 2626831493, ; 179: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 15
	i32 2634653062, ; 180: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 37
	i32 2663698177, ; 181: System.Runtime.Loader => 0x9ec4cf01 => 123
	i32 2665622720, ; 182: System.Drawing.Primitives => 0x9ee22cc0 => 101
	i32 2676780864, ; 183: System.Data.Common.dll => 0x9f8c6f40 => 96
	i32 2724373263, ; 184: System.Runtime.Numerics.dll => 0xa262a30f => 124
	i32 2732626843, ; 185: Xamarin.AndroidX.Activity => 0xa2e0939b => 55
	i32 2735172069, ; 186: System.Threading.Channels => 0xa30769e5 => 131
	i32 2737747696, ; 187: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 57
	i32 2752995522, ; 188: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 21
	i32 2758225723, ; 189: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 49
	i32 2764765095, ; 190: Microsoft.Maui.dll => 0xa4caf7a7 => 50
	i32 2778768386, ; 191: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 79
	i32 2785988530, ; 192: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 27
	i32 2801831435, ; 193: Microsoft.Maui.Graphics => 0xa7008e0b => 52
	i32 2806116107, ; 194: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 6
	i32 2810250172, ; 195: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 60
	i32 2831556043, ; 196: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 19
	i32 2847789619, ; 197: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 37
	i32 2853208004, ; 198: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 79
	i32 2861189240, ; 199: Microsoft.Maui.Essentials => 0xaa8a4878 => 51
	i32 2909740682, ; 200: System.Private.CoreLib => 0xad6f1e8a => 137
	i32 2916838712, ; 201: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 80
	i32 2919462931, ; 202: System.Numerics.Vectors.dll => 0xae037813 => 118
	i32 2959614098, ; 203: System.ComponentModel.dll => 0xb0682092 => 94
	i32 2978675010, ; 204: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 64
	i32 2987532451, ; 205: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 77
	i32 3038032645, ; 206: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 34
	i32 3057625584, ; 207: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 71
	i32 3059408633, ; 208: Mono.Android.Runtime => 0xb65adef9 => 139
	i32 3059793426, ; 209: System.ComponentModel.Primitives => 0xb660be12 => 92
	i32 3069363400, ; 210: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 38
	i32 3077302341, ; 211: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 12
	i32 3103600923, ; 212: System.Formats.Asn1 => 0xb8fd311b => 103
	i32 3147165239, ; 213: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 100
	i32 3178803400, ; 214: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 72
	i32 3195844289, ; 215: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 38
	i32 3220365878, ; 216: System.Threading => 0xbff2e236 => 133
	i32 3258312781, ; 217: Xamarin.AndroidX.CardView => 0xc235e84d => 58
	i32 3265493905, ; 218: System.Linq.Queryable.dll => 0xc2a37b91 => 108
	i32 3280506390, ; 219: System.ComponentModel.Annotations.dll => 0xc3888e16 => 91
	i32 3305363605, ; 220: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 7
	i32 3316684772, ; 221: System.Net.Requests.dll => 0xc5b097e4 => 115
	i32 3317135071, ; 222: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 63
	i32 3346324047, ; 223: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 73
	i32 3357674450, ; 224: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 24
	i32 3358260929, ; 225: System.Text.Json => 0xc82afec1 => 129
	i32 3362522851, ; 226: Xamarin.AndroidX.Core => 0xc86c06e3 => 61
	i32 3366347497, ; 227: Java.Interop => 0xc8a662e9 => 138
	i32 3374999561, ; 228: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 75
	i32 3381016424, ; 229: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 3
	i32 3428513518, ; 230: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 42
	i32 3463511458, ; 231: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 11
	i32 3471940407, ; 232: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 93
	i32 3476120550, ; 233: Mono.Android => 0xcf3163e6 => 140
	i32 3479583265, ; 234: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 24
	i32 3484440000, ; 235: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 23
	i32 3485117614, ; 236: System.Text.Json.dll => 0xcfbaacae => 129
	i32 3580758918, ; 237: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 31
	i32 3608519521, ; 238: System.Linq.dll => 0xd715a361 => 109
	i32 3641597786, ; 239: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 67
	i32 3643446276, ; 240: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 28
	i32 3643854240, ; 241: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 72
	i32 3657292374, ; 242: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 41
	i32 3660523487, ; 243: System.Net.NetworkInformation => 0xda2f27df => 113
	i32 3672681054, ; 244: Mono.Android.dll => 0xdae8aa5e => 140
	i32 3697841164, ; 245: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 33
	i32 3724971120, ; 246: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 71
	i32 3732100267, ; 247: System.Net.NameResolution => 0xde7354ab => 112
	i32 3748608112, ; 248: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 97
	i32 3786282454, ; 249: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 59
	i32 3792276235, ; 250: System.Collections.NonGeneric => 0xe2098b0b => 88
	i32 3802395368, ; 251: System.Collections.Specialized.dll => 0xe2a3f2e8 => 89
	i32 3823082795, ; 252: System.Security.Cryptography.dll => 0xe3df9d2b => 127
	i32 3841636137, ; 253: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 43
	i32 3849253459, ; 254: System.Runtime.InteropServices.dll => 0xe56ef253 => 122
	i32 3889960447, ; 255: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 32
	i32 3896106733, ; 256: System.Collections.Concurrent.dll => 0xe839deed => 86
	i32 3896760992, ; 257: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 61
	i32 3928044579, ; 258: System.Xml.ReaderWriter => 0xea213423 => 135
	i32 3931092270, ; 259: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 74
	i32 3955647286, ; 260: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 56
	i32 3980434154, ; 261: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 27
	i32 3987592930, ; 262: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 9
	i32 4003436829, ; 263: System.Diagnostics.Process.dll => 0xee9f991d => 98
	i32 4025784931, ; 264: System.Memory => 0xeff49a63 => 110
	i32 4046471985, ; 265: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 49
	i32 4073602200, ; 266: System.Threading.dll => 0xf2ce3c98 => 133
	i32 4094352644, ; 267: Microsoft.Maui.Essentials.dll => 0xf40add04 => 51
	i32 4099507663, ; 268: System.Drawing.dll => 0xf45985cf => 102
	i32 4100113165, ; 269: System.Private.Uri => 0xf462c30d => 120
	i32 4101236366, ; 270: Npgsql.EntityFrameworkCore.PostgreSQL => 0xf473e68e => 54
	i32 4101842092, ; 271: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 39
	i32 4102112229, ; 272: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 22
	i32 4116429668, ; 273: AppGarcomSys => 0xf55bbb64 => 85
	i32 4125707920, ; 274: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 17
	i32 4126470640, ; 275: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 42
	i32 4150914736, ; 276: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 29
	i32 4182413190, ; 277: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 69
	i32 4213026141, ; 278: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 97
	i32 4271975918, ; 279: Microsoft.Maui.Controls.dll => 0xfea12dee => 48
	i32 4274976490, ; 280: System.Runtime.Numerics => 0xfecef6ea => 124
	i32 4292120959 ; 281: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 69
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [282 x i32] [
	i32 113, ; 0
	i32 112, ; 1
	i32 53, ; 2
	i32 132, ; 3
	i32 33, ; 4
	i32 52, ; 5
	i32 122, ; 6
	i32 131, ; 7
	i32 91, ; 8
	i32 59, ; 9
	i32 78, ; 10
	i32 30, ; 11
	i32 31, ; 12
	i32 94, ; 13
	i32 2, ; 14
	i32 30, ; 15
	i32 55, ; 16
	i32 15, ; 17
	i32 66, ; 18
	i32 14, ; 19
	i32 106, ; 20
	i32 134, ; 21
	i32 132, ; 22
	i32 110, ; 23
	i32 34, ; 24
	i32 85, ; 25
	i32 26, ; 26
	i32 90, ; 27
	i32 65, ; 28
	i32 136, ; 29
	i32 119, ; 30
	i32 13, ; 31
	i32 7, ; 32
	i32 47, ; 33
	i32 108, ; 34
	i32 44, ; 35
	i32 21, ; 36
	i32 63, ; 37
	i32 19, ; 38
	i32 128, ; 39
	i32 86, ; 40
	i32 116, ; 41
	i32 1, ; 42
	i32 16, ; 43
	i32 4, ; 44
	i32 123, ; 45
	i32 115, ; 46
	i32 106, ; 47
	i32 105, ; 48
	i32 25, ; 49
	i32 46, ; 50
	i32 96, ; 51
	i32 120, ; 52
	i32 104, ; 53
	i32 92, ; 54
	i32 28, ; 55
	i32 82, ; 56
	i32 66, ; 57
	i32 100, ; 58
	i32 90, ; 59
	i32 76, ; 60
	i32 102, ; 61
	i32 43, ; 62
	i32 3, ; 63
	i32 56, ; 64
	i32 101, ; 65
	i32 107, ; 66
	i32 68, ; 67
	i32 93, ; 68
	i32 83, ; 69
	i32 136, ; 70
	i32 16, ; 71
	i32 36, ; 72
	i32 22, ; 73
	i32 73, ; 74
	i32 36, ; 75
	i32 20, ; 76
	i32 39, ; 77
	i32 98, ; 78
	i32 18, ; 79
	i32 2, ; 80
	i32 64, ; 81
	i32 109, ; 82
	i32 32, ; 83
	i32 76, ; 84
	i32 60, ; 85
	i32 125, ; 86
	i32 0, ; 87
	i32 82, ; 88
	i32 103, ; 89
	i32 116, ; 90
	i32 6, ; 91
	i32 87, ; 92
	i32 105, ; 93
	i32 57, ; 94
	i32 47, ; 95
	i32 87, ; 96
	i32 104, ; 97
	i32 10, ; 98
	i32 5, ; 99
	i32 130, ; 100
	i32 25, ; 101
	i32 70, ; 102
	i32 80, ; 103
	i32 62, ; 104
	i32 111, ; 105
	i32 130, ; 106
	i32 126, ; 107
	i32 81, ; 108
	i32 114, ; 109
	i32 127, ; 110
	i32 35, ; 111
	i32 58, ; 112
	i32 23, ; 113
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
	i32 29, ; 129
	i32 26, ; 130
	i32 107, ; 131
	i32 8, ; 132
	i32 88, ; 133
	i32 77, ; 134
	i32 40, ; 135
	i32 5, ; 136
	i32 68, ; 137
	i32 0, ; 138
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
	i32 8, ; 157
	i32 75, ; 158
	i32 53, ; 159
	i32 117, ; 160
	i32 18, ; 161
	i32 137, ; 162
	i32 54, ; 163
	i32 114, ; 164
	i32 135, ; 165
	i32 40, ; 166
	i32 13, ; 167
	i32 10, ; 168
	i32 95, ; 169
	i32 117, ; 170
	i32 138, ; 171
	i32 48, ; 172
	i32 11, ; 173
	i32 128, ; 174
	i32 20, ; 175
	i32 84, ; 176
	i32 121, ; 177
	i32 62, ; 178
	i32 15, ; 179
	i32 37, ; 180
	i32 123, ; 181
	i32 101, ; 182
	i32 96, ; 183
	i32 124, ; 184
	i32 55, ; 185
	i32 131, ; 186
	i32 57, ; 187
	i32 21, ; 188
	i32 49, ; 189
	i32 50, ; 190
	i32 79, ; 191
	i32 27, ; 192
	i32 52, ; 193
	i32 6, ; 194
	i32 60, ; 195
	i32 19, ; 196
	i32 37, ; 197
	i32 79, ; 198
	i32 51, ; 199
	i32 137, ; 200
	i32 80, ; 201
	i32 118, ; 202
	i32 94, ; 203
	i32 64, ; 204
	i32 77, ; 205
	i32 34, ; 206
	i32 71, ; 207
	i32 139, ; 208
	i32 92, ; 209
	i32 38, ; 210
	i32 12, ; 211
	i32 103, ; 212
	i32 100, ; 213
	i32 72, ; 214
	i32 38, ; 215
	i32 133, ; 216
	i32 58, ; 217
	i32 108, ; 218
	i32 91, ; 219
	i32 7, ; 220
	i32 115, ; 221
	i32 63, ; 222
	i32 73, ; 223
	i32 24, ; 224
	i32 129, ; 225
	i32 61, ; 226
	i32 138, ; 227
	i32 75, ; 228
	i32 3, ; 229
	i32 42, ; 230
	i32 11, ; 231
	i32 93, ; 232
	i32 140, ; 233
	i32 24, ; 234
	i32 23, ; 235
	i32 129, ; 236
	i32 31, ; 237
	i32 109, ; 238
	i32 67, ; 239
	i32 28, ; 240
	i32 72, ; 241
	i32 41, ; 242
	i32 113, ; 243
	i32 140, ; 244
	i32 33, ; 245
	i32 71, ; 246
	i32 112, ; 247
	i32 97, ; 248
	i32 59, ; 249
	i32 88, ; 250
	i32 89, ; 251
	i32 127, ; 252
	i32 43, ; 253
	i32 122, ; 254
	i32 32, ; 255
	i32 86, ; 256
	i32 61, ; 257
	i32 135, ; 258
	i32 74, ; 259
	i32 56, ; 260
	i32 27, ; 261
	i32 9, ; 262
	i32 98, ; 263
	i32 110, ; 264
	i32 49, ; 265
	i32 133, ; 266
	i32 51, ; 267
	i32 102, ; 268
	i32 120, ; 269
	i32 54, ; 270
	i32 39, ; 271
	i32 22, ; 272
	i32 85, ; 273
	i32 17, ; 274
	i32 42, ; 275
	i32 29, ; 276
	i32 69, ; 277
	i32 97, ; 278
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
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ df9aaf29a52042a4fbf800daf2f3a38964b9e958"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
