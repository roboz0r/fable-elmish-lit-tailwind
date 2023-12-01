[<RequireQualifiedAccess>]
module Counter

type Msg =
    | Increment
    | Decrement

type Model = { Count: int }

let init () = { Count = 0 }

let update (msg: Msg) (model: Model) =
    match msg with
    | Increment -> { model with Count = model.Count + 1 }
    | Decrement -> { model with Count = model.Count - 1 }

open Lit
let hmr = HMR.createToken ()

[<HookComponent>]
let view (model: Model) dispatch =
    Hook.useHmr (hmr)

    html
        $"""
        <div class="flex flex-col items-center">
            <h1 class="text-4xl font-bold">Counter</h1>
            <div class="flex flex-row items-center">
                <button class="btn btn-primary text-xl" @click={Ev(fun _ -> dispatch Increment)}>+</button>
                <span class="text-4xl mx-4">{model.Count}</span>
                <button class="btn btn-primary text-xl" @click={Ev(fun _ -> dispatch Decrement)}>-</button>
            </div>
        </div>
    """
