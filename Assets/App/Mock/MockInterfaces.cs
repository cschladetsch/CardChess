﻿namespace App.Mock
{
    using App.Agent;
    using App.Model;

    public interface IBlackPlayerModel : IPlayerModel { }
    public interface IWhitePlayerModel : IPlayerModel { }

    public interface IBlackPlayerAgent : IPlayerAgent { }
    public interface IWhitePlayerAgent : IPlayerAgent { }
}
