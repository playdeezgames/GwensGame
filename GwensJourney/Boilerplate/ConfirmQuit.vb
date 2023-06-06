Module ConfirmQuit
    Sub Run(engine As IEngine, context As IContext)
        engine.Prompts.Clear()
        engine.Prompts.Add("Are you sure you want to quit?")
        engine.ActionItems.Clear()
        engine.ActionItems.Add("No", AddressOf MainMenu.Run)
        engine.ActionItems.Add("Yes", AddressOf QuitGame.Run)
    End Sub
End Module
