Public Interface IActionItems
    Function Clear() As IActionItems
    Function Add(text As String, action As Action) As IActionItems
    Function Choose() As Boolean
    Function HasAny() As Boolean
End Interface
