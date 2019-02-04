using System;

namespace ELF
{
    public enum BitFormat : byte
    {
        BIT_32 = 1,
        BIT_64 = 2
    }

    public enum EndianFormat : byte
    {
        LITTLE_ENDIAN = 1,
        BIG_ENDIAN = 2
    }

    public enum TargetABI : byte
    {
        SYSTEM_V,
        HP_UX,
        NET_BSD,
        LINUX,
        GNU_HURD,
        SOLARIS,
        AIX,
        IRIX,
        FREE_BSD,
        TRU64,
        MODESTO,
        OPEN_BSD,
        OPEN_VMS,
        NONSTOP_KERNEL,
        AROS,
        FENIX_OS,
        CLOUD_ABI,
        SORTIX = 0x53
    }

    public enum BinaryType : ushort
    {
        NONE        = 0,
        RELOCATABLE = 1,
        EXECUTABLE  = 2,
        SHARED      = 3,
        CORE        = 4,
        LO_OS       = 0xFE00,
        HI_OS       = 0xFEFF,
        LO_PROC     = 0xFF00,
        HI_PROC     = 0xFFFF
    }

    public enum BinaryISA : short
    {
        DEFAULT  = 0,
        M32      = 1,
        SPARC    = 2,
        X86      = 3,
        M68K     = 4,
        M88K     = 5,
        MCU      = 6,
        X860     = 7,
        MIPS     = 8,
        S370     = 9,
        MIPS_RS3 = 10,
        SPARCV9  = 11,
        PARISC   = 15,
        VPP550   = 17,
        SPARC32  = 18,
        X960     = 19,
        POWERPC  = 20,
        PPC64    = 21,
        S390     = 22,
        SPU      = 23,
        V800     = 36,
        FR20     = 37,
        RH32     = 38,
        MCORE    = 39,
        ARM      = 40,
        ALPHA    = 41,
        SUPERH   = 42,
        SPARC_V9 = 43,
        TRICORE  = 44,
        ARC      = 45,
        H8_300   = 46,
        H8_300H  = 47,
        H8S      = 48,
        H8_500   = 49,
        IA64     = 50,
        MIPS_X   = 51,
        COLDFIRE = 52,
        M68HC12  = 53,
        MMA      = 54,
        PCP      = 55,
        NCPU     = 56,
        NDR1     = 57,
        STARFIRE = 58,
        ME16     = 59,
        ST100    = 60,
        TINYJ    = 61,
        AMD64    = 62,
        SONY_DSP = 63,
        PDP_10   = 64,
        PDP_11   = 65,
        FX66     = 66,
        ST9PLUS  = 67,
        ST7      = 68,
        VAX      = 75,
        PJAVA    = 91,
        AARCH64  = 183,
        CUDA     = 190,
        AMD_GPU  = 224,
        RISC_V   = 243
    }

    [Flags]
    public enum FlagsMIPS : uint
    {
        NOREORDER =          0x00000001,
        PIC =                0x00000002,
        CPIC =               0x00000004,
        XGOT =               0x00000008,
        UCODE =              0x00000010,
        ABI32 =              0x00000020,
        OPTIONS_FIRST =      0x00000080,
        MODE32ON64 =         0x00000100,
        FP64ON32 =           0x00000200,
        NAN2008 =            0x00000400,
        ABI =                0x0000F000,
        ABI_O32 =            0x00001000,
        ABI_O64 =            0x00002000,
        ABI_EABI32 =         0x00003000,
        ABI_EABI64 =         0x00004000,
        MACH =               0x00FF0000,
        MACH_3900 =          0x00810000,
        MACH_4010 =          0x00820000,
        MACH_4100 =          0x00830000,
        MACH_4650 =          0x00850000,
        MACH_4120 =          0x00870000,
        MACH_4111 =          0x00880000,
        MACH_SB1 =           0x008A0000,
        MACH_OCTEON =        0x008B0000,
        MACH_XLR =           0x008C0000,
        MACH_OCTEON2 =       0x008D0000,
        MACH_OCTEON3 =       0x008E0000,
        MACH_5400 =          0x00910000,
        MACH_5900 =          0x00920000,
        MACH_IAMR2 =         0x00930000,
        MACH_5500 =          0x00990000,
        MACH_LS2E =          0x00A00000,
        MACH_LS2F =          0x00A10000,
        MACH_LS3A =          0x00A20000,
        ARCH_ASE_MICROMIPS = 0x02000000,
        ARCH_ASE_M16 =       0x04000000,
        ARCH_ASE_MDMX =      0x08000000,
        ARCH_ASE =           0x0F000000,
        ARCH =               0xF0000000,
        ARCH_1 =             0x00000000,
        ARCH_2 =             0x10000000,
        ARCH_3 =             0x20000000,
        ARCH_4 =             0x30000000,
        ARCH_5 =             0x40000000,
        ARCH_32 =            0x50000000,
        ARCH_64 =            0x60000000,
        ARCH_32R2 =          0x70000000,
        ARCH_64R2 =          0x80000000,
        ARCH_32R6 =          0x90000000,
        ARCH_64R6 =          0xA0000000
    }

