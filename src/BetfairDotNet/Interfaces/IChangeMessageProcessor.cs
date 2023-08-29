namespace BetfairDotNet.Interfaces;


internal interface IChangeMessageProcessor<in TInput, out TOutput> {
    TOutput Process(TInput input);
}