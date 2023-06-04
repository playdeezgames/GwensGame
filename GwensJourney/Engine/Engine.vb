Public Class Engine
    Implements IEngine
    Sub New()
        Prompts = New Prompts()
    End Sub
    Public ReadOnly Property Prompts As IPrompts Implements IEngine.Prompts
End Class
