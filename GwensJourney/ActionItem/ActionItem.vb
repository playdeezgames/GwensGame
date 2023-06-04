Friend Class ActionItem
    Implements IActionItem

    Private ReadOnly performAction As Action(Of IEngine)
    Private ReadOnly engine As IEngine

    Friend Sub New(engine As IEngine, text As String, action As Action(Of IEngine))
        DisplayText = text
        performAction = action
        Me.engine = engine
    End Sub

    Friend ReadOnly Property DisplayText As String Implements IActionItem.DisplayText

    Friend Sub Perform() Implements IActionItem.Perform
        performAction(Engine)
    End Sub
End Class
