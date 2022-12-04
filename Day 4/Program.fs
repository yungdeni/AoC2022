// For more information see https://aka.ms/fsharp-console-apps
open System.IO

let setsFromStrArray (strArr:string[]) = 
    Set.ofList [(int strArr[0]) .. (int strArr[1])]

let splitPairs (line:string) =
    line.Split(",")

let parseSets (strArr: string[]) = 
    let first = strArr[0].Split("-") |> setsFromStrArray
    let second = strArr[1].Split("-") |> setsFromStrArray
    first, second

let checkSubsets sets = 
    Set.isSubset (fst sets) (snd sets) || Set.isSuperset (fst sets) (snd sets)

let checkIntersect sets = 
    let intersect = Set.toList (Set.intersect (fst sets) (snd sets))
    match intersect with
    | [] -> false
    |_ -> true

[<EntryPoint>]
let main argv = 
    let input = 
        File.ReadAllLines(__SOURCE_DIRECTORY__ + "/input.txt")
        |> Array.map splitPairs
        |> Array.map parseSets
    let part1 = 
        input
        |> Array.map checkSubsets
        |> Array.map (fun x -> if x = true then 1 else 0)
        |> Array.sum
    let part2 = 
        input
        |> Array.map checkIntersect
        |> Array.map (fun x -> if x = true then 1 else 0)
        |> Array.sum
    printfn "Part1: %i" part1
    printfn "Part2: %i" part2
    0