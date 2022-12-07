using Microsoft.EntityFrameworkCore;


public interface IPlaylistRepo
{
    Task<List<PlaylistRatingDto>> GetRatings();
    Task<PlaylistRatingDto?> GetRating(string playlistId);
    Task<PlaylistRatingDto> AddRating(PlaylistRatingDto rating);
    Task<PlaylistRatingDto> UpdateRating(PlaylistRatingDto rating);
    // Task DeleteRating(string playlistId);
}


public class PlaylistRepo : IPlaylistRepo {
    private readonly PlaylistDbContext _context;

    public PlaylistRepo(PlaylistDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlaylistRatingDto>> GetRatings()
    {
        return await _context.Ratings.Select(e => new PlaylistRatingDto(e.Id, e.Rating, e.SpotifyId)).ToListAsync();
    }

    public async Task<PlaylistRatingDto?> GetRating(string playlistId)
    {
        var entity = await _context.Ratings.SingleOrDefaultAsync(h => h.SpotifyId == playlistId);
        if (entity == null)
            return null;
        return EntityToDetailDto(entity);
    }

    private PlaylistRatingDto EntityToDetailDto(PlaylistRatingEntity entity)
    {
        return new PlaylistRatingDto(entity.Id, entity.Rating, entity.SpotifyId);
    }

    public async Task<PlaylistRatingDto> AddRating(PlaylistRatingDto rating)
    {
        var entity = new PlaylistRatingEntity
        {
            Id = Guid.NewGuid(),
            Rating = rating.Rating,
            SpotifyId = rating.SpotifyId
        };
        _context.Ratings.Add(entity);
        await _context.SaveChangesAsync();
        return EntityToDetailDto(entity);
    }

    public async Task<PlaylistRatingDto> UpdateRating(PlaylistRatingDto rating)
    {
        var entity = await _context.Ratings.SingleOrDefaultAsync(h => h.SpotifyId == rating.SpotifyId);
        if (entity == null)
            throw new Exception($"Playlist rating with id {rating.SpotifyId} not found");
        entity.Rating = rating.Rating;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return EntityToDetailDto(entity);
    }
}