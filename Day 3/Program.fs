// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO

let charMapping = 
    let vals = [1..52]
    let chars = ['a'..'z'] @ ['A'..'Z']
    List.zip chars vals
    |> Map.ofList

let convertCharsToValues char = 
    charMapping[char]

let splitCompartments list = 
    List.splitInto 2 list

let splitGroups list = 
    List.chunkBySize 3 list


let returnSetsFromList (listofList: int list list) = 
    let rec inner (listofList: int list list) sets = 
        match listofList with
        | head::tail -> inner tail sets @ [Set.ofList head]
        | [] -> sets
    Seq.ofList (inner listofList [])

let findDupe listoflist =
    let sets = returnSetsFromList listoflist
    let dupe = Set.intersectMany sets
    List.exactlyOne (Set.toList dupe)
    
[<EntryPoint>]
let main argv = 
    let input = 
        File.ReadAllLines(__SOURCE_DIRECTORY__ + "/input.txt")
        |> Array.toList
        |> List.map (Seq.map convertCharsToValues >> List.ofSeq)
    let part1 = 
        input
        |> List.map splitCompartments
        |> List.map findDupe
        |> List.reduce (+)
    let part2 = 
        input
        |> splitGroups
        |> List.map findDupe
        |> List.reduce (+)
    printfn "Part1: %i" part1
    printfn "Part2: %i" part2
    0