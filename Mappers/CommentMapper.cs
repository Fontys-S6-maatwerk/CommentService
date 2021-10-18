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
                text = commentQueryModel.text
            };
        }

        public static CommentQueryModel MapToQueryModel(CommentDataModel commentDataModel)
        {
            return new CommentQueryModel()
            {
                Id = commentDataModel.Id,
                UserId = commentDataModel.UserId,
                SolutionId = commentDataModel.SolutionId,
                text = commentDataModel.text
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
