public interface IDropdownData
{
    string[] ArrayRegion { get; }
    string[] ArrayHeader { get; }
    string[] ArraySector { get; }
    string[] ArrayClass { get; }
}
public class DataManager : IDropdownData
{//드롭다운의 메뉴를 관리함
    public string[] ArrayRegion { get; } = new string[15] { "강원", "경남", "경북", "광주", "대구", "대전", "부산", "세종", "울산", "인천", "전남", "전북", "제주", "충남", "충북" };
    public string[] ArrayHeader { get; } = new string[5] { "상호명", "상권업종대분류명", "상권업종중분류명", "상권업종소분류명", "표준산업분류명" };
    public string[] ArraySector { get; } = new string[10] { "숙박", "소매", "수리·개인", "예술·스포츠", "음식", "부동산", "과학·기술", "교육", "시설관리·임대", "보건의료" };
    public string[] ArrayClass { get; } = new string[2]  { "지역별 상권 랭킹", "시군구별 상권 랭킹" };
}
