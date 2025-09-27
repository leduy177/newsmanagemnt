using AutoMapper;
using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src =>
                    src.UserArticles.FirstOrDefault(ua => ua.RoleInArticle == "AUTHOR")!.User.FullName))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.ArticleTags != null
                        ? src.ArticleTags
                            .Where(at => at.Tag != null)
                            .Select(at => at.Tag.Name)
                            .ToList()
                        : new List<string>()));

            CreateMap<ArticleCreateDto, Article>();
            CreateMap<ArticleUpdateDto, Article>();
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.ArticleTitle, opt => opt.MapFrom(src => src.Article.Title))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<CommentDTO, Comment>();

            // ✅ Mapping từ Entity -> DTO
            CreateMap<UserArticle, UserArticleDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            // ✅ Mapping DTO -> Entity
            CreateMap<UserArticleDTO, UserArticle>()
                .ForMember(dest => dest.User, opt => opt.Ignore());

            // ✅ Mapping từ UserArticleCreateDTO -> Entity
            CreateMap<UserArticleCreateDTO, UserArticle>()
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Tag, TagCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
        }
    }
}
