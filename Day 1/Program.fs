open System
open System.IO

let splitNewlineAndReturn (inp:string) = 
    inp.Split([|"\r\n\r\n"|], StringSplitOptions.None)

let splitNewline (inp: string) = 
     inp.Split([|"\r\n"|], StringSplitOptions.None)


[<EntryPoint>]
let main argv = 
    let input = 
        File.ReadAllText(__SOURCE_DIRECTORY__ + "/input.txt")
        |> splitNewlineAndReturn
        |> Array.map splitNewline
        |> Array.map (Array.map int)
        |> Array.map (Array.reduce (+))
        |> Array.sort
        |> Array.rev
    let part1 = input[0]
    let part2 = Array.take 3 input |> Array.sum
    printfn "Part1: %i" part1
    printfn "Part2: %i" part2
    0 // Integer exit code