Public Interface IApplicationStateMachine
    Sub AddState(identifier As String, state As IApplicationState)
    Function GetState(identifier As String) As IApplicationState
    Property CurrentStateIdentifier As String
    ReadOnly Property CurrentState As IApplicationState
End Interface
