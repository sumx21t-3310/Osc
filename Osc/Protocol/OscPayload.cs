namespace Osc.Protocol;

public record OscPayload(string Address, OscArgument[] Arguments);