Friend Module GameOver
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add($"[red]Yer dead![/]")
        engine.ActionItems.Clear()
        engine.ActionItems.Add("That sucks!", AddressOf MainMenu.Run)
    End Sub
End Module
