Friend Class ActionItem
    Implements IActionItem

    Private ReadOnly performAction As Action(Of IEngine, IContext)
    Private ReadOnly engine As IEngine

    Friend Sub New(engine As IEngine, text As String, action As Action(Of IEngine, IContext))
        DisplayText = text
        performAction = action
        Me.engine = engine
    End Sub

    Friend ReadOnly Property DisplayText As String Implements IActionItem.DisplayText

    Friend Sub Perform(context As IContext) Implements IActionItem.Perform
        performAction(engine, context)
    End Sub
End Class
