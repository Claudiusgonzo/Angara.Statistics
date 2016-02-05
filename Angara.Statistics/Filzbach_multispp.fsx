﻿#load "Scripts/load-project-debug.fsx"
open Angara.Statistics
open Angara.Filzbach

do
    // multispp

    let rng = MT19937()

    // fake data
    let numdata = 999
    let numsp = 10
    let trueparams = Array.init numsp (fun _ -> Uniform(1.0,10.0) |> draw rng)
    do
        printfn "%A" trueparams
        let s = summary trueparams
        printfn "mean = %g, std dev = %g" s.mean (sqrt s.variance)
    let fake_data = Array.init numdata (fun _ ->
        let spid = rng.uniform_int(numsp-1)
        spid, Poisson trueparams.[spid] |> draw rng)

    // parameters

    let parameters = Parameters()
                        .Add("lambda", numsp, 100.0, 0.01, 200.0, isLog=true)
                        .Add("lambda_mean", 100.0, 0.01, 200.0, isLog=true)
                        .Add("lambda_std", 0.2, 0.01, 2.0, isLog=true)

    // log-likelihood
    let multispp (data:(int*float) seq) (p:Parameters) =
        let lambda = p.GetValues "lambda"
        let lambda_mean = p.GetValue "lambda_mean"
        let lambda_std = p.GetValue "lambda_std"
        (data |> Seq.map (fun (spp,spcount) ->
            log_pdf (Poisson lambda.[spp]) spcount)
        |> Seq.sum)
        + 
        (lambda |> Seq.map (fun lam ->
            log_pdf (Normal(lambda_mean, lambda_std)) lam)
        |> Seq.sum)

    // runmcmc
    printfn "Initial log-likelihood = %g" (multispp fake_data parameters)
    Sampler.runmcmc(parameters, multispp fake_data, 5000, 50) |> Sampler.print

    let c_data =
                [|
                1, 4.000000
                0, 1.000000
                1, 3.000000
                0, 1.000000
                5, 6.000000
                6, 9.000000
                4, 9.000000
                2, 1.000000
                3, 3.000000
                9, 10.000000
                9, 8.000000
                2, 5.000000
                1, 5.000000
                9, 8.000000
                8, 9.000000
                2, 5.000000
                3, 8.000000
                3, 10.000000
                7, 8.000000
                1, 6.000000
                7, 11.000000
                0, 0.000000
                8, 4.000000
                1, 6.000000
                6, 4.000000
                6, 5.000000
                3, 7.000000
                9, 6.000000
                6, 8.000000
                5, 6.000000
                6, 4.000000
                7, 13.000000
                5, 4.000000
                7, 6.000000
                4, 5.000000
                7, 11.000000
                9, 6.000000
                2, 3.000000
                4, 9.000000
                7, 12.000000
                3, 6.000000
                6, 6.000000
                1, 4.000000
                6, 6.000000
                6, 5.000000
                1, 5.000000
                2, 1.000000
                8, 11.000000
                2, 2.000000
                0, 1.000000
                0, 0.000000
                8, 5.000000
                0, 0.000000
                9, 9.000000
                5, 7.000000
                8, 6.000000
                3, 2.000000
                6, 6.000000
                6, 4.000000
                8, 7.000000
                9, 7.000000
                5, 4.000000
                7, 6.000000
                7, 6.000000
                2, 4.000000
                7, 14.000000
                3, 4.000000
                3, 6.000000
                6, 5.000000
                9, 9.000000
                2, 4.000000
                8, 11.000000
                2, 3.000000
                5, 5.000000
                6, 1.000000
                0, 0.000000
                3, 7.000000
                6, 4.000000
                0, 3.000000
                5, 4.000000
                8, 5.000000
                4, 3.000000
                7, 11.000000
                0, 1.000000
                0, 0.000000
                4, 11.000000
                5, 5.000000
                2, 2.000000
                3, 16.000000
                1, 7.000000
                0, 2.000000
                3, 8.000000
                1, 6.000000
                2, 2.000000
                9, 4.000000
                0, 0.000000
                6, 3.000000
                5, 6.000000
                4, 6.000000
                7, 17.000000
                0, 2.000000
                0, 1.000000
                8, 4.000000
                9, 5.000000
                2, 3.000000
                8, 9.000000
                6, 3.000000
                4, 12.000000
                5, 11.000000
                5, 7.000000
                2, 3.000000
                6, 2.000000
                4, 4.000000
                1, 6.000000
                6, 4.000000
                0, 0.000000
                3, 12.000000
                9, 6.000000
                9, 3.000000
                5, 7.000000
                8, 8.000000
                9, 5.000000
                7, 6.000000
                4, 7.000000
                4, 8.000000
                5, 6.000000
                7, 5.000000
                7, 5.000000
                3, 9.000000
                1, 5.000000
                8, 11.000000
                1, 6.000000
                9, 12.000000
                0, 3.000000
                1, 7.000000
                0, 0.000000
                4, 7.000000
                7, 13.000000
                5, 4.000000
                9, 9.000000
                8, 11.000000
                5, 1.000000
                7, 6.000000
                6, 7.000000
                8, 6.000000
                3, 8.000000
                9, 8.000000
                2, 5.000000
                4, 7.000000
                2, 3.000000
                3, 7.000000
                9, 4.000000
                2, 2.000000
                0, 0.000000
                2, 4.000000
                2, 6.000000
                5, 7.000000
                4, 2.000000
                4, 2.000000
                2, 7.000000
                2, 1.000000
                0, 0.000000
                3, 7.000000
                5, 5.000000
                1, 9.000000
                1, 9.000000
                2, 5.000000
                1, 9.000000
                5, 6.000000
                3, 10.000000
                7, 5.000000
                6, 8.000000
                4, 3.000000
                5, 2.000000
                9, 8.000000
                0, 1.000000
                0, 0.000000
                3, 6.000000
                1, 5.000000
                2, 4.000000
                7, 9.000000
                2, 4.000000
                7, 8.000000
                3, 8.000000
                1, 4.000000
                4, 11.000000
                5, 5.000000
                6, 2.000000
                1, 4.000000
                9, 11.000000
                0, 1.000000
                6, 5.000000
                6, 1.000000
                1, 6.000000
                5, 8.000000
                3, 7.000000
                0, 0.000000
                4, 4.000000
                2, 4.000000
                5, 1.000000
                5, 6.000000
                3, 10.000000
                7, 9.000000
                3, 5.000000
                3, 10.000000
                9, 6.000000
                2, 5.000000
                9, 5.000000
                6, 4.000000
                0, 2.000000
                1, 7.000000
                8, 6.000000
                1, 11.000000
                0, 1.000000
                8, 11.000000
                8, 10.000000
                2, 2.000000
                8, 9.000000
                6, 5.000000
                8, 3.000000
                0, 1.000000
                5, 3.000000
                4, 7.000000
                2, 3.000000
                5, 5.000000
                5, 9.000000
                5, 5.000000
                7, 10.000000
                1, 2.000000
                4, 8.000000
                1, 4.000000
                0, 0.000000
                8, 6.000000
                4, 6.000000
                2, 4.000000
                6, 4.000000
                6, 2.000000
                1, 7.000000
                8, 12.000000
                2, 4.000000
                1, 5.000000
                3, 8.000000
                3, 9.000000
                7, 7.000000
                5, 5.000000
                3, 3.000000
                5, 8.000000
                3, 10.000000
                2, 4.000000
                6, 5.000000
                6, 5.000000
                6, 3.000000
                2, 10.000000
                7, 10.000000
                7, 8.000000
                3, 6.000000
                6, 7.000000
                7, 9.000000
                5, 6.000000
                4, 3.000000
                9, 2.000000
                6, 8.000000
                9, 11.000000
                8, 10.000000
                2, 3.000000
                2, 4.000000
                3, 15.000000
                2, 1.000000
                1, 8.000000
                2, 3.000000
                3, 8.000000
                7, 13.000000
                3, 7.000000
                0, 0.000000
                6, 5.000000
                3, 12.000000
                6, 5.000000
                8, 15.000000
                2, 2.000000
                2, 3.000000
                3, 8.000000
                1, 7.000000
                3, 3.000000
                3, 11.000000
                2, 0.000000
                7, 9.000000
                6, 6.000000
                2, 3.000000
                7, 13.000000
                0, 0.000000
                3, 10.000000
                3, 9.000000
                5, 5.000000
                8, 10.000000
                5, 8.000000
                9, 4.000000
                3, 9.000000
                5, 10.000000
                6, 10.000000
                6, 6.000000
                8, 11.000000
                8, 6.000000
                1, 5.000000
                0, 1.000000
                2, 4.000000
                8, 11.000000
                6, 4.000000
                4, 3.000000
                0, 1.000000
                9, 11.000000
                0, 1.000000
                3, 9.000000
                0, 2.000000
                2, 3.000000
                9, 14.000000
                3, 4.000000
                3, 3.000000
                1, 2.000000
                7, 11.000000
                6, 5.000000
                8, 6.000000
                9, 8.000000
                6, 2.000000
                3, 9.000000
                2, 1.000000
                9, 6.000000
                3, 4.000000
                5, 5.000000
                7, 6.000000
                6, 3.000000
                8, 6.000000
                2, 6.000000
                9, 6.000000
                8, 7.000000
                6, 2.000000
                2, 3.000000
                9, 5.000000
                5, 1.000000
                5, 3.000000
                4, 5.000000
                4, 6.000000
                9, 9.000000
                4, 4.000000
                4, 2.000000
                5, 13.000000
                3, 6.000000
                0, 1.000000
                0, 1.000000
                6, 6.000000
                0, 0.000000
                5, 8.000000
                6, 4.000000
                5, 5.000000
                0, 1.000000
                1, 6.000000
                5, 5.000000
                7, 9.000000
                7, 7.000000
                3, 12.000000
                2, 6.000000
                1, 6.000000
                2, 5.000000
                2, 1.000000
                5, 5.000000
                0, 2.000000
                4, 8.000000
                7, 19.000000
                5, 4.000000
                5, 7.000000
                7, 4.000000
                3, 10.000000
                1, 3.000000
                0, 2.000000
                5, 7.000000
                5, 4.000000
                0, 0.000000
                1, 8.000000
                0, 1.000000
                8, 6.000000
                4, 5.000000
                7, 4.000000
                2, 6.000000
                3, 10.000000
                4, 11.000000
                7, 5.000000
                3, 11.000000
                0, 0.000000
                6, 3.000000
                2, 3.000000
                6, 4.000000
                7, 11.000000
                0, 0.000000
                2, 1.000000
                7, 8.000000
                8, 11.000000
                2, 2.000000
                2, 7.000000
                6, 4.000000
                4, 5.000000
                3, 7.000000
                6, 3.000000
                8, 11.000000
                7, 8.000000
                4, 4.000000
                7, 15.000000
                9, 7.000000
                1, 3.000000
                3, 6.000000
                2, 3.000000
                7, 8.000000
                6, 4.000000
                5, 12.000000
                0, 2.000000
                7, 7.000000
                2, 3.000000
                9, 7.000000
                8, 7.000000
                3, 8.000000
                5, 5.000000
                6, 7.000000
                8, 9.000000
                8, 8.000000
                7, 14.000000
                1, 2.000000
                9, 9.000000
                7, 9.000000
                7, 9.000000
                8, 13.000000
                4, 5.000000
                6, 1.000000
                1, 5.000000
                0, 0.000000
                6, 4.000000
                1, 6.000000
                3, 10.000000
                3, 5.000000
                0, 4.000000
                8, 5.000000
                7, 8.000000
                5, 1.000000
                3, 5.000000
                4, 4.000000
                6, 7.000000
                1, 8.000000
                4, 5.000000
                0, 1.000000
                3, 7.000000
                4, 8.000000
                1, 8.000000
                0, 0.000000
                4, 7.000000
                8, 11.000000
                3, 6.000000
                5, 4.000000
                9, 6.000000
                8, 10.000000
                7, 8.000000
                2, 5.000000
                4, 7.000000
                2, 4.000000
                8, 10.000000
                8, 3.000000
                2, 5.000000
                3, 9.000000
                6, 4.000000
                9, 11.000000
                3, 9.000000
                5, 8.000000
                0, 2.000000
                9, 8.000000
                8, 5.000000
                6, 1.000000
                5, 9.000000
                2, 1.000000
                8, 13.000000
                4, 9.000000
                1, 4.000000
                1, 5.000000
                7, 9.000000
                4, 5.000000
                8, 6.000000
                3, 9.000000
                1, 9.000000
                8, 6.000000
                1, 5.000000
                5, 5.000000
                2, 3.000000
                5, 6.000000
                6, 2.000000
                9, 15.000000
                6, 4.000000
                1, 5.000000
                4, 2.000000
                9, 7.000000
                6, 4.000000
                0, 2.000000
                8, 8.000000
                0, 1.000000
                4, 12.000000
                9, 6.000000
                9, 6.000000
                2, 5.000000
                0, 0.000000
                9, 7.000000
                4, 5.000000
                9, 5.000000
                9, 9.000000
                1, 5.000000
                5, 5.000000
                2, 2.000000
                3, 5.000000
                7, 9.000000
                6, 4.000000
                4, 7.000000
                9, 10.000000
                2, 2.000000
                8, 8.000000
                4, 9.000000
                2, 4.000000
                4, 8.000000
                9, 5.000000
                2, 4.000000
                8, 6.000000
                0, 1.000000
                2, 1.000000
                1, 4.000000
                7, 9.000000
                5, 8.000000
                6, 4.000000
                7, 7.000000
                5, 3.000000
                6, 3.000000
                3, 15.000000
                5, 8.000000
                8, 6.000000
                8, 9.000000
                5, 2.000000
                5, 6.000000
                1, 4.000000
                6, 7.000000
                6, 5.000000
                7, 11.000000
                6, 7.000000
                9, 5.000000
                2, 2.000000
                6, 4.000000
                0, 0.000000
                9, 8.000000
                4, 3.000000
                2, 2.000000
                6, 7.000000
                3, 7.000000
                0, 0.000000
                6, 8.000000
                6, 3.000000
                7, 11.000000
                2, 3.000000
                9, 6.000000
                1, 6.000000
                8, 8.000000
                5, 7.000000
                9, 7.000000
                3, 6.000000
                3, 10.000000
                9, 8.000000
                8, 13.000000
                5, 6.000000
                6, 5.000000
                2, 3.000000
                6, 1.000000
                1, 3.000000
                2, 5.000000
                9, 6.000000
                0, 2.000000
                5, 4.000000
                6, 6.000000
                7, 10.000000
                2, 3.000000
                8, 9.000000
                4, 6.000000
                3, 11.000000
                4, 6.000000
                6, 8.000000
                2, 2.000000
                4, 5.000000
                7, 10.000000
                8, 9.000000
                1, 6.000000
                9, 7.000000
                9, 5.000000
                6, 6.000000
                8, 8.000000
                3, 8.000000
                9, 8.000000
                3, 9.000000
                8, 7.000000
                4, 3.000000
                7, 6.000000
                4, 8.000000
                9, 12.000000
                8, 13.000000
                0, 0.000000
                5, 9.000000
                8, 14.000000
                8, 11.000000
                4, 6.000000
                2, 2.000000
                8, 10.000000
                1, 5.000000
                1, 13.000000
                7, 4.000000
                9, 4.000000
                0, 0.000000
                8, 8.000000
                0, 2.000000
                5, 4.000000
                5, 7.000000
                7, 15.000000
                1, 2.000000
                7, 7.000000
                6, 3.000000
                1, 4.000000
                2, 1.000000
                9, 7.000000
                5, 3.000000
                6, 1.000000
                6, 7.000000
                2, 4.000000
                7, 3.000000
                8, 7.000000
                9, 14.000000
                8, 6.000000
                3, 11.000000
                7, 6.000000
                0, 1.000000
                7, 6.000000
                7, 11.000000
                9, 5.000000
                0, 0.000000
                7, 12.000000
                5, 7.000000
                4, 6.000000
                4, 2.000000
                1, 8.000000
                8, 10.000000
                0, 0.000000
                0, 1.000000
                1, 7.000000
                0, 1.000000
                5, 8.000000
                5, 8.000000
                1, 7.000000
                9, 5.000000
                6, 3.000000
                9, 7.000000
                5, 5.000000
                5, 10.000000
                6, 3.000000
                4, 7.000000
                8, 5.000000
                0, 1.000000
                9, 8.000000
                9, 4.000000
                9, 4.000000
                1, 6.000000
                6, 4.000000
                9, 10.000000
                8, 4.000000
                6, 5.000000
                2, 4.000000
                1, 5.000000
                2, 4.000000
                3, 4.000000
                1, 3.000000
                7, 9.000000
                1, 6.000000
                5, 7.000000
                9, 6.000000
                4, 8.000000
                4, 3.000000
                2, 2.000000
                5, 4.000000
                8, 13.000000
                6, 1.000000
                7, 8.000000
                6, 3.000000
                8, 8.000000
                7, 9.000000
                4, 6.000000
                8, 10.000000
                2, 3.000000
                8, 17.000000
                6, 3.000000
                2, 3.000000
                9, 5.000000
                9, 5.000000
                2, 3.000000
                9, 7.000000
                4, 8.000000
                0, 2.000000
                9, 13.000000
                9, 5.000000
                4, 10.000000
                9, 4.000000
                0, 2.000000
                8, 4.000000
                2, 4.000000
                0, 0.000000
                3, 8.000000
                9, 10.000000
                0, 2.000000
                2, 1.000000
                8, 8.000000
                5, 5.000000
                2, 4.000000
                4, 5.000000
                7, 10.000000
                2, 1.000000
                7, 9.000000
                3, 11.000000
                3, 9.000000
                4, 4.000000
                2, 8.000000
                4, 8.000000
                9, 9.000000
                4, 2.000000
                2, 3.000000
                8, 11.000000
                4, 6.000000
                5, 7.000000
                9, 9.000000
                7, 7.000000
                2, 2.000000
                3, 12.000000
                6, 2.000000
                0, 2.000000
                8, 10.000000
                9, 9.000000
                4, 6.000000
                6, 3.000000
                6, 7.000000
                2, 3.000000
                1, 11.000000
                5, 5.000000
                0, 1.000000
                9, 5.000000
                5, 3.000000
                0, 1.000000
                7, 10.000000
                2, 6.000000
                4, 8.000000
                8, 9.000000
                3, 12.000000
                0, 0.000000
                0, 1.000000
                3, 8.000000
                8, 10.000000
                3, 9.000000
                6, 2.000000
                8, 6.000000
                8, 7.000000
                3, 11.000000
                4, 5.000000
                0, 3.000000
                8, 10.000000
                6, 3.000000
                4, 11.000000
                3, 9.000000
                6, 3.000000
                9, 10.000000
                0, 1.000000
                2, 4.000000
                4, 4.000000
                4, 4.000000
                6, 5.000000
                9, 9.000000
                0, 1.000000
                4, 10.000000
                9, 14.000000
                4, 5.000000
                3, 8.000000
                4, 7.000000
                3, 10.000000
                2, 2.000000
                5, 2.000000
                5, 4.000000
                9, 6.000000
                4, 7.000000
                9, 11.000000
                5, 11.000000
                6, 2.000000
                6, 7.000000
                2, 4.000000
                7, 8.000000
                9, 5.000000
                2, 1.000000
                1, 2.000000
                1, 11.000000
                6, 2.000000
                9, 8.000000
                6, 5.000000
                3, 5.000000
                1, 10.000000
                7, 15.000000
                1, 3.000000
                1, 6.000000
                7, 13.000000
                6, 4.000000
                9, 15.000000
                0, 0.000000
                4, 4.000000
                2, 2.000000
                9, 6.000000
                0, 1.000000
                3, 6.000000
                7, 7.000000
                9, 8.000000
                7, 8.000000
                9, 13.000000
                9, 3.000000
                5, 5.000000
                2, 2.000000
                6, 3.000000
                0, 1.000000
                4, 4.000000
                7, 5.000000
                5, 7.000000
                1, 6.000000
                7, 12.000000
                3, 8.000000
                4, 3.000000
                6, 7.000000
                1, 6.000000
                4, 6.000000
                1, 4.000000
                0, 1.000000
                2, 4.000000
                9, 9.000000
                2, 5.000000
                9, 6.000000
                6, 5.000000
                4, 5.000000
                1, 6.000000
                8, 5.000000
                8, 4.000000
                1, 8.000000
                7, 10.000000
                4, 10.000000
                0, 2.000000
                5, 6.000000
                2, 2.000000
                4, 7.000000
                1, 4.000000
                0, 1.000000
                6, 0.000000
                6, 2.000000
                3, 11.000000
                9, 10.000000
                7, 15.000000
                4, 6.000000
                7, 12.000000
                8, 7.000000
                2, 3.000000
                3, 8.000000
                2, 1.000000
                3, 11.000000
                6, 4.000000
                4, 3.000000
                3, 14.000000
                7, 8.000000
                4, 7.000000
                1, 6.000000
                9, 10.000000
                1, 5.000000
                5, 5.000000
                2, 3.000000
                9, 5.000000
                6, 1.000000
                8, 14.000000
                4, 3.000000
                8, 13.000000
                4, 3.000000
                2, 5.000000
                1, 9.000000
                1, 6.000000
                5, 7.000000
                2, 2.000000
                9, 11.000000
                8, 9.000000
                9, 5.000000
                1, 8.000000
                0, 0.000000
                6, 5.000000
                6, 3.000000
                8, 8.000000
                1, 7.000000
                4, 1.000000
                3, 8.000000
                9, 8.000000
                0, 1.000000
                1, 6.000000
                7, 9.000000
                7, 14.000000
                9, 3.000000
                5, 7.000000
                3, 14.000000
                7, 10.000000
                3, 8.000000
                5, 10.000000
                4, 2.000000
                2, 3.000000
                6, 2.000000
                1, 7.000000
                6, 3.000000
                5, 3.000000
                4, 4.000000
                5, 6.000000
                0, 3.000000
                5, 3.000000
                8, 6.000000
                5, 7.000000
                3, 10.000000
                1, 1.000000
                2, 4.000000
                6, 5.000000
                6, 4.000000
                0, 3.000000
                2, 2.000000
                0, 2.000000
                0, 4.000000
                8, 8.000000
                5, 4.000000
                6, 8.000000
                5, 6.000000
                9, 12.000000
                3, 9.000000
                0, 1.000000
                1, 12.000000
                7, 11.000000
                1, 14.000000
                0, 0.000000
                7, 9.000000
                5, 2.000000
                5, 5.000000
                8, 7.000000
                5, 6.000000
                1, 4.000000
                7, 13.000000
                9, 6.000000
                4, 9.000000
                9, 5.000000
                1, 7.000000
                4, 8.000000
                0, 0.000000
                9, 8.000000
                3, 11.000000
                1, 8.000000
                2, 0.000000
                3, 9.000000
                3, 9.000000
                1, 9.000000
                4, 5.000000
                0, 1.000000
                8, 7.000000
                9, 9.000000
                3, 8.000000
                1, 6.000000
                0, 1.000000
                0, 2.000000
                4, 9.000000
                8, 6.000000
                5, 8.000000
                6, 6.000000
                2, 3.000000
                7, 14.000000
                1, 5.000000
                3, 6.000000
                6, 4.000000
                2, 3.000000
                4, 6.000000
                8, 9.000000
                3, 8.000000
                7, 6.000000
                2, 5.000000
                2, 3.000000
                9, 6.000000
                4, 7.000000
                3, 7.000000
                5, 3.000000
                6, 3.000000
                5, 5.000000
                1, 8.000000
                3, 8.000000
                1, 11.000000
                9, 8.000000
                3, 7.000000
                9, 8.000000
                4, 6.000000
                8, 6.000000
                |]
    Sampler.runmcmc(parameters, multispp c_data, 10000, 50) |> Sampler.print