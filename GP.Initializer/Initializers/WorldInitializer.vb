Public Module WorldInitializer
    Public Sub Initialize(world As IWorld)
        world.Avatar = GwenInitializer.Initialize(world)
        With world.Avatar
            .GenerateForage()
            .GenerateShortcut()
        End With

    End Sub
End Module
