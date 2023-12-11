using System;

namespace ExpenseTracker.Core.Utils.FinalResults
{
    // Fluent interface
    public class ServicesResultsDto
    {
        public bool Success { get; set; }
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public ResultsTypes ResultType { get; set; }
        public object Id { get; set; }
    }
    public class ServicesResultsDtoBuilder
    {
        private ServicesResultsDto _results = new ServicesResultsDto(); // Initializes the context

        // set the value for properties
        public ServicesResultsDtoBuilder Success(bool success = true)
        {
            _results.Success = success;
            return this;
        }

        public ServicesResultsDtoBuilder ResponseCode(string responseCode)
        {
            _results.ResponseCode = responseCode;
            return this;
        }

        public ServicesResultsDtoBuilder Message(string message)
        {
            _results.Message = message;
            return this;
        }
        public ServicesResultsDtoBuilder ResultType(ResultsTypes resultType)
        {
            _results.ResultType = resultType;
            return this;
        }
        public ServicesResultsDtoBuilder Id(object id)
        {
            _results.Id = id;
            return this;
        }
        // Prints the data to console
        public ServicesResultsDto Results()
        {
            return _results;
        }
        //public ServicesResultsDtoBuilder WithMessage(string message)
        //{
        //    _results.Message = message;
        //    return this;
        //}
        //public ServicesResultsDto Build()
        //{
        //    return _results;
        //}

    }
}
