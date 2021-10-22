Public Class ActionItem
    Implements IActionItem

    Private performAction As Action

    Public Sub New(text As String, action As Action)
        DisplayText = text
        performAction = action
    End Sub

    Public ReadOnly Property DisplayText As Object Implements IActionItem.DisplayText

    Public Sub Perform() Implements IActionItem.Perform
        performAction()
    End Sub
End Class
