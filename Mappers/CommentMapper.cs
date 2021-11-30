using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Mappers
{
    public static class CommentMapper
    {
        public static CommentDataModel MapToDataModel(CommentQueryModel commentQueryModel)
        {
            return new CommentDataModel()
            {
                Id = commentQueryModel.Id,
                UserId = commentQueryModel.UserId,
                SolutionId = commentQueryModel.SolutionId,
                Text = commentQueryModel.Content,
                CreationTime = commentQueryModel.CreationTime,
                User = MapToUserDBO(commentQueryModel.User)
            };
        }

        public static UserDBO MapToUserDBO(UserQM user)
        {
            if (user.Equals(null))
            {
                return null;
            }

            return new UserDBO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public static UserQM MapToUserQM(UserDBO user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserQM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public static CommentQueryModel MapToQueryModel(CommentDataModel commentDataModel)
        {
            return new CommentQueryModel()
            {
                Id = commentDataModel.Id,
                UserId = commentDataModel.UserId,
                SolutionId = commentDataModel.SolutionId,
                Content = commentDataModel.Text,
                CreationTime = commentDataModel.CreationTime,
                User = MapToUserQM(commentDataModel.User)
            };
        }

        internal static CommentPageResponse MapToQueryPage(CommentPage commentPage)
        {
            return new CommentPageResponse()
            {
                Items = MapToQueryModels(commentPage.Items),
                TotalElements = commentPage.TotalElements,
                TotalPages = commentPage.TotalPages
            };
        }

        public static List<CommentDataModel> MapToDataModels(List<CommentQueryModel> commentQueryModels)
        {
            return commentQueryModels.Select(comment => MapToDataModel(comment)).ToList();
        }

        public static List<CommentQueryModel> MapToQueryModels(List<CommentDataModel> commentDataModels)
        {
            return commentDataModels.Select(comment => MapToQueryModel(comment)).ToList();
        }
    }
}
