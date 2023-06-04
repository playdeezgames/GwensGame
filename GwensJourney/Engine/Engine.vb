Public Class Engine
    Implements IEngine
    Sub New()
        Prompts = New Prompts()
        ActionItems = New ActionItems(Me)
    End Sub
    Public ReadOnly Property Prompts As IPrompts Implements IEngine.Prompts
    Public ReadOnly Property ActionItems As IActionItems Implements IEngine.ActionItems
End Class
