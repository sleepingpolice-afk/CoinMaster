package wesleyfrederickoh.wesleyfrederickohUnity.dto;

public class PlayerUpgradeDTO {

    private String name;
    private String description;
    private Long baseCost;
    private Double costMult;
    private Integer level;

    public PlayerUpgradeDTO() {}

    public PlayerUpgradeDTO(String name, String description, Long baseCost, Double costMult, Integer level) {
        this.name = name;
        this.description = description;
        this.baseCost = baseCost;
        this.costMult = costMult;
        this.level = level;
    }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
    public Long getBaseCost() { return baseCost; }
    public void setBaseCost(Long baseCost) { this.baseCost = baseCost; }
    public Double getCostMult() { return costMult; }
    public void setCostMult(Double costMult) { this.costMult = costMult; }
    public Integer getLevel() { return level; }
    public void setLevel(Integer level) { this.level = level; }
}