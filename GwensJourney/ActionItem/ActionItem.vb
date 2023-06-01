Friend Class ActionItem
    Implements IActionItem

    Private ReadOnly performAction As Action

    Friend Sub New(text As String, action As Action)
        DisplayText = text
        performAction = action
    End Sub

    Friend ReadOnly Property DisplayText As String Implements IActionItem.DisplayText

    Friend Sub Perform() Implements IActionItem.Perform
        performAction()
    End Sub
End Class
