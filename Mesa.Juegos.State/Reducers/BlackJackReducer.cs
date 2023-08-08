using Fluxor;
using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.Juegos.State.States;

namespace Mesa.Juegos.State.Reducers
{
    public class BlackJackReducer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static BlackJackSore OnStartCreateRequest(BlackJackSore state, StartCreateRequest action)
        {
            return state with
            {
                Loader = state.Loader with
                {
                    RequestIsLoading = true,
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static BlackJackSore OnEndCreateRequest(BlackJackSore state, EndCreateRequest action)
        {
            return state with
            {
                Loader = state.Loader with
                {
                    RequestIsLoading = false,
                },
                Request = action.Request
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static BlackJackSore OnStartAcceptRequest(BlackJackSore state, StartAcceptRequest action)
        {
            return state with
            {
                Loader = state.Loader with
                {
                    RequestIsLoading = true,
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static BlackJackSore OnEndAcceptRequest(BlackJackSore state, EndAcceptRequest action)
        {
            return state with
            {
                Loader = state.Loader with
                {
                    RequestIsLoading = false,
                },
                Request = action.Request
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static BlackJackSore OnStartCreateBlackJack(BlackJackSore state, StartCreateBlackJack action)
        {
            return state with
            {
                Loader = state.Loader with
                {
                    BlackJackIsLoading = true,
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static BlackJackSore OnEndCreateBlackJack(BlackJackSore state, EndCreateBlackJack action)
        {
            return state with
            {
                Loader = state.Loader with
                {
                    BlackJackIsLoading = false,
                },
                BlackjackInfo = action.BlackJackInfo
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static BlackJackSore OnClearOnError(BlackJackSore state, OnClearOnError action)
        {
            return state with
            {
                Loader = new(false, false, false)
            };
        }
    }
}
