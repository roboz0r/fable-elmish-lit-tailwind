module App

open Fable.Core
open Fable.Core.JsInterop
open Browser

open Router

type Model =
    {
        Route: SiteRoute
        Counter: Counter.Model
    }

[<RequireQualifiedAccess>]
type Msg =
    | NavigateTo of SiteRoute
    | Counter of Counter.Msg

open Elmish
open Elmish.Navigation

let setTitle route =
    document.title <- SiteRoute.getNavTitle route

let update (msg: Msg) (model: Model) =
    console.log ($"update: {msg}")

    match msg with
    | Msg.NavigateTo route ->
        let url = SiteRoute.getUrl route

        let newModel =
            setTitle route
            { model with Route = route }

        newModel, Navigation.newUrl url

    | Msg.Counter msg ->
        let newModel = Counter.update msg model.Counter
        { model with Counter = newModel }, Cmd.none

open Lit
open Lit.Elmish

let hmr = HMR.createToken ()

let homeView (model: Model) (dispatch: Msg -> unit) =
    html
        $"""
    <div class="flex flex-col items-center">
        <h1 class="text-4xl font-bold">Fable Elmish Lit Tailwind Starter</h1>
        <p class="text-xl">This is a starter template for Fable Elmish Lit Tailwind projects.</p>
        <div class="flex flex-row items-center">
            <ul class="list-disc list-inside">
                <li class="text-xl"><a href="https://fable.io/" target="_blank" class="link">Fable</a></li>
                <li class="text-xl"><a href="https://elmish.github.io/elmish/" target="_blank" class="link">Elmish</a></li> 
                <li class="text-xl"><a href="https://fable.io/Fable.Lit/" target="_blank" class="link">Lit</a></li>
                <li class="text-xl"><a href="https://tailwindcss.com/" target="_blank" class="link">Tailwind</a>
                    + <a href="https://daisyui.com/" target="_blank" class="link">DaisyUI</a></li>
            </ul>
            <button class="btn btn-primary text-xl" @click={Ev(fun _ -> dispatch (Msg.NavigateTo(SiteRoute.Counter)))}>Go to counter</button>
        </div>
    </div>
    """

[<HookComponent>]
let view (model: Model) (dispatch: Msg -> unit) =
    Hook.useHmr (hmr)

    html
        $"""
    <div data-theme="dark" class="h-screen"> 
        <div class="navbar bg-base-100">
            <div class="navbar-start">
                <button class="text-xl" @click={Ev(fun _ -> dispatch (Msg.NavigateTo(SiteRoute.Home)))}>Fable Elmish Lit Tailwind Starter</button>
            </div>
            <div class="navbar-center">
                <button class="btn btn-ghost text-xl" @click={Ev(fun _ -> dispatch (Msg.NavigateTo(SiteRoute.Home)))}>Home</button>
                <button class="btn btn-ghost text-xl" @click={Ev(fun _ -> dispatch (Msg.NavigateTo(SiteRoute.Counter)))}>Counter</button>
            </div>
        </div>
        {match model.Route with
         | SiteRoute.Home -> homeView model dispatch
         | SiteRoute.Counter -> Counter.view model.Counter (Msg.Counter >> dispatch)}
    </div>
    """

let initialState route =
    match route with
    | Some route ->
        {
            Route = route
            Counter = Counter.init ()
        },
        Cmd.none
    | None ->
        // Redirect to home if route is invalid
        {
            Route = SiteRoute.Home
            Counter = Counter.init ()
        },
        Navigation.newUrl "."

let urlUpdate (result: SiteRoute option) (model: Model) =
    match result with
    | Some x -> { model with Route = x }, Cmd.none
    | None -> { model with Route = SiteRoute.Home }, Navigation.newUrl "/"

let navParser: Navigation.Parser<SiteRoute option> =
    fun location ->
        location.pathname.Split('/')
        |> List.ofArray
        |> List.tail // Path starts with '/' so there is always an empty first element
        |> SiteRoute.tryParseRoute

Program.mkProgram initialState update view
|> Program.toNavigable navParser urlUpdate
|> Program.withLit "app" // This is the id of the root element in index.html
|> Program.run