    public enum SegmentType
    {
        NULL    = 0x00000000,
        LOAD    = 0x00000001, /* loadable segment */
        DYNAMIC = 0x00000002, /* dynamically link info */
        INTERP  = 0x00000003, /* interpreter */
        NOTE    = 0x00000004, /* aux info */
        SHLIB   = 0x00000005, /* reserved */
        PHDR    = 0x00000006, /* header table */
        LO_OS   = 0x60000000,
        HI_OS   = 0x6FFFFFFF,
        LO_PROC = 0x70000000,
        HI_PROC = 0x7FFFFFFF
    }

    [Flags]
    public enum SegmentFlags
    {
        EXECUTABLE = 1,
        WRITABLE   = 2,
        READABLE   = 4
    }

    public enum SectionType
    {
        NULL          = 0,
        PROGBITS      = 1, /* program data */
        SYM_TABLE     = 2, /* link symbol table */
        STR_TABLE     = 3, /* string table */
        RELOC_ADD     = 4, /* relocation with addendums */
        HASH_TABLE    = 5, /* symbol hash table */
        DYNAMIC       = 6, /* dynamic link info */
        NOTE          = 7, /* aux info */
        NOBITS        = 8, /* header-only section */
        RELOC         = 9, /* relocation entries */
        SHARED        = 10, /* reserved; for shared libraries */
        DYN_SYM_TABLE = 11, /* dynamic linking symbol table */
        INIT_ARRAY    = 14, /* array of init function pointers */
        FINISH_ARRAY  = 15, /* array of finish function pointers */
        PREINIT_ARRAY = 16, /* array of pre-init function pointers */
        SECTION_GROUP = 17, /* section group */
        SYMTAB_SH_NDX = 18  /* indexes for SHN_XINDEX entries */
    }

    [Flags]
    public enum SectionFlags
    {
        WRITABLE = 0x1,
        ALLOCATED = 0x2,
        EXECUTABLE = 0x4,
        MERGEABLE = 0x10,
        STRINGS   = 0x20,
        INFO_LINK = 0x40,
        LINK_ORDER = 0x80,
        OS_NONCONFORMANT = 0x100,
        GROUP = 0x200,
        TLS = 0x400
    }

    public enum BindingType : byte
    {
        LOCAL = 0,
        GLOBAL = 1,
        WEAK = 2
    }

    public enum SymbolType : byte
    {
        NO_TYPE = 0,
        OBJECT = 1,
        FUNCTION = 2,
        SECTION = 3,
        FILE = 4
    }

    public enum RelocationType : ushort
    {
        MIPS_NONE = 0,
        MIPS_16 = 1,
        MIPS_32 = 2,
        MIPS_REL32 = 3,
        MIPS_26 = 4,
        MIPS_HI16 = 5,
        MIPS_LO16 = 6,
        MIPS_GPREL16 = 7,
        MIPS_LITERAL = 8,
        MIPS_GOT16 = 9,
        MIPS_PC16 = 10,
        MIPS_CALL16 = 11,
        MIPS_GPREL32 = 12
    }
}
