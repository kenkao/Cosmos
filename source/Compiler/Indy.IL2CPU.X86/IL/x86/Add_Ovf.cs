//using System;
//using System.IO;


//using CPU = Indy.IL2CPU.Assembler.X86;

//namespace Indy.IL2CPU.IL.X86 {
//    [OpCode(OpCodeEnum.Add_Ovf)]
//    public class Add_Ovf: Op {
//        private string mNextLabel;
//        private string mCurLabel;
//        private uint mCurOffset;
//        private MethodInformation mMethodInformation;
//        public Add_Ovf(ILReader aReader, MethodInformation aMethodInfo)
//            : base(aReader, aMethodInfo) {
//             mMethodInformation = aMethodInfo;
//            mCurOffset = aReader.Position;
//            mCurLabel = IL.Op.GetInstructionLabel(aReader);
//            mNextLabel = IL.Op.GetInstructionLabel(aReader.NextPosition);
//        }
//        public override void DoAssemble() {
//            EmitNotImplementedException(Assembler, GetServiceProvider(), "Add_Ovf not yet implemented", mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
//            //AddWithOverflow(Assembler, true);
//        }
//    }
//}