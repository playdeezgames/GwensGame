Public Interface IActionItems
    Function Clear() As IActionItems
    Function Add(text As String, action As Action(Of IEngine, IContext)) As IActionItems
    Function Choose(context As IContext) As Boolean
    Function HasAny() As Boolean
End Interface
