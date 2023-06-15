Friend Class LoadState
    Inherits BaseState

    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()
            .ForegroundColor = Brown
            .WriteLine("Load From:")
            .ForegroundColor = Gray
            Dim index = 1
            For Each slot In SaveSlots
                If File.Exists(slot.Item2) Then
                    .WriteLine($"{index}. {slot.Item1}")
                End If
                index += 1
            Next
            .WriteLine("0. Never Mind")
        End With
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(MainMenu)
            Case 1 To 5
                Dim filename = SaveSlots(choice - 1).Item2
                If Not File.Exists(filename) Then
                    Return False
                End If
                Engine.World = World.Load(filename)
                With _frameBuffer
                    .BackgroundColor = Black
                    .WriteLine()
                    If Engine.World IsNot Nothing Then
                        .ForegroundColor = LightGreen
                        .WriteLine($"Loaded game from {SaveSlots(choice - 1).Item1}!")
                        GoToState(Neutral)
                    Else
                        .ForegroundColor = Red
                        .WriteLine($"Failed to load game from {SaveSlots(choice - 1).Item1}!")
                    End If
                End With
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
