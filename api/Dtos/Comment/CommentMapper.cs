namespace api.Dtos.Comment;

public static class CommentMapper
{
    public static CommnetDto ToCommentDto(this api.models.Comment commentModel){
        return new CommnetDto
        {
            CommentId = commentModel.CommentId,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId,
            
        }; 
    }
      public static api.models.Comment ToCommnetFromCreate(this CreateCommentDto createCommentDto, int stockId){
        return new api.models.Comment
        {
            Title = createCommentDto.Title,
            Content = createCommentDto.Content,
            StockId = stockId,
        }; 
    }
      public static api.models.Comment ToCommentFromUpdate(this UpdateCommentRequestDto updateCommentRequestDto){
        return new api.models.Comment
        {
            Title = updateCommentRequestDto.Title,
            Content = updateCommentRequestDto.Content,
        
        }; 
    }
}

