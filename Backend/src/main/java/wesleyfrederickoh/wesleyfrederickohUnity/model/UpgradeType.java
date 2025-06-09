package wesleyfrederickoh.wesleyfrederickohUnity.model;

import jakarta.persistence.*;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import java.util.UUID;

@Entity
@Table(name = "Upgrades") // Match dump.sql table name
public class UpgradeType {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO) // Let Hibernate handle UUID generation with DB default
    @Column(name = "UpgradeID", updatable = false, nullable = false)
    @JdbcTypeCode(SqlTypes.UUID)
    private UUID id;

    @Column(name = "Name", unique = true, nullable = false) // Renamed from upgradeName to Name
    private String name;

    @Column(name = "base_cost", nullable = false)
    private long baseCost;

    @Column(name = "cost_mult", nullable = false)
    private float costMult;

    @Column(name = "description", nullable = false, columnDefinition = "TEXT")
    private String description;

    // Constructors
    public UpgradeType() {}

    public UpgradeType(String name, long baseCost, float costMult, String description) {
        this.name = name;
        this.baseCost = baseCost;
        this.costMult = costMult;
        this.description = description;
    }

    // Getters and Setters
    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public long getBaseCost() {
        return baseCost;
    }

    public void setBaseCost(long baseCost) {
        this.baseCost = baseCost;
    }

    public float getCostMult() {
        return costMult;
    }

    public void setCostMult(float costMult) {
        this.costMult = costMult;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }
}
