using System;

	public enum ElementType {
		Prompt = 1,
		Dialog = 2,
		Function = 3,
		LinkedFunctionCall = 4,
		BusinessVariable = 5,
		Script = 6,
		FunctionCall = 7,
		InternalFunction = 8,
		WebService = 9,
        ConfigurationVariable = 10,
        NotSupported = 11
	}

	public enum TextPartType {
		Text = 1,
		Variable = 2,
		Punctuation = 3,
		Dynamic = 4
	}

	public enum VariableType {
		Text = 1,
		Integer = 2,
		Number = 3,
		Boolean = 4,
        List = 5
	}

	public enum DialogType {
		AskFor = 1,
		Tell = 2,
		Record = 3
	}

    /// <summary>
    /// Enumeration to determine the status of the outbound recording call.
    /// </summary>
    public enum CallStatusType
    {
        NotInitiated,
        Initiated,
        Error,
        Success,
        Unknown
    }

    /// <summary>
    /// Enumeration to identify the location of the linked file.
    /// </summary>
    public enum LinkedFileLocation
    {
        Production,
        Development
    }
