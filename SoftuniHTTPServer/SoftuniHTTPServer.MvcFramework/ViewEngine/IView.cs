﻿namespace SoftuniHTTPServer.MvcFramework.ViewEngine
{
    public interface IView
    {
        string ExecuteTemplate(object viewModel);
    }
}
