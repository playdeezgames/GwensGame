Friend Class ChangePaceState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        Dim avatar = Engine.World.Avatar
        With FrameBuffer
            .WriteLine(,, Black)

            .Write("Current Pace: ", Gray)
            .WriteLine(GetPaceName(avatar.Pace), GetPaceColor(avatar.Pace))

            .WriteLine("Change Pace:", Brown)
            For paceIndex = MinimumPace To MaximumPace
                .Write($"{paceIndex}. ", Gray)
                .WriteLine(GetPaceName(paceIndex), GetPaceColor(paceIndex))
            Next
            .WriteLine("0. Go Back", Gray)
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(Neutral)
            Case MinimumPace To MaximumPace
                Engine.World.Avatar.SetPace(choice)
                GoToState(Neutral)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
