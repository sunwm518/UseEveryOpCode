using AsmResolver.DotNet;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.DotNet.Signatures;
using AsmResolver.DotNet.Signatures.Types;
using AsmResolver.PE.DotNet.Cil;
using AsmResolver.PE.DotNet.Metadata.Tables.Rows;

namespace UseEveryOpCode.OpCodes;
using static AsmResolver.PE.DotNet.Cil.CilOpCodes;

public class LdcI4OpCode : IOpCode
{
    private readonly CilOpCode _opCode;

    public LdcI4OpCode(CilOpCode opCode)
    {
        _opCode = opCode;
    }

    public IList<CilInstruction> CallingInstructions => new List<CilInstruction>();
    public MethodDefinition? Generate(TypeDefinition typeDefinition)
    {
        var method = new MethodDefinition(_opCode.ToString().Replace(".","_"), MethodAttributes.Public | MethodAttributes.Static,
            new MethodSignature(CallingConventionAttributes.Default, typeDefinition.Module!.CorLibTypeFactory.Void,
                Enumerable.Empty<TypeSignature>()));
        method.CilMethodBody = new CilMethodBody(method)
        {
            Instructions =
            {
                {_opCode,0},
                {Pop},
                {Ret}
            }
        };
        return method;
    }
}